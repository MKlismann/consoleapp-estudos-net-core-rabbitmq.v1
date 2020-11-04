using consoleapp_estudos_net_core_rabbitmq.v1.CrossCutting;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Resources;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Services.Queries
{
    public class GetRegionNorthwindEventQuery : IGetRegionNorthwindEventQuery
    {
        private IRabbitMqInfra _rabbitMqInfra;



        private readonly string _queueName;



        public GetRegionNorthwindEventQuery(IRabbitMqInfra rabbitMqInfra)
        {
            _rabbitMqInfra = rabbitMqInfra;
            _queueName = RabbitMqConstants.RABBIT_MQ_INCLUDE_REGION_NORTHWIND_QUEUE;
        }



        public void Handle()
        {
            var retrievedMessage = _rabbitMqInfra.GetMessage(_queueName);

            var formattedMessage = string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.MENSAGEM_RECUPERADA, retrievedMessage);
            WriteLogUtil.WriteLog(formattedMessage);
        }
    }
}
