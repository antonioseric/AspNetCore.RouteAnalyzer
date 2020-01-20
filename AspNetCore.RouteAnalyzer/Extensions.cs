using AspNetCore.RouteAnalyzer.Inner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.RouteAnalyzer
{
    public static class RouteAnalyzerServiceCollectionExtensions
    {
        public static IServiceCollection AddRouteAnalyzer(this IServiceCollection services)
        {
            services.AddSingleton<IRouteAnalyzer, RouteAnalyzer.Inner.RouteAnalyzerImpl>();
            return services;
        }
    }

    public static class RouteAnalyzerRouteBuilderExtensions
    {
        public static string RouteAnalyzerUrlPath { get; private set; } = "";

        public static IRouteBuilder MapRouteAnalyzer(this IRouteBuilder routes, string routeAnalyzerUrlPath)
        {
            RouteAnalyzerUrlPath = routeAnalyzerUrlPath;
            routes.Routes.Add(new Router(routes.DefaultHandler, routeAnalyzerUrlPath));
            return routes;
        }

        public static IEndpointRouteBuilder MapRouteAnalyzer(this IEndpointRouteBuilder routes, string routeAnalyzerUrlPath = null)
        {
            RouteAnalyzerUrlPath = routeAnalyzerUrlPath ?? "/routes";
            routes.MapControllerRoute("routeAnalyzer", "{controller=RouteAnalyzer_Main}/{action=ShowAllRoutes}");
            routes.MapGet(RouteAnalyzerUrlPath, async context => context.Response.Redirect("RouteAnalyzer_Main/ShowAllRoutes"));
            return routes;
        }
    }
}
