using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminImagesController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public AdminImagesController(IWebHostEnvironment hostingEnviroment, IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnviroment = hostingEnviroment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }



        // provavelmente eu vou voltar aqui (no futuro) e nao vou entender nada, ent eh melhor deixar previos comentários

        // o método UploadFiles é responsável por lidar com o upload de múltiplos arquivos para o servidor
        // ele verifica se os arquivos foram selecionados corretamente e se o tamanho total dos arquivos não excede o
        // limite permitido (10 arquivos, no caso)
        // se passar nesssa verificacao, ele salva esses arquivos no servidor e retorna uma mensagem de sucesso

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excede o permitido (10)";
                return View(ViewData);
            }

            // calcula o tamanho total dos arquivos
            long size = files.Sum(f => f.Length);

            // pega o caminho do servidor
            var filesPathsName = new List<string>();


            // define o caminho onde os arquivos serao salvos
            var filePath = Path.Combine(_hostingEnviroment.WebRootPath, 
                _myConfig.NomePastaImagensProdutos);


            // percorre todos os arquivos e se realmente forem imagens (ou gif ne), copia para o servidor
            foreach (var file in files)
            {
                if (file.FileName.Contains(".jpg") || 
                    file.FileName.Contains(".png") ||
                    file.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", file.FileName);

                    filesPathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
               
            }

            // retorna uma mensagem de sucesso
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, o total de {size} bytes";

            // retorna o caminho dos arquivos
            ViewBag.Arquivos = filesPathsName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnviroment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhuma imagem encontrada em {userImagesPath}";
                
            }

            model.files = files;

            return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
            string _imagemDelete = Path.Combine(_hostingEnviroment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDelete)))
            {
                System.IO.File.Delete(_imagemDelete);

                ViewData["Deletado"] = $"Arquivo {fname} deletado com sucesso";
            }

            return View("index");
        }
    }
}
