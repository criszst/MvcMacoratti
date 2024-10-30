using MvcMacorattiLanchesMac.Context;
using MvcMacorattiLanchesMac.Models;
using MvcMacorattiLanchesMac.Repositories.Interfaces;

namespace MvcMacorattiLanchesMac.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoItens = _carrinhoCompra.CarrinhoCompraItems;

            foreach (var items in carrinhoItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = items.Quantidade,
                    LancheId = items.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = items.Lanche.Preco,
                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }

            _appDbContext.SaveChanges();
        }
    }
}
