using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

/// <summary>
/// Summary description for ImageThumb
/// </summary>
public class ImageThumb
{
    /// <summary>
    /// This class will create imageThumbs to my own use.
    /// </summary>
    public ImageThumb()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Create ThumbNail using .net functions 
    /// </summary>
    /// <param name="nome">Name of Image</param>
    /// <param name="_path">Origin Path Dir</param>
    /// <param name="_pathsave">Destiny path dir</param>
    /// <param name="_bStretch">Maintain aspect of image</param>
    /// <param name="_width">new size</param>
    /// <param name="_height">new height</param>
    /// <returns>Name of new thumb image file</returns>
    public static string createThumb(string nome, string _path, string _pathsave, bool _bStretch, int _width)
    {
        Bitmap bitmapNew = new Bitmap(_path + "\\" + nome); // load original image

        int widthOrig = 0;
        int heightOrig = 0;
        int widthTh = 0; int f = 0; int heightTh = 0;
        int _height = 0;


        widthOrig = bitmapNew.Width;
        heightOrig = bitmapNew.Height;
        int fx = widthOrig / _width;

        _height = heightOrig / fx;

        int fy = heightOrig / _height; // subsampling factors




        if (!_bStretch)
        { // retain aspect ratio

            
            // must fit in thumbnail size

            f = Math.Max(fx, fy); if (f < 1) f = 1;
            widthTh = (int)(widthOrig / f); heightTh = (int)(heightOrig / f);
        }
        else
        {
            widthTh = _width; heightTh = _height;
        }
        bitmapNew = (Bitmap)bitmapNew.GetThumbnailImage(widthTh, heightTh,
          new System.Drawing.Image.GetThumbnailImageAbort(ImageThumb.ThumbnailCallback), IntPtr.Zero);

        


        bitmapNew.Save(_pathsave + "\\" + nome);
        bitmapNew.Dispose();
        return _pathsave + "\\" + nome;

    }

    public static bool ThumbnailCallback() { return false; }
    
    
    /// <summary>
    /// Get a prefix for unique file name
    /// </summary>
    /// <returns></returns>
    public static string getPrefixUniqueName()
    {
        System.Guid guid = System.Guid.NewGuid ();

        string pref = DateTime.Now.ToString("yyyyMMddHHmiss") + "_" + guid.ToString().Substring(0, 3) + "_";
        
        return pref;
    }
}
 