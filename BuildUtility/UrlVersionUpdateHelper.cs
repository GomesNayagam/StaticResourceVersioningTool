namespace BuildUtility
{
    using System.Text.RegularExpressions;

    public class UrlVersionUpdateHelper
    {
        private Regex m_fileExtentionRegEx;
        private string m_fileExtention;
        private string m_replaceText;

        public UrlVersionUpdateHelper(string fileExtention, bool update)
        {
            var regExString = string.Empty;
            if (update)
            {
                regExString = "." + fileExtention + "\\?v=.*?(?=(\"|'))";
                this.m_replaceText = "." + fileExtention;
            }
            else
            {
                regExString = "(href|src)=.*\\." + fileExtention + "?(?=(\"|'))";
                this.m_replaceText = "${0}";
            }
            //
            //var updateRegEx = update ? "\\?v=.*?\"" : "\"";
            this.m_fileExtention = fileExtention;
            this.m_fileExtentionRegEx = new Regex(regExString, RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public bool AppendVersion(ref string content, string version)
        {
            var modified = this.m_fileExtentionRegEx.IsMatch(content);
            if (modified)
            {
                //content = this.m_fileExtentionRegEx.Replace(content, "." + this.m_fileExtention + "?v=" + version + "\"");
                content = this.m_fileExtentionRegEx.Replace(content, this.m_replaceText + "?v=" + version);
            }
            return modified;
        }
    }
}
