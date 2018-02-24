using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ecommerce01.Classes
{
    public class FilesHelper
    {
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name, string name2)
        {

            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return false;
            }
            try
            {
                if (file != null)
                {
                    //
                    //
                   // var oldpath = Path.Combine(HttpContext.Current.Server.MapPath(folder), name2);
                     //System.IO.File.Delete(Path.Combine(HttpContext.Current.Server.MapPath(folder), name2));
                    var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
               
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}