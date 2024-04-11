using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria>  Categorias { get; }
    }
}
