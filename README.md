# ASP.NET Core Web App with HTMX (Controllers)

This repository demonstrates how to build an interactive web application using **ASP.NET Core MVC** with **HTMX** to enhance the user experience by incorporating partial page updates, without relying on heavy JavaScript frameworks.

## Features:
- **ASP.NET Core MVC**: A lightweight, flexible web framework for building dynamic web applications.
- **HTMX Integration**: Seamless partial page updates using HTMX attributes (`hx-get`, `hx-post`, `hx-target`, `hx-swap`, etc.) to enable modern, AJAX-like interactions without needing full page reloads.
- **Custom Tag Helpers**: Dynamically render views using a custom Tag Helper powered by a `ViewLocatorService`, which allows for easy and flexible view management across your app.
- **Form Handling with Validation**: Built-in form handling, including server-side validation with real-time feedback using HTMX to update only parts of the page.
- **Error Handling**: Custom 404 and 500 error pages with redirection for cleaner error management.
- **Flexbox-Based Layout**: Ensures a responsive and modern user interface with a sticky footer and containerized content.

## Getting Started:
1. Clone the repository:
   ```bash
   git clone https://github.com/Shame625/ASP-.NET-Core-Web-App-with-HTMX-Controllers-.git
   ```
2. Install the required .NET packages:
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Key Concepts:
- **HTMX**: A lightweight library that enables modern front-end interactions using standard HTML, without relying on JavaScript frameworks like React or Angular.
- **Partial Views & Forms**: Submit forms asynchronously, validate inputs, and update sections of the page without full refreshes.
- **Error Handling**: Custom error views for 404 and 500 errors, ensuring smooth user experience even in failure scenarios.

## Technologies Used:
- **ASP.NET Core MVC**: The robust and flexible server-side web framework.
- **HTMX**: A minimal JavaScript framework for modern front-end interactions.
- **Fluent Validation**: For server-side validation of form inputs.
- **Bootstrap**: For styling and responsive design.

## License:
This project is licensed under the MIT License. Feel free to use, modify, and distribute it as per the license terms.

---

### Explanation of the BaseController:

```csharp
using Htmx;
using HTMXTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HTMXTest.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ViewOrPartial(object model)
        {
            string viewName = ControllerContext.ActionDescriptor.ActionName;

            return ViewOrPartial(viewName, model);
        }

        protected IActionResult ViewOrPartial(string viewName = null, object model = null)
        {
            if (viewName == null)
            {
                viewName = ControllerContext.ActionDescriptor.ActionName;
            }

            var _viewLocator = HttpContext.RequestServices.GetRequiredService<IViewLocatorService>();
            var resolvedViewName = _viewLocator.FindView(viewName);
            if (Request.IsHtmx())
            {
                return PartialView(resolvedViewName, model);
            }

            return View(resolvedViewName, model);
        }
    }
}
```

### Why is it nice to have?

The `BaseController` you’ve written provides a clean, reusable way to dynamically render views based on the context of the request, especially when dealing with regular view rendering or partial rendering (via HTMX). Here’s why it’s beneficial:

#### 1. **Automatic View Name Resolution**
   - **`ControllerContext.ActionDescriptor.ActionName`**: If no view name is provided, this code automatically uses the current action’s name as the view name, reducing boilerplate and making it easier to follow the convention over configuration pattern.

#### 2. **Dynamic View Lookup Using `ViewLocatorService`**
   - The call to `HttpContext.RequestServices.GetRequiredService<IViewLocatorService>()` allows you to dynamically find views using a centralized service. This gives flexibility in how views are organized and located, decoupling the view discovery logic from the controller logic.

#### 3. **HTMX Integration**
   - By checking `Request.IsHtmx()`, the controller can determine if the request was made via HTMX (an AJAX-like request for partial content updates). This allows the controller to return either a partial view (using `PartialView`) or a full view (using `View`), depending on the nature of the request.
   - This keeps the controller flexible, allowing the same action to handle both traditional full-page requests and dynamic partial updates.

#### 4. **Centralized Logic for Partial and Full Views**
   - Instead of having to write logic in every controller method to differentiate between full page views and partial views, this base controller centralizes that logic. This means that all controllers inheriting from `BaseController` can benefit from this functionality without rewriting the code.
   
#### 5. **Cleaner Controllers**
   - By moving this logic into the base controller, it keeps the child controllers (like `HomeController`) cleaner and easier to maintain. You can focus on the business logic of your controllers without worrying about how the views are being resolved or whether it’s a partial or full view request.

### Overall Benefits:
- **Reusability**: Common logic for view rendering is centralized, promoting reuse and cleaner controllers.
- **Flexibility**: Dynamically decides whether to return a full or partial view, based on the request type (HTMX or regular).
- **Convention Over Configuration**: Automatically resolves view names based on the current action when no specific view name is provided.
- **Separation of Concerns**: Delegates view lookup to a service (`ViewLocatorService`), making it easier to extend or modify how views are located without touching controller logic.

---

This setup is particularly powerful when building interactive web applications that need partial updates and full-page rendering from the same action, giving you flexibility and clean, maintainable code.


Special thanks to: Khalid Abuhakmeh for creating 
https://www.nuget.org/packages/Htmx 