using System.Collections.Generic;

namespace AspNetCore.RouteAnalyzer
{
    public interface IRouteAnalyzer
    {
        IEnumerable<RouteInformation> GetAllRouteInformations();
    }
}
