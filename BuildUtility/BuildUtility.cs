namespace BuildUtility
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class BuildUtility : IBuildUtility, IDisposable
    {
        private Collection<UrlVersionUpdateHelper> m_urlUpdateHelpers = new Collection<UrlVersionUpdateHelper>();
        private Logger m_logger = new Logger();

        ~BuildUtility()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_logger != null)
                {
                    this.m_logger.Dispose();
                    this.m_logger = null;
                }
            }
        }

        public bool VersionStaticContentUrl(BuildUtilityOption buildUtilityOptions)
        {
            var filesModified = 0;
            try
            {
                this.m_logger.Write("Started task: VersionStaticContentUrl at " + DateTime.Now.ToString(CultureInfo.InvariantCulture), true);
                this.m_urlUpdateHelpers.Clear();
                var targetFileTypes = buildUtilityOptions.TartgetFileTypes.Split(new char[] { '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var contentExtentions = buildUtilityOptions.ContentFileTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var fileType in targetFileTypes)
                {
                    this.m_urlUpdateHelpers.Add(new UrlVersionUpdateHelper(fileType, buildUtilityOptions.UpdateVersion));
                }

                var rootFolder = new DirectoryInfo(buildUtilityOptions.RootFolder);
                if (rootFolder.Exists)
                {
                    var files = rootFolder.GetFiles(
                        "*.*",
                        buildUtilityOptions.ScanSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
                        ).Where(f => contentExtentions.Contains(f.Extension, StringComparer.OrdinalIgnoreCase)).ToList();
                    this.m_logger.Write("Number of files found : " + files.Count, true);
                    foreach (var file in files)
                    {
                        var fileContent = File.ReadAllText(file.FullName, Encoding.UTF8);
                        var modified = false;
                        foreach (var urlUpdateHelper in this.m_urlUpdateHelpers)
                        {
                            var result = urlUpdateHelper.AppendVersion(ref fileContent, buildUtilityOptions.Version);
                            modified = modified || result;
                        }
                        if (modified)
                        {
                            File.WriteAllText(file.FullName, fileContent, Encoding.UTF8);
                            filesModified++;
                            this.m_logger.Write(string.Format("Updated {0}", file.FullName), buildUtilityOptions.VerboseOutput);
                        }
                    }
                }
                this.m_logger.Write("Number of files modified : " + filesModified, true);
                this.m_logger.Write("Completed task: VersionStaticContentUrl at " + DateTime.Now.ToString(CultureInfo.InvariantCulture), true);
                return true;
            }
            catch (Exception ex)
            {
                this.m_logger.Write(ex.Message, false);
                this.m_logger.Write("Number of files modified : " + filesModified, true);
                this.m_logger.Write("Completed task: VersionStaticContentUrl at " + DateTime.Now.ToString(CultureInfo.InvariantCulture), true);
                return false;
            }
        }
    }
}
