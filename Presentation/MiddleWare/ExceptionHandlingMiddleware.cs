using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Extentions;
using System.Net.Http;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var statusCode = HttpStatusCode.InternalServerError;
		var message = "An error occurred. Please try again later.";

		if (exception is KeyNotFoundException) // Example: Not Found Exception
		{
			statusCode = HttpStatusCode.NotFound;
			message = exception.Message;
		}
		// Add more custom exception handling as needed

		var response = new PetHealthCareResponse<object>(false, message, null);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)statusCode;
		return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
	}
}
