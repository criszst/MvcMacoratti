using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using MvcMacorattiLanchesMac.Context;

namespace MvcMacorattiLanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }


        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // define uma sessao
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            // obtem um servico do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem o id do carrinho ( ou cria um ID se nao tiver um - usando o operador de coalescência nula ?? )
            string carrinhoID = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();


            // atribui o id do carrinho na sessao
            session.SetString("CarrinhoId", carrinhoID);


            // retorna o carrinho com o contexto e o ID atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoID
            };
        }


        public void AddCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                l => l.Lanche.LancheId == lanche.LancheId
                &&
                l.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }

            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }


        public void RemoveCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                l => l.Lanche.LancheId == lanche.LancheId
                &&
                l.CarrinhoCompraId == CarrinhoCompraId);

            //var qtdLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;

                    //qtdLocal = carrinhoCompraItem.Quantidade;
                }

                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();

            //return qtdLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(l => l.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            return _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

        }


    }
}