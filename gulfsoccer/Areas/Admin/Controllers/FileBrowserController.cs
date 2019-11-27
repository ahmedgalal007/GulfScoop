using gulfsoccer.Areas.Admin.FileBrowser.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gulfsoccer.Areas.Admin.Controllers
{

    public class FileBrowserController : EditorFileBrowserController
    {
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

        /// <summary>
        /// Gets the valid file extensions by which served files will be filtered.
        /// </summary>
        public override string Filter
        {
            get
            {
                return "*.txt, *.doc, *.docx, *.xls, *.xlsx, *.ppt, *.pptx, *.zip, *.rar, *.jpg, *.jpeg, *.gif, *.png";
            }
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
    }


    //public class FileBrowserController : Controller
    //{
    //    private const string contentFolderRoot = "~/Content/";
    //    private const string prettyName = "Images/";
    //    private static readonly string[] foldersToCopy = new[] { "~/Content/" };
    //    private const string DefaultFilter = "*.txt,*.doc,*.docx,*.xls,*.xlsx,*.ppt,*.pptx,*.zip,*.rar,*.jpg,*.jpeg,*.gif,*.png";

    //    private readonly FileBrowser.Models.DirectoryBrowser directoryBrowser;
    //    private readonly ContentInitializer contentInitializer;

    //    public FileBrowserController()
    //    {
    //        directoryBrowser = new FileBrowser.Models.DirectoryBrowser();
    //        contentInitializer = new ContentInitializer(contentFolderRoot, foldersToCopy, prettyName);
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
    //    public ActionResult File(string fileName)
    //    {
    //        var path = NormalizePath(fileName);

    //        if (AuthorizeFile(path))
    //        {
    //            var physicalPath = Server.MapPath(path);

    //            if (System.IO.File.Exists(physicalPath))
    //            {
    //                const string contentType = "application/octet-stream";
    //                return File(System.IO.File.OpenRead(physicalPath), contentType, fileName);
    //            }
    //        }

    //        throw new HttpException(403, "Forbidden");
    //    }

    //    public virtual bool AuthorizeFile(string path)
    //    {
    //        return CanAccess(path) && IsValidFile(Path.GetExtension(path));
    //    }
    //}
}