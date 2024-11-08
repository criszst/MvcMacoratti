using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.ViewModels
{
    public class PedidoLancheViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
