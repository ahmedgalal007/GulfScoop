// Source Article
// https://dejanstojanovic.net/aspnet/2018/march/using-webp-images-in-apsnet-with-a-fallback-to-jpeg-and-png/

using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace gulfsoccer.Handlers
{
    public class WebPHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Url.AbsoluteUri.EndsWith(".webp", StringComparison.InvariantCultureIgnoreCase))
            {
                var path = context.Request.Url.AbsolutePath;
                path = path.TrimEnd(".webp".ToCharArray());
                string[] Dimentios = path.Split('/').Last()?.Split('x');
                int width = 0, height = 0;
                string file; // = HttpUtility.UrlDecode(Uri);
                if (int.TryParse(Dimentios[0], out width) && int.TryParse(Dimentios[1], out height))
                {
                    file = context.Server.MapPath("/") + String.Join("\\", path.Split('/').Take(path.Split('/').Length - 1));
                }
                else
                {
                    file = context.Server.MapPath(path);
                }

                byte[] photoBytes = System.IO.File.ReadAllBytes(file);
                // Format is automatically detected though can be changed.
                ISupportedImageFormat format = new WebPFormat { Quality = 70, };
                Size size = new Size(width, height);
                //if (context.Request.UserAgent.IndexOf("Chrome/", StringComparison.InvariantCultureIgnoreCase) >= 0)
                //{
                    context.Response.ClearHeaders();
                    context.Response.ClearContent();

                    if (File.Exists(file))
                    {

                        // var content = File.ReadAllBytes(path);
                        // context.Response.OutputStream.Write(content, 0, content.Length);
                        // context.Response.OutputStream.Flush();
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
                                context.Response.ContentType = "image/webp";
                                context.Response.Buffer = false;
                                context.Response.BufferOutput = false;

                                outStream.CopyToAsync(context.Response.OutputStream);
                                context.Response.AppendHeader("Content-type", "image/webp");
                                context.Response.OutputStream.Flush();
                                
                                //this.Response.Write(outStream.GetBuffer());
                                // return new HttpStatusCodeResult(200);
                            }
                        }
                        
                    }
                    else
                    {
                        ImageFallback(context, path);
                    }
                //}
                //else
                //{
                //    ImageFallback(context, path);
                //}
            }
        }

        private String GetImagePath(String path, String extension)
        {
            return Path.Combine(Path.GetDirectoryName(path), String.Concat(Path.GetFileNameWithoutExtension(path), ".", extension));
        }

        private void ImageFallback(HttpContext context, String path)
        {
            var extensions = new String[] { "png", "jpg", "jpeg" };
            bool found = false;
            foreach (var extension in extensions)
            {
                var imagePath = GetImagePath(path, extension);
                if (File.Exists(imagePath))
                {
                    var staticUrl = context.Request.Url.AbsoluteUri.Substring(0, context.Request.Url.AbsoluteUri.LastIndexOf("/"));
                    staticUrl = String.Concat(staticUrl, "/", Path.GetFileName(imagePath));
                    found = true;
                    context.Response.Redirect(staticUrl);
                }
            }

            if (!found)
            {
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.StatusCode = 404;
            }
        }
    }
}