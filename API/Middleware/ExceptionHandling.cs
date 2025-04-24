public class ExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandling> _logger;

    public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Logando a exceção
            _logger.LogError(ex, "Ocorreu um erro durante o processamento da requisição.");

            // Definindo uma resposta personalizada
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var response = new { message = "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde." };
            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
