using System.ComponentModel.DataAnnotations;

namespace MvcMacorattiLanchesMac.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }

        public decimal Preco { get; set; }

        public string ImagemUrl { get; set; }
        public string ImagemThumbUrl { get; set; }

        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }


        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
