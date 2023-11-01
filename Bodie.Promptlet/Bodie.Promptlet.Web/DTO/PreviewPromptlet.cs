using Bodie.Promptlet.Infrastructure.Models;

namespace Bodie.Promptlet.Web.DTO
{
    public class PreviewPromptlet : ComposedPromptlet
    {
        public string RawBodyString { get; set; }
        public string PreviewString { get; set; }
        public string Variables { get; set; }
    }
}
