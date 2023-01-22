using FluentValidation;

namespace RmqLib.Config
{
    internal class RabbitMQConfigValidation : AbstractValidator<RabbitMQConfig>
    {
        public RabbitMQConfigValidation()
        {
            _ = RuleFor(x => x.HostName).NotEmpty();
            _ = RuleFor(x => x.ConnectionString).NotEmpty();
            _ = RuleFor(x => x.ExchangeName).NotEmpty();
            _ = RuleFor(x => x.ExchangeType).NotEmpty();
        }
    }
}