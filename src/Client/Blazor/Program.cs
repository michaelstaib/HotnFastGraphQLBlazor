﻿using Internal;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Client.Extensions;
using Client.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddHttpClient(
                "ChatClient",
                (services, client) =>
                {
                    var token = services.GetRequiredService<ITokenStore>().GetToken();
                    
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    client.AddBearerToken(token);
                });
            builder.Services.AddChatClient();
            builder.Services.AddServices();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
