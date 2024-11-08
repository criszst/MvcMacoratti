using MvcMacorattiLanchesMac.Context;
using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.Areas.Admin.Services
{
    public class GraficoVendasService
    {
        private readonly AppDbContext context;

        public GraficoVendasService(AppDbContext context)
        {
            this.context = context;
        }

        // grafico de vendas onde será mostrado os lanches mais vendidos
        // em si, ele mostrará os lanches mais vendidos no período de 360 dias
        public List<LancheGrafico> GetVendasLanches(int dias = 360)
        {
            // pegar os lanches mais vendidos no período de 360 dias (padrao, posso mudar a data ao declarar o objeto)
            var data = DateTime.Now.AddDays(-dias);

            // retornar o lanche mais vendido no período de 360 dias
            var lanches = (from pd in context.PedidoDetalhes
                           join l in context.Lanches on pd.LancheId equals l.LancheId
                           where pd.Pedido.PedidoEnviado >= data
                           group pd by new { pd.LancheId, l.Nome }
                             into g
                           select new
                           {
                               LancheNome = g.Key.Nome,
                               LanchesQuantidade = g.Sum(q => q.Quantidade),
                               LanchesValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                           });

            // criando uma lista com os lanches mais vendidos
            var lista = new List<LancheGrafico>();

            // percorrendo essa lista
            foreach(var item in lanches)
            {
                // e, por fim, adicionando os lanches mais vendidos na lista
                var lanche = new LancheGrafico();
                lanche.LancheNome = item.LancheNome;
                lanche.LanchesQuantidade = item.LanchesQuantidade;
                lanche.LanchesValorTotal = item.LanchesValorTotal;

                lista.Add(lanche);
            }

            return lista;
        }
    }
}
