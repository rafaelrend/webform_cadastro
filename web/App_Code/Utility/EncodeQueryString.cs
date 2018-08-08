using System;
using System.Web;
using System.Text;
using System.Globalization;
using Utilities;

namespace Utilities
{
    public class Encode
    {
        public enum EncodeType
        {
            None,
            Hex,
            GZip,
            BZip2,
            GZipHex,
            BZip2Hex
        }

        private Encode()
        {
        }

        public static QueryString EncodeQueryString(QueryString queryString)
        {
            return EncodeQueryString(queryString, EncodeType.Hex);
        }

        public static QueryString EncodeQueryString(QueryString queryString, EncodeType type)
        {
            QueryString newQueryString = new QueryString();
            string nm = String.Empty;
            string val = String.Empty;

            foreach (string name in queryString)
            {
                nm = name;
                val = queryString[name];

                switch (type)
                {
                    case EncodeType.None:
                        newQueryString.Add(nm, HttpContext.Current.Server.UrlEncode(val));
                        break;
                    
                    case EncodeType.Hex:
                        newQueryString.Add(nm, Hex(val));
                        break;
                    
                    case EncodeType.GZip:
                        newQueryString.Add(nm, HttpContext.Current.Server.UrlEncode(Compression.Compress(val, CompressionType.GZip)));
                        break;
                    
                    case EncodeType.BZip2:
                        newQueryString.Add(nm, HttpContext.Current.Server.UrlEncode(Compression.Compress(val, CompressionType.BZip2)));
                        break;
                    
                    case EncodeType.GZipHex:
                        newQueryString.Add(nm, HttpContext.Current.Server.UrlEncode(Compression.Compress(Hex(val), CompressionType.GZip)));
                        break;
                    
                    case EncodeType.BZip2Hex:
                        newQueryString.Add(nm, HttpContext.Current.Server.UrlEncode(Compression.Compress(Hex(val), CompressionType.BZip2)));
                        break;
                }
            }

            return newQueryString;
        }

        public static QueryString DecodeQueryString(QueryString queryString)
        {
            return DecodeQueryString(queryString, EncodeType.Hex);
        }

        public static QueryString DecodeQueryString(QueryString queryString, EncodeType type)
        {
            QueryString newQueryString = new QueryString();
            string nm;
            string val;

            foreach (string name in queryString)
            {
                nm = name;
                switch (type)
                {
                    case EncodeType.None:
                        val = HttpContext.Current.Server.UrlDecode(queryString[name]);
                        break;

                    case EncodeType.Hex:
                        val = DeHex(queryString[name]);
                        break;

                    case EncodeType.GZip:
                        val = Compression.DeCompress(HttpContext.Current.Server.UrlDecode(queryString[name]), CompressionType.GZip);
                        break;

                    case EncodeType.BZip2:
                        val = Compression.DeCompress(HttpContext.Current.Server.UrlDecode(queryString[name]), CompressionType.BZip2);
                        break;

                    case EncodeType.GZipHex:
                        val = DeHex(Compression.DeCompress(HttpContext.Current.Server.UrlDecode(queryString[name]), CompressionType.GZip));
                        break;

                    case EncodeType.BZip2Hex:
                        val = DeHex(Compression.DeCompress(HttpContext.Current.Server.UrlDecode(queryString[name]), CompressionType.BZip2));
                        break;
                    
                    default:
                        val = DeHex(queryString[name]);
                        break;
                }
                
                newQueryString.Add(nm, val);
            }

            return newQueryString;
        }

        public static string DeHex(string hexstring)
        {
            string ret = String.Empty;
            StringBuilder sb = new StringBuilder(hexstring.Length / 2);

            for (int i = 0; i <= hexstring.Length - 1; i = i + 2)
            {
                sb.Append((char)int.Parse(hexstring.Substring(i, 2), NumberStyles.HexNumber));
            }

            return sb.ToString();
        }

        public static string Hex(string sData)
        {
            string temp = String.Empty; ;
            string newdata = String.Empty;
            StringBuilder sb = new StringBuilder(sData.Length * 2);

            for (int i = 0; i < sData.Length; i++)
            {
                if ((sData.Length - (i + 1)) > 0)
                {
                    temp = sData.Substring(i, 2);

                    if (temp == @"\n") newdata += "0A";
                    else if (temp == @"\b") newdata += "20";
                    else if (temp == @"\r") newdata += "0D";
                    else if (temp == @"\c") newdata += "2C";
                    else if (temp == @"\\") newdata += "5C";
                    else if (temp == @"\0") newdata += "00";
                    else if (temp == @"\t") newdata += "07";
                    else
                    {
                        sb.Append(String.Format("{0:X2}", (int)(sData.ToCharArray())[i]));
                        i--;
                    }
                }
                else
                {
                    sb.Append(String.Format("{0:X2}", (int)(sData.ToCharArray())[i]));
                }

                i++;
            }

            return sb.ToString();
        }
    }
}
