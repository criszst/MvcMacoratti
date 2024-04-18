using Microsoft.AspNetCore.Mvc;
using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.ViewModels
{
    public class CarrinhoCompraViewModel : Controller
    {
        public CarrinhoCompra CarrinhoCompra { get; set; }

        public decimal CarrinhoCompraTotal { get; set; }
    }
}
