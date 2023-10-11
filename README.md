![Frame 298](https://github.com/daniloopinheiro/AspNetv6YarpMicrosServices/assets/64677271/889e8353-3b6c-4df0-84bf-34d0a4ec4ded)

# Asp.Net v6 [Yarp](https://www.nuget.org/packages/Yarp.ReverseProxy) MicrosServices
local de construção, OS Linux (Ubuntu 20.04.6 LTS x86_64): sudo apt autoremove && sudo apt autoclean  && sudo apt update && sudo apt upgrade -y && sudo apt dist-upgrade -y

### *"Olá caro dev do dotNet, nessa pequena demonstração, você vera neste repositório o código fonte que lhe mostrará, após a execução, o pacote YARP, realizando uma solicitação via o pacote, com uma das apis intermediando os serviços"*

## Processo de Criação
*obs: instalar versão .net6*

```shell
$ dotnet new globaljson --sdk-version 6.x.x
```

## Etapa 1: Criando duas APIs

Criado dois projetos separados de API Web do ASP.NET para PrimeiroServico e SegundoServico. Use os seguintes comandos:

```shell
$ dotnet new webapi -o PrimeiroServico -f net6.0
$ dotnet new webapi -o SegundoServico -f net6.0
```

## Etapa 2: Instalação do Pacote YARP

Adicione os pacotes YARP NuGet aos dois projetos de APIs:

> PrimeiroServico
```shell
$ dotnet add package Yarp.ReverseProxy --version 2.0.1
$ dotnet add package Yarp.Telemetry.Consumption --version 2.0.1
```

> SegundoServico
```shell
$ dotnet add package Yarp.ReverseProxy --version 2.0.1
$ dotnet add package Yarp.Telemetry.Consumption --version 2.0.1
```

## Etapa 3: Configurar as APIs

Em cada arquivo Startup.cs do PrimeiroServico e SegundoServico, configure um endpoint simples:

> // Dentro do método Configure em Startup.cs para PrimeiroServico
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/primeiro", async context =>
    {
        await context.Response.WriteAsync("User Service");
    });
});
```

> // Dentro do método Configure em Startup.cs para SegundoServico
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/segundo", async context =>
    {
        await context.Response.WriteAsync("User Service");
    });
});
```

## Etapa 4: Criado Gateway de APIs

Criando um novo projeto de API Web ASP.NET para o API Gateway:

```shell
$ dotnet new webapi -o ApiGateway -f net6.0
```
## Etapa 5: Instalação do Pacote YARP para API Gateway

Adicione pacotes YARP NuGet ao projeto ApiGateway: 

```shell
$ dotnet add package Yarp.ReverseProxy --version 2.0.1
$ dotnet add package Yarp.Telemetry.Consumption --version 2.0.1
```

## Etapa 6: Configurando o API Gateway

Atualize o arquivo Startup.cs no projeto ApiGateway:

```csharp
var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
  .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

app.Run();

```

## Etapa 7: Configurando o YARP em appsettings.json

Criando um arquivo appsettings.json no projeto ApiGateway e configure o YARP:

```csharp
{
  "ReverseProxy": {
    "Clusters": {
      "user": {
        "Destinations": {
          "primeiroServico": { "Address": "https://localhost:5001" }
        }
      },
      "product": {
        "Destinations": {
          "segundoServico": { "Address": "https://localhost:5002" }
        }
      }
    },
    "Routes": [
      {
        "RouteId": "primeiraRoute",
        "ClusterId": "primeiro",
        "Match": { "Path": "/primeiro/{**catch-all}" }
      },
      {
        "RouteId": "segundaRoute",
        "ClusterId": "segundo",
        "Match": { "Path": "/segundo/{**catch-all}" }
      }
    ]
  }
}
```

## Etapa Final: Executando os serviços e o gateway

Execute cada microsserviço (PrimeiroServico e SegundoServico) e o API Gateway (ApiGateway). 

```csharp
$ dotnet run
```

Acesse o gateway em https://localhost:5000 e teste as rotas configuradas, como https://localhost:5000/primeiro e https://localhost:5000/segundo.

---
### Referências
---
- [Código fonte](https://github.com/daniloopinheiro/AspNetv6YarpMicrosServices)
- [Doc template webapi MSFT](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template)
- [Doc add package MSFT](https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet-add-package)
- [Doc reference MSFT](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference)

- [Doc YARP](https://microsoft.github.io/reverse-proxy/index.html)
- [Nuget - Yarp ReverseProxy](https://www.nuget.org/packages/Yarp.ReverseProxy)
- [Nuget - Telemetry Consumption](https://www.nuget.org/packages/Yarp.Telemetry.Consumption)
---
