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
            //else
            //{
            //    if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
            //    {
            //        lanches = _lancheRepository.Lanches
            //            // tambem posso colocar o Equals("Normal") em vez de ==
            //            .Where(l => l.Categoria.CategoriaNome == "Normal")
            //            .OrderBy(l => l.Nome);
            //    } 
            //    else
            //    {
            //        lanches = _lancheRepository.Lanches
            //            .Where(l => l.Categoria.CategoriaNome == "Natural")
            //            .OrderBy(l => l.Nome);
            //    }


                
            //        categoriaAtual = categoria;
                
            //}

            var lanchesListModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual,
            };

            return View(lanchesListModel);
        }
    }
}
