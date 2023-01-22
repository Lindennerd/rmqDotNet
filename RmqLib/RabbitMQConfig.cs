namespace RmqLib
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string ExchangeName { get; set; } = null!;
        public string ExchangeType { get; set; } = null!;
    }
}