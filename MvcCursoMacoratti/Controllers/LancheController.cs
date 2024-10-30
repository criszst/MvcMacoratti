using Microsoft.AspNetCore.Mvc;
using MvcMacorattiLanchesMac.Models;
using MvcMacorattiLanchesMac.Repositories.Interfaces;
using MvcMacorattiLanchesMac.ViewModel;

namespace MvcMacorattiLanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;


            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {

                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(l => l.Nome);

                categoriaAtual = categoria;
            }

            var lanchesListModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual,
            };

            return View(lanchesListModel);
        }

        public IActionResult Details(int lancheID)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheID);
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;

            string categoriaAtual = string.Empty;


            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }

            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any()) categoriaAtual = "Lanches";

                else categoriaAtual = "Nenhum lanche foi encontrado";
                
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual,
            });
        }
    }
}
