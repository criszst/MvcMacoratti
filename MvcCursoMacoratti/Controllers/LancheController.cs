using Microsoft.AspNetCore.Mvc;
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

        public IActionResult List()
        {
            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);
            var lanchesListModel = new LancheListViewModel();
            lanchesListModel.Lanches = _lancheRepository.Lanches;
            lanchesListModel.CategoriaAtual = "Categoria Atual";

            return View(lanchesListModel);
        }
    }
}
