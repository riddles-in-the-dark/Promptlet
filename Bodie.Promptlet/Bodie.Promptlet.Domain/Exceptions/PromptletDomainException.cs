namespace Bodie.Promptlet.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class PromptletDomainException : Exception
    {
        public PromptletDomainException()
        { }

        public PromptletDomainException(string message)
            : base(message)
        { }

        public PromptletDomainException(string message,
                                       Exception innerException)
            : base(message, innerException)
        { }
    }
}