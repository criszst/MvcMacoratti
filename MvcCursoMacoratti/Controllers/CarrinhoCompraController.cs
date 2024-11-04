using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMacorattiLanchesMac.Models;
using MvcMacorattiLanchesMac.Repositories.Interfaces;
using MvcMacorattiLanchesMac.ViewModels;

namespace MvcMacorattiLanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraVM);
        }

        [Authorize]
        public IActionResult AdicionarItemCompra(int lancheID)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                     .FirstOrDefault(p => p.LancheId == lancheID);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AddCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoveItemCompra (int lancheID)
        {
            var lancheSelect = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheID);

            if (lancheSelect != null)
            {
                _carrinhoCompra.RemoveCarrinho(lancheSelect);
            }

            return RedirectToAction("Index");
        }
    }
}
