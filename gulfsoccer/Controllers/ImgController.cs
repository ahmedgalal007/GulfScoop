using gulfsoccer.Models;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Controllers
{
    public class ImgController : Controller
    {
        // GET: Category/Id
        // GET: Img
        [OutputCache(Duration = 86400, VaryByParam = "Uri",Location =System.Web.UI.OutputCacheLocation.Server)]
        public async Task<ActionResult> Index(string Uri)
        {
            if(Uri.EndsWith(".webp", StringComparison.InvariantCultureIgnoreCase))
            {
                Uri = Uri.TrimEnd(".webp".ToCharArray());
            }
            string[] Dimentios = Uri.Split('/').Last()?.Split('x');
            // string[] Dimentios = imgSize?.Split('x');

            int width = 0, height = 0;
            string file; // = HttpUtility.UrlDecode(Uri);
            if (int.TryParse(Dimentios[0], out width) && int.TryParse(Dimentios[1], out height))
            {
                file = this.Server.MapPath("/") + String.Join("\\", Uri.Split('/').Take(Uri.Split('/').Length - 1));
            }
            else
            {
                file = this.Server.MapPath(Uri);
            }

            byte[] photoBytes = System.IO.File.ReadAllBytes(file);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new WebPFormat { Quality = 70,  };
            Size size = new Size(width, height);
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(format)
                                    .Save(outStream);
                    }
                    // Do something with the stream.
                    Response.ContentType = "image/webp";
                    Response.Buffer = false;
                    Response.BufferOutput = false;

                    await outStream.CopyToAsync(this.Response.OutputStream);
                    //this.Response.Write(outStream.GetBuffer());
                    return new HttpStatusCodeResult(200);
                }
            }
        }
    }
}