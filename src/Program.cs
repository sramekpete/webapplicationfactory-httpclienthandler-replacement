namespace HttpClientHandlerReplacement;

using HttpClientHandlerReplacement.SendGrid;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Default HTTP client registration
        builder.Services
            .AddHttpClient()
            .ConfigureHttpClientDefaults(builder =>
                builder.ConfigureHttpClient(hc => hc.BaseAddress = new Uri("https://www.google.com"))
            );

        // Named HTTP client registration
        builder.Services
            .AddHttpClient<HttpClient>(HttpClientNames.Google)
            .ConfigureHttpClient(hc => hc.BaseAddress = new Uri("https://www.google.com"));

        // Typed  HTTP client registration
        builder.Services
            .AddHttpClient<ISendGridClient, SendGridClient>()
            .ConfigureHttpClient(hc => hc.BaseAddress = new Uri("https://www.google.com"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
