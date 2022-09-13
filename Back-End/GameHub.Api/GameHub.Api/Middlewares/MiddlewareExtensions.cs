namespace GameHub.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureAuthenticationByJwtBearer(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies["jwt"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });
        }
    }
}
