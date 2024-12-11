
using QED.Models;
using QED.Modules;
using QED.Modules.Calculator;
using Swashbuckle.AspNetCore.Annotations;

namespace QED.Modules.Calculator
{
    public class CalculatorModule : IModule
    {
        private const string SwaggerTag = "Calculator";

        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            return services;
        }
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/add/{a}/{b}", (string a, string b) => Add.Handle(a,b))
            .WithTags(SwaggerTag)
            .WithMetadata(new SwaggerOperationAttribute(Add.Summary))
            .Produces<SumResponse>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest);

            return endpoints;
        }
    }
}
