

using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<ExchangeRateService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


    




app.MapPost ("/exchange", (ExchangeRequest request, ExchangeRateService service) =>
{

    var taxa = service.Find(request.from, request.to);

    if (taxa == null)
    {
        return Results.BadRequest($"Cotação não encontrada para {request.from} > {request.to}");
    }
    

    var convertedAmount = request.amount * taxa.rate;


     return Results.Ok(new ExchangeResponse(request.from, request.to, request.amount, taxa.rate, convertedAmount));
});


        
    


app.Run();

public record ExchangeRequest(string from, string to,decimal amount);


public record ExchangeResponse(string from, string to, decimal amount, decimal rate, decimal convertedAmount);

public record ExchangeRate(string from, string to, decimal rate, DateTime Time, string source);


   

