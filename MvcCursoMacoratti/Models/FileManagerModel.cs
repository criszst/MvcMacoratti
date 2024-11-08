namespace MvcMacorattiLanchesMac.Models
{
    public class FileManagerModel
    {
        public FileInfo[] files { get; set; }

        public IFormFile IFormFile { get; set; }

        public List<IFormFile> IFormFiles { get; set; }
        
        public string PathImagesProduto { get; set; }
    }
}
