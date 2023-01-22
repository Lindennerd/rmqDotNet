using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RmqLib.Config;

namespace RmqLib
{
    public static class SetupRabbitMQ
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = GetRabbitMQConfig(configuration);
            services.AddSingleton(settings);
            services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
            {
                HostName = settings.HostName,
                Uri = new System.Uri(settings.ConnectionString),
                DispatchConsumersAsync = true
            });

            services.AddSingleton<ModelFactory>();
            services.AddSingleton(sp => sp.GetRequiredService<ModelFactory>().CreateChannel());

            return services;
        }

        private static RabbitMQConfig GetRabbitMQConfig(IConfiguration configuration)
        {
            var configSection = configuration.GetSection("RabbitMQ");
            if (configSection == null)
            {
                throw new System.Exception("RabbitMQ configuration section not found");
            }

            var settings = new RabbitMQConfig();
            configSection.Bind(settings);

            var settingsValidator = new RabbitMQConfigValidation();
            var validationResult = settingsValidator.Validate(settings);

            if (!validationResult.IsValid)
            {
                var messages = new System.Text.StringBuilder();
                foreach (var error in validationResult.Errors)
                {
                    messages.AppendLine(error.ErrorMessage);
                }

                throw new System.Exception(messages.ToString());
            }

            return settings;
        }
    }
}