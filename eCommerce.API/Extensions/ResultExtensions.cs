using eCommerce.API.Common;
using eCommerce.Core.Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller, string? createdActionName = null)
    {
        if (result.IsSuccess)
        {
            return result.StatusCode switch
            {
                StatusCodes.Status201Created => controller.CreatedAtAction(createdActionName, new { id = result.Value }, result.Value),
                _ => controller.Ok(result.Value)
            };
        }

        var problemDetails = new UnifiedProblemDetails
        {
            Status = result.StatusCode,
            Title = GetTitleForStatusCode(result.StatusCode),
            Detail = result.Error,
            Instance = controller.HttpContext.Request.Path,
            Type = $"https://httpstatuses.com/{result.StatusCode}"
        };

        if (result.StatusCode != StatusCodes.Status400BadRequest)
        {
            problemDetails.Errors = new List<string> { result.Error };
        }

        return controller.StatusCode(result.StatusCode, problemDetails);
    }

    public static IActionResult ValidationProblem(this ControllerBase controller, ValidationResult validationResult)
    {
        var problemDetails = new UnifiedProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Error",
            Instance = controller.HttpContext.Request.Path,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ValidationErrors = validationResult
                .Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                )
        };

        return controller.BadRequest(problemDetails);
    }

    private static string GetTitleForStatusCode(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Validation Error",
        StatusCodes.Status401Unauthorized => "Unauthorized",
        StatusCodes.Status403Forbidden => "Forbidden",
        StatusCodes.Status404NotFound => "Not Found",
        StatusCodes.Status409Conflict => "Conflict",
        _ => "Server Error"
    };
}
