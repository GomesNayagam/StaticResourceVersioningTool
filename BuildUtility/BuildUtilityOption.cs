namespace BuildUtility
{
    public class BuildUtilityOption
    {
        public string Version { get; set; }
        public string ContentFileTypes { get; set; }
        public string TartgetFileTypes { get; set; }
        public string RootFolder { get; set; }
        public bool ScanSubFolders { get; set; }
        public bool MinificationRequired { get; set; }
        public bool VerboseOutput { get; set; }
        public bool UpdateVersion { get; set; }
    }
}
