using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IObservable<EventSubscription>>(s => GetTestSubscriptions());
            services.AddSingleton(typeof(IObserver<ServiceEvent>), typeof(Demuxer));
        }

        private static IObservable<EventSubscription> GetTestSubscriptions()
        {
            return Observable.Create<EventSubscription>(o =>
            {
                o.OnNext(new EventSubscription()
                {
                    Endpoint = "http://localhost:8080/api/sink",
                    EventType = "createsomething/v1.0"
                });
                o.OnNext(new EventSubscription()
                {
                    Endpoint = "http://localhost:8080/api/sink",
                    EventType = "publishedsomething/v1.0"
                });
                o.OnCompleted();
                return Disposable.Create(() => { Console.WriteLine("Completed"); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
