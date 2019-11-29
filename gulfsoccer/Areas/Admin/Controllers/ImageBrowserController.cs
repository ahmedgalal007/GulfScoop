﻿using gulfsoccer.Areas.Admin.FileBrowser.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.Controllers
{
    public class ImageBrowserController : EditorImageBrowserController
    {
        private const string DefaultFilter = "*.png,*.gif,*.jpg,*.jpeg";
        private const string contentFolderRoot = "~/Content/";
        private const string prettyName = "Images/";
        private static readonly string[] foldersToCopy = new[] { "~/Content/shared/" };


        /// <summary>
        /// Gets the base paths from which content will be served.
        /// </summary>
        public override string ContentPath
        {
            get
            {
                return CreateUserFolder();
            }
        }
        public override ActionResult Thumbnail(string path)
        {
            return base.Thumbnail(path);
        }

        public override ActionResult Upload(string path, HttpPostedFileBase file)
        {
            return base.Upload(path, file);
        }
        private string CreateUserFolder()
        {
            var virtualPath = Path.Combine(contentFolderRoot, "UserFiles", prettyName);

            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                foreach (var sourceFolder in foldersToCopy)
                {
                    CopyFolder(Server.MapPath(sourceFolder), path);
                }
            }
            return virtualPath;
        }

        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }

            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

        //[OutputCache(Duration = 360, VaryByParam = "path")]
        //public ActionResult Image(string path)
        //{
        //    path = NormalizePath(path);

        //    if (AuthorizeImage(path))
        //    {
        //        var physicalPath = Server.MapPath(path);

        //        if (System.IO.File.Exists(physicalPath))
        //        {
        //            const string contentType = "image/png";
        //            return File(System.IO.File.OpenRead(physicalPath), contentType);
        //        }
        //    }

        //    throw new HttpException(403, "Forbidden");
        //}
        //public virtual bool AuthorizeImage(string path)
        //{
        //    return CanAccess(path) && IsValidFile(Path.GetExtension(path));
        //}

        //private bool IsValidFile(string fileName)
        //{
        //    var extension = Path.GetExtension(fileName);
        //    var allowedExtensions = DefaultFilter.Split(',');

        //    return allowedExtensions.Any(e => e.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase));
        //}
    }

    //public class ImageBrowserController : Controller
    //{
    //    private const string contentFolderRoot = "~/Content/";
    //    private const string prettyName = "Images/";
    //    private static readonly string[] foldersToCopy = new[] { "~/Content/" };
    //    private const string DefaultFilter = "*.png,*.gif,*.jpg,*.jpeg";

    //    private const int ThumbnailHeight = 80;
    //    private const int ThumbnailWidth = 80;

    //    private readonly FileBrowser.Models.DirectoryBrowser directoryBrowser;
    //    private readonly ContentInitializer contentInitializer;
    //    private readonly FileBrowser.Models.ThumbnailCreator thumbnailCreator;

    //    public ImageBrowserController()
    //    {
    //        directoryBrowser = new FileBrowser.Models.DirectoryBrowser();
    //        contentInitializer = new ContentInitializer(contentFolderRoot, foldersToCopy, prettyName);
    //        thumbnailCreator = new FileBrowser.Models.ThumbnailCreator();
    //    }

    //    public string ContentPath
    //    {
    //        get
    //        {
    //            return contentInitializer.CreateUserFolder(Server);
    //        }
    //    }

    //    private string ToAbsolute(string virtualPath)
    //    {
    //        return VirtualPathUtility.ToAbsolute(virtualPath);
    //    }

    //    private string CombinePaths(string basePath, string relativePath)
    //    {
    //        return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), relativePath);
    //    }

    //    public virtual bool AuthorizeRead(string path)
    //    {
    //        return CanAccess(path);
    //    }

    //    protected virtual bool CanAccess(string path)
    //    {
    //        return path.StartsWith(ToAbsolute(ContentPath), StringComparison.OrdinalIgnoreCase);
    //    }

    //    private string NormalizePath(string path)
    //    {
    //        if (string.IsNullOrEmpty(path))
    //        {
    //            return ToAbsolute(ContentPath);
    //        }

    //        return CombinePaths(ToAbsolute(ContentPath), path);
    //    }

    //    public virtual JsonResult Read(string path)
    //    {
    //        path = NormalizePath(path);

    //        if (AuthorizeRead(path))
    //        {
    //            try
    //            {
    //                directoryBrowser.Server = Server;

    //                var result = directoryBrowser
    //                    .GetContent(path, DefaultFilter)
    //                    .Select(f => new
    //                    {
    //                        name = f.Name,
    //                        type = f.Type == EntryType.File ? "f" : "d",
    //                        size = f.Size
    //                    });

    //                return Json(result, JsonRequestBehavior.AllowGet);
    //            }
    //            catch (DirectoryNotFoundException)
    //            {
    //                throw new HttpException(404, "File Not Found");
    //            }
    //        }

    //        throw new HttpException(403, "Forbidden");
    //    }


    //    public virtual bool AuthorizeThumbnail(string path)
    //    {
    //        return CanAccess(path);
    //    }

    //    [OutputCache(Duration = 3600, VaryByParam = "path")]
    //    public virtual ActionResult Thumbnail(string path)
    //    {
    //        path = NormalizePath(path);

    //        if (AuthorizeThumbnail(path))
    //        {
    //            var physicalPath = Server.MapPath(path);

    //            if (System.IO.File.Exists(physicalPath))
    //            {
    //                Response.AddFileDependency(physicalPath);

    //                return CreateThumbnail(physicalPath);
    //            }
    //            else
    //            {
    //                throw new HttpException(404, "File Not Found");
    //            }
    //        }
    //        else
    //        {
    //            throw new HttpException(403, "Forbidden");
    //        }
    //    }

    //    private FileContentResult CreateThumbnail(string physicalPath)
    //    {
    //        using (var fileStream = System.IO.File.OpenRead(physicalPath))
    //        {
    //            var desiredSize = new FileBrowser.Models.ImageSize
    //            {
    //                Width = ThumbnailWidth,
    //                Height = ThumbnailHeight
    //            };

    //            const string contentType = "image/png";

    //            return File(thumbnailCreator.Create(fileStream, desiredSize, contentType), contentType);
    //        }
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public virtual ActionResult Destroy(string path, string name, string type)
    //    {
    //        path = NormalizePath(path);

    //        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
    //        {
    //            path = CombinePaths(path, name);
    //            if (type.ToLowerInvariant() == "f")
    //            {
    //                DeleteFile(path);
    //            }
    //            else
    //            {
    //                DeleteDirectory(path);
    //            }

    //            return Json(new object[0]);
    //        }
    //        throw new HttpException(404, "File Not Found");
    //    }

    //    public virtual bool AuthorizeDeleteFile(string path)
    //    {
    //        return CanAccess(path);
    //    }

    //    public virtual bool AuthorizeDeleteDirectory(string path)
    //    {
    //        return CanAccess(path);
    //    }

    //    protected virtual void DeleteFile(string path)
    //    {
    //        if (!AuthorizeDeleteFile(path))
    //        {
    //            throw new HttpException(403, "Forbidden");
    //        }

    //        var physicalPath = Server.MapPath(path);

    //        if (System.IO.File.Exists(physicalPath))
    //        {
    //            System.IO.File.Delete(physicalPath);
    //        }
    //    }

    //    protected virtual void DeleteDirectory(string path)
    //    {
    //        if (!AuthorizeDeleteDirectory(path))
    //        {
    //            throw new HttpException(403, "Forbidden");
    //        }

    //        var physicalPath = Server.MapPath(path);

    //        if (Directory.Exists(physicalPath))
    //        {
    //            Directory.Delete(physicalPath, true);
    //        }
    //    }

    //    public virtual bool AuthorizeCreateDirectory(string path, string name)
    //    {
    //        return CanAccess(path);
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public virtual ActionResult Create(string path, FileBrowser.Models.FileBrowserEntry entry)
    //    {
    //        path = NormalizePath(path);
    //        var name = entry.Name;

    //        if (!string.IsNullOrEmpty(name) && AuthorizeCreateDirectory(path, name))
    //        {
    //            var physicalPath = Path.Combine(Server.MapPath(path), name);

    //            if (!Directory.Exists(physicalPath))
    //            {
    //                Directory.CreateDirectory(physicalPath);
    //            }

    //            return Json(new
    //            {
    //                name = entry.Name,
    //                type = "d",
    //                size = entry.Size
    //            });
    //        }

    //        throw new HttpException(403, "Forbidden");
    //    }


    //    public virtual bool AuthorizeUpload(string path, HttpPostedFileBase file)
    //    {
    //        return CanAccess(path) && IsValidFile(file.FileName);
    //    }

    //    private bool IsValidFile(string fileName)
    //    {
    //        var extension = Path.GetExtension(fileName);
    //        var allowedExtensions = DefaultFilter.Split(',');

    //        return allowedExtensions.Any(e => e.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase));
    //    }

    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public virtual ActionResult Upload(string path, HttpPostedFileBase file)
    //    {
    //        path = NormalizePath(path);
    //        var fileName = Path.GetFileName(file.FileName);

    //        if (AuthorizeUpload(path, file))
    //        {
    //            file.SaveAs(Path.Combine(Server.MapPath(path), fileName));

    //            return Json(new
    //            {
    //                size = file.ContentLength,
    //                name = fileName,
    //                type = "f"
    //            }, "text/plain");
    //        }

    //        throw new HttpException(403, "Forbidden");
    //    }

    //    [OutputCache(Duration = 360, VaryByParam = "path")]
    //    public ActionResult Image(string path)
    //    {
    //        path = NormalizePath(path);

    //        if (AuthorizeImage(path))
    //        {
    //            var physicalPath = Server.MapPath(path);

    //            if (System.IO.File.Exists(physicalPath))
    //            {
    //                const string contentType = "image/png";
    //                return File(System.IO.File.OpenRead(physicalPath), contentType);
    //            }
    //        }

    //        throw new HttpException(403, "Forbidden");
    //    }

    //    public virtual bool AuthorizeImage(string path)
    //    {
    //        return CanAccess(path) && IsValidFile(Path.GetExtension(path));
    //    }
    //}
}