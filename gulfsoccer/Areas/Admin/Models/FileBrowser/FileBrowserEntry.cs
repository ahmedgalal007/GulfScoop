namespace gulfsoccer.Areas.Admin.FileBrowser.Models
{
    public class FileBrowserEntry
    {
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public long Size { get; set; }
    }
}