using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MvcMacorattiLanchesMac.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Endereco {  get; set; }

        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.Add("href", "mailto:" + Endereco);
            output.Content.SetContent(Conteudo);
        }
    }
}
