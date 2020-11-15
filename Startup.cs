using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace webapp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static string content = "hello2";
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(content);
                });
                endpoints.MapPost("/", async context =>
                {
                    var body = context.Request.Body;
                    var header = context.Request.Headers;
                    var headerstring = JsonConvert.SerializeObject(header);
                    byte[] bytes = new byte[2048*1024];
                     var off=   await body.ReadAsync(bytes, 0, 2048*1024);
                    content = headerstring + "     ";
                    content+= System.Text.Encoding.UTF8.GetString(bytes,0,off);
                    content += "     " +DateTime.Now.ToString();
                    await context.Response.WriteAsync(content);
                });
            });
        }
    }
}
