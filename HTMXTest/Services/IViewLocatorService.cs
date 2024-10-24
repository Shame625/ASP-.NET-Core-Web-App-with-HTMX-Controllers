namespace HTMXTest.Services
{
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;

    public interface IViewLocatorService
    {
        string FindView(string viewName);
    }

    public class ViewLocatorService : IViewLocatorService
    {
        private readonly ICompositeViewEngine _viewEngine;

        // Static readonly to cache controller names and search locations forever
        private static readonly List<string> _controllerNamesCache;

        static ViewLocatorService()
        {
            // Cache all controller names in the application
            _controllerNamesCache = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => type.Name.Replace("Controller", ""))
                .ToList();
        }

        public ViewLocatorService(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public string FindView(string viewName)
        {
            var searchLocations = new List<string>();

            // Add search paths for each controller from the cache
            foreach (var controllerName in _controllerNamesCache)
            {
                searchLocations.Add($"~/Views/{controllerName}/{viewName}.cshtml");
            }

            // Always search in the Shared folder
            searchLocations.Add($"~/Views/Shared/{viewName}.cshtml");

            // Try to find the view in the specified locations
            foreach (var location in searchLocations)
            {
                var result = _viewEngine.GetView(executingFilePath: null, viewPath: location, isMainPage: false);
                if (result.Success)
                {
                    return location;
                }
            }

            throw new InvalidOperationException($"The view '{viewName}' was not found in the search locations.");
        }
    }

}
