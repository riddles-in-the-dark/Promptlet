using Bodie.Promptlet.Domain.Exceptions;
using Bodie.Promptlet.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bodie.Promptlet.Domain.AggregatesModel.ComposedPrompletAggregate
{
    public class ComposedPromptlet : Entity, IAggregateRoot
    {
        public int ComposedPromptletId { get; private set; }
        public int ComposedPromptletVersion { get; private set; }
        public string ComposedPromptletName { get; private set; }
        public string? ComposedPromptletDescription { get; set; }
        public string? ComposedPromptletHeader { get; set; }
        public string? ComposedPromptletFooter { get; set; }

        private bool _isDraft;

        private readonly List<PromptletArtifact> _promptletArtifacts;
        public IReadOnlyCollection<PromptletArtifact> PromptletArtifacts => _promptletArtifacts;

        public static ComposedPromptlet NewComposedPromptlet()
        {
            return new ComposedPromptlet
            {
                _isDraft = true
            };
        }

        protected ComposedPromptlet()
        {
            _promptletArtifacts = new List<PromptletArtifact>();
            _isDraft = false;
        }

        public ComposedPromptlet(int composedPromptletId,
                                 int composedPromptletVersion,
                                 string composedPromptletName,
                                 string? composedPromptletDescription,
                                 string? composedPromptletHeader,
                                 string? composedPromptletFooter)
        {
            ComposedPromptletId = composedPromptletId;
            ComposedPromptletVersion = composedPromptletVersion;
            ComposedPromptletName = composedPromptletName;
            ComposedPromptletDescription = composedPromptletDescription;
            ComposedPromptletHeader = composedPromptletHeader;
            ComposedPromptletFooter = composedPromptletFooter;
        }
    }

    public class PromptletArtifact : Entity
    {
        private int? _promptletArtifactId;
        private int? _promptletArtifactVersion;
        private int? _promptletArtifactOrder;
        private string _promptletArtifactName;
        private string? _promptletArtifactDescription;
        private string? _promptletArtifactContent;
        private string? _variableStartDeliminator;
        private string? _variableEndDeliminator;

        public PromptletArtifact(int promptletArtifactId,
                                 int promptletArtifactVersion,
                                 int promptletArtifactOrder,
                                 string promptletArtifactName,
                                 string? promptletArtifactDescription,
                                 string? promptletArtifactContent,
                                 string? variableStartDeliminator,
                                 string? variableEndDeliminator)
        {
            _promptletArtifactId =  promptletArtifactId;
            _promptletArtifactVersion = promptletArtifactVersion;
            _promptletArtifactOrder = promptletArtifactOrder;
            _promptletArtifactName = promptletArtifactName;
            _promptletArtifactDescription = promptletArtifactDescription;
            _promptletArtifactContent = promptletArtifactContent;
            _variableStartDeliminator = variableStartDeliminator;
            _variableEndDeliminator = variableEndDeliminator;
        }

        protected PromptletArtifact() { }    
    }
}
