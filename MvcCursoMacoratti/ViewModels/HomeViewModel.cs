using MvcMacorattiLanchesMac.Models;

namespace MvcMacorattiLanchesMac.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
