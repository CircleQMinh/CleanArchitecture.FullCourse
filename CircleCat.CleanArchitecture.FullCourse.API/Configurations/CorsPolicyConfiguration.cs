namespace CircleCat.CleanArchitecture.FullCourse.API.Configurations
{
    public static class CorsPolicyConfiguration
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => {
                options.AddPolicy("AppCorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                //options.AddPolicy("StrictPolicy",
                //policy =>
                //{
                //    policy.WithOrigins("http://example.com",
                //                        "http://www.contoso.com")
                //                        .AllowAnyHeader()
                //                        .AllowAnyMethod();
                //});
            });
            return services;
        }
    }
}
