using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Services.Commands
{
    public class IncludeRegionNorthwindEventCommand : IIncludeRegionNorthwindEventCommand
    {
        private IRabbitMqInfra _rabbitMqInfra;



        private readonly string _queueName;



        public IncludeRegionNorthwindEventCommand(IRabbitMqInfra rabbitMqInfra)
        {
            _rabbitMqInfra = rabbitMqInfra;
            _queueName = RabbitMqConstants.RABBIT_MQ_INCLUDE_REGION_NORTHWIND_QUEUE;
        }



        public void Handle()
        {
            for (int index = 1; index <= 10; index++)
            {
                var sendMessage = new IncludeRegionNorthwindEvent(
                    index.ToString(ApplicationConstants.CULTURE_INFO_PT_BR),
                    $"Região-{index}");

                _rabbitMqInfra.PublishMessage<IncludeRegionNorthwindEvent>(sendMessage, _queueName);
            }
        }
    }
}
