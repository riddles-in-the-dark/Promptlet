namespace Promptlet.Api.Client
{
    public record ApiClientConfiguration
    {
        public string ApiBaseUrl { get; set; } = @"http://localhost:5049";
        public double HandlerLifetimeInSeconds { get; set; } = 5;
    }
}