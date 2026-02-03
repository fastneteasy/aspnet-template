
using NLog;
using NLog.Web;

namespace AspNetTemplate.WebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");


            try
            {
                logger.Debug("init main");
                var builder = WebApplication.CreateBuilder(args);
                var app = builder.ConfigureServices().Build();
                app.ConfigurePipeline();
                await app.RunAsync();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

    }
}
