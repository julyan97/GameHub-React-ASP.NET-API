Add Authentication/Authorization

1. Configuring services
```c#
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy =>
                      {
                          policy
                          .WithOrigins("http://localhost:3000", "https://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});
...

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = false
    };
});

builder.Services.AddDefaultIdentity<IdentityUser>(op =>
{
    op.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<GameHubDbContext>();
....

app.UseAuthentication();
app.UseAuthorization();
```

2. Custom Middleware for appending Authentication header
```c#
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

//Custom middlewares
app.ConfigureAuthenticationByJwtBearer();
```

3. Configure SameSitePolicy
```c#
app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.Always,
        HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
        MinimumSameSitePolicy = SameSiteMode.None,
    });
```

4. Configure Cors
```c#
//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy =>
                      {
                          policy
                          .WithOrigins("http://localhost:3000", "https://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});
...

app.UseCors("CORS");

```


