using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chat.Server.Messages;
using Chat.Server.People;
using Chat.Server.Subscriptions;
using Chat.Server.Users;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;

namespace Chat.Server
{
    public partial class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthenticationServices(services);

            services.AddCors();

            services.AddSingleton<InMemoryEventHandler>();
            services.AddSingleton<IEventSender>(sp => sp.GetRequiredService<InMemoryEventHandler>());
            services.AddSingleton<IEventSubscription>(sp => sp.GetRequiredService<InMemoryEventHandler>());

            services
                .AddRepositories()
                .AddDataLoaderRegistry()
                .AddGraphQL(
                    SchemaBuilder.New()
                        .AddQueryType(d => d.Name("Query"))
                        .AddType<PersionQueries>()
                        .AddMutationType(d => d.Name("Mutation"))
                        .AddType<PersonMutations>()
                        .AddType<UserMutations>()
                        .AddType<MessageMutations>()
                        .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddType<MessageSubscriptions>()
                        .AddType<PersonExtension>()
                        .AddType<MessageExtension>());

            services.AddQueryRequestInterceptor((context, builder, ct) =>
            {
                builder.AddProperty("currentUserEmail", "foo@bar.com");
                return Task.CompletedTask;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(o => o
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();

            app.UseWebSockets();

            app.UseGraphQL().UseVoyager();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
