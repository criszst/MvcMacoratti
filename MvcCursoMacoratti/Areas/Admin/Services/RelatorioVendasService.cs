using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using MvcMacorattiLanchesMac.Context;
using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;
        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate);
            }

            return await resultado
                .Include(l => l.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();
        }
    }
}
