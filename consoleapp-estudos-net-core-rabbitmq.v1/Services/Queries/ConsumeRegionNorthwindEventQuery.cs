using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Services.Queries
{
    public class ConsumeRegionNorthwindEventQuery : IConsumeRegionNorthwindEventQuery
    {
        private IRabbitMqInfra _rabbitMqInfra;



        private readonly string _queueName;



        public ConsumeRegionNorthwindEventQuery(IRabbitMqInfra rabbitMqInfra)
        {
            _rabbitMqInfra = rabbitMqInfra;
            _queueName = RabbitMqConstants.RABBIT_MQ_INCLUDE_REGION_NORTHWIND_QUEUE;
        }



        public void Handle()
        {
            _rabbitMqInfra.ConsumeMessages(_queueName);
        }
    }
}
