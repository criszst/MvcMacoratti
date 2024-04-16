using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.ViewModel
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
