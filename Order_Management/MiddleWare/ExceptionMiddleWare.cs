﻿using Order_Management.HandlingErrors;
using System.Net;
using System.Text.Json;

namespace Order_Management.MiddleWare
{
    public class ExceptionMiddleWare
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message); //for develop

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500


                var respnse = _env.IsDevelopment() ?
                    new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                  : new ExceptionResponse((int)HttpStatusCode.InternalServerError);


                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };


                var json = JsonSerializer.Serialize(respnse, option);

                await httpContext.Response.WriteAsync(json);

            }

        }


    }
}
