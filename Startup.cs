using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup {
  public Startup(IConfiguration configuration) {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  public void ConfigureServices(IServiceCollection services) {
    services.AddControllersWithViews().AddRazorRuntimeCompilation();

    services.Configure<RazorViewEngineOptions>(options => {
      options.ViewLocationFormats.Clear();
      options.ViewLocationFormats.Add("src/views/{1}/{0}.cshtml");
    });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    if (env.EnvironmentName == "Development") {
        app.UseDeveloperExceptionPage();
    } else {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseEndpoints(endpoints => {
      endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    });
  }
}