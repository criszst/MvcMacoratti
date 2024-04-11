using MvcMacorattiLanchesMac.Context;
using MvcMacorattiLanchesMac.Models;
using MvcMacorattiLanchesMac.Repositories.Interfaces;

namespace MvcMacorattiLanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        IEnumerable<Categoria> ICategoriaRepository.Categorias => _context.Categorias; 
    }
}
