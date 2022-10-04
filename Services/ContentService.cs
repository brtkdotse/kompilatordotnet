using Markdig;
using System.Reflection;

namespace Kompilator.Services
{
    public class ContentService
    {
        private string _basePath;

        public ContentService()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            _basePath = System.IO.Path.GetDirectoryName(exePath);
        }

        public async Task<string> GetContentAsync(string id)
        {
            string path = Path.Combine(_basePath, "Markdown", id + ".md");
            if (!File.Exists(path))
                return string.Empty;

            string mdText = await File.ReadAllTextAsync(path);

            string html = Markdown.ToHtml(mdText);

            return html;
        }
    }
}
