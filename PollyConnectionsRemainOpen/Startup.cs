using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using PollyConnectionsRemainOpen.HttpClients;
using System;

namespace PollyConnectionsRemainOpen
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddTransient<ITestProcessor, TestProcessor>();

			String baseUrl = "http://localhost:5450/";
			// HttpClient without Polly
			services.AddHttpClient<INoPollyHttpClient, ZeHttpClient>(x => x.BaseAddress = new Uri(baseUrl));

			// HttpClient with Polly
			services.AddHttpClient<IPollyHttpClient, ZeHttpClient>(x => x.BaseAddress = new Uri(baseUrl))
					.AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(2, (retryAttempt) => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt))));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}