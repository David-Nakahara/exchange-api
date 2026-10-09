

using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var taxas = new List<ExchangeRate>
    {
        new ExchangeRate("BRL", "EUR", 0.17m, DateTime.UtcNow, "Fonte Educacional"),
        new ExchangeRate("EUR", "BRL", 5.88m, DateTime.UtcNow, "Fonte Educacional")
    };


app.MapPost ("/exchange", (ExchangeRequest request) =>
{

    var taxa = taxas.FirstOrDefault(t=> t.from == request.from  &&  t.to == request.to);

    var convertedAmount = request.amount * taxa.rate;

    

    
    

    return new ExchangeResponse (request.from , request.to , request.amount, taxa.rate, convertedAmount  );
});


        
    


app.Run();

public record ExchangeRequest(string from, string to,decimal amount);


public record ExchangeResponse(string from, string to, decimal amount, decimal rate, decimal convertedAmount);

public record ExchangeRate(string from, string to, decimal rate, DateTime Time, string source);


   

