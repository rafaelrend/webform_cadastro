using System;
using System.IO;
using System.Text;
using System.IO.Compression;
using ICSharpCode.SharpZipLib;

namespace Utilities
{
    public enum CompressionType
    {
        GZip,
        BZip2,
        Zip,
        Deflate_Net,
        GZip_Net
    }

    public class Compression
    {

        public static CompressionType CompressionProvider = CompressionType.BZip2;

        private static Stream OutputStream(Stream outputStream)
        {
            return OutputStream(outputStream, CompressionProvider);
        }

        private static Stream OutputStream(Stream outputStream, CompressionType type)
        {
            switch (type)
            {
                case CompressionType.BZip2:
                    return new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(outputStream);

                case CompressionType.GZip:
                    return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(outputStream);

                case CompressionType.Zip:
                    return new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outputStream);

                case CompressionType.Deflate_Net:
                    return new DeflateStream(outputStream, CompressionMode.Compress, true);

                case CompressionType.GZip_Net:
                    return new GZipStream(outputStream, CompressionMode.Compress, true);

                default:
                    return new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(outputStream);
            }
        }

        private static Stream InputStream(Stream inputStream)
        {
            return InputStream(inputStream, CompressionProvider);
        }

        private static Stream InputStream(Stream inputStream, CompressionType type)
        {
            switch (type)
            {
                case CompressionType.BZip2:
                    return new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(inputStream);

                case CompressionType.GZip:
                    return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);

                case CompressionType.Zip:
                    return new ICSharpCode.SharpZipLib.Zip.ZipInputStream(inputStream);

                case CompressionType.Deflate_Net:
                    return new DeflateStream(inputStream, CompressionMode.Decompress);

                case CompressionType.GZip_Net:
                    return new GZipStream(inputStream, CompressionMode.Decompress);

                default:
                    return new ICSharpCode.SharpZipLib.GZip.GZipInputStream(inputStream);
            }
        }

        public static string Compress(string stringToCompress)
        {
            return Compress(stringToCompress, CompressionProvider);
        }

        public static string Compress(string stringToCompress, CompressionType type)
        {
            byte[] compressedData = CompressToByte(stringToCompress, type);
            string strOut = Convert.ToBase64String(compressedData);

            return strOut;
        }

        public static byte[] Compress(byte[] bytesToCompress)
        {
            return Compress(bytesToCompress, CompressionProvider);
        }

        public static byte[] Compress(byte[] bytesToCompress, CompressionType type)
        {
            MemoryStream ms = new MemoryStream();
            Stream s = OutputStream(ms, type);
            s.Write(bytesToCompress, 0, bytesToCompress.Length);
            s.Close();

            return ms.ToArray();
        }

        public static byte[] CompressToByte(string stringToCompress)
        {
            return CompressToByte(stringToCompress, CompressionProvider);
        }

        public static byte[] CompressToByte(string stringToCompress, CompressionType type)
        {
            byte[] bytData = Encoding.Unicode.GetBytes(stringToCompress);

            return Compress(bytData, type);
        }

        public static string DeCompress(string stringToDecompress)
        {
            return DeCompress(stringToDecompress, CompressionProvider);
        }

        public static string DeCompress(string stringToDecompress, CompressionType type)
        {
            string outString = String.Empty;

            if (stringToDecompress == null)
            {
                throw new ArgumentNullException("stringToDecompress", "You tried to use an empty string");
            }

            try
            {
                byte[] inArr = Convert.FromBase64String(stringToDecompress.Trim());
                outString = System.Text.Encoding.Unicode.GetString(DeCompress(inArr, type));
            }
            catch (NullReferenceException nEx)
            {
                return nEx.Message;
            }

            return outString;
        }

        public static byte[] DeCompress(byte[] bytesToDecompress)
        {
            return DeCompress(bytesToDecompress, CompressionProvider);
        }

        public static byte[] DeCompress(byte[] bytesToDecompress, CompressionType type)
        {
            byte[] writeData = new byte[4096];
            Stream s = InputStream(new MemoryStream(bytesToDecompress), type);
            MemoryStream outStream = new MemoryStream();

            while (true)
            {
                int size = s.Read(writeData, 0, writeData.Length);

                if (size > 0)
                {
                    outStream.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }

            s.Close();
            byte[] outArr = outStream.ToArray();
            outStream.Close();

            return outArr;
        }
    }
}
