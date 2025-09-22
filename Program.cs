using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Very_Useful_Reasons;

namespace Very_Important_Cli_App;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var host = Host.CreateApplicationBuilder(args);
		host.Services
			.AddLogging();

		IHost app = host.Build();

		var lifeTime = app.Services.GetRequiredService<IHostApplicationLifetime>();
		CancellationToken token = lifeTime.ApplicationStopping;
		var logger = app.Services.GetRequiredService<ILogger<Program>>();

		string reason = await UsefulReasons.GetReason(logger, token);
		logger.LogInformation("{reason}", reason);

		await app.RunAsync(token);
	}
}
