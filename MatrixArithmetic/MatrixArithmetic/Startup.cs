namespace MatrixArithmetic
{
    using MatrixArithmetic.Services.Implementations;
    using MatrixArithmetic.Services.Interfaces;
    using Microsoft.AspNetCore.Components.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMatrixService, MatrixService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
