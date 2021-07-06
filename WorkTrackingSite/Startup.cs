using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkTrackingSite.Attributes;
using WorkTrackingSite.Context;

namespace WorkTrackingSite
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IConfiguration config)
        {
            AppConfiguration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 1000000000;
            });

            services.AddControllers();

            services.AddMvc();
            #region _
            //services.Configure<FormOptions>(opt =>
            //{
            //    opt.MemoryBufferThreshold = 1000000000;
            //});

            //services.AddRazorPages(opt =>
            //{
            //    opt.Conventions.AddPageApplicationModelConvention("/UploadPrinterDriver",
            //        model => 
            //        {
            //            model.Filters.Add(new DisableFormValueModelBindingAttribute());
            //        });
            //});
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            // Добавление возможности использования статических файлов
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Index}/{Action=Index}");
            });
        }
    }
}
