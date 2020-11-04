using RabbitMQ.Client;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq
{
    public interface IRabbitMqInfra
    {
        void PublishMessage<T>(T message, string queueName);

        string GetMessage(string queueName);

        void ConsumeMessages(string queueName);
    }
}
