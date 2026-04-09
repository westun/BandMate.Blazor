namespace BandMate.Shared.Security
{
    public class FileUploadValidator : IFileUploadValidator
    {
        private readonly HashSet<string> _notAllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            // Executables & Binaries
            ".exe", ".dll", ".com", ".msi", ".msu", ".bin", ".scr", ".cpl", ".ocx", ".drv", ".sys",

            // Scripts & Web Shells
            ".php", ".php3", ".php4", ".php5", ".phtml", ".asp", ".aspx", ".asax", ".jsp", ".jspx",
            ".js", ".jse", ".vbs", ".vbe", ".vb", ".ps1", ".ps1xml", ".ps2", ".bat", ".cmd", ".sh",
            ".bash", ".pl", ".cgi", ".py", ".pyc", ".jar", ".app", ".wsf", ".wsc", ".wsh",

            // Configuration & Server Files
            ".htaccess", ".htpasswd", ".config", ".web.config", ".pif", ".lnk", ".reg", ".inf",

            // Macro-Enabled Documents (Often contain malware)
            ".docm", ".dotm", ".xlsm", ".xltm", ".xlam", ".pptm", ".potm", ".ppsm", ".sldm",

            // Archive & Package Formats (High-risk if not scanned)
            ".iso", ".img", ".vhd", ".vhdx", ".pkg", ".deb", ".rpm"
        };

        public bool IsExtensionAllowed(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            var extension = Path.GetExtension(fileName);
            return !string.IsNullOrEmpty(extension) && !_notAllowedExtensions.Contains(extension);
        }
    }

}
