﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddHttpClient(
                "ChatClient",
                c => c.BaseAddress = new Uri("http://localhost:5000/"));
            builder.Services.AddChatClient();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}