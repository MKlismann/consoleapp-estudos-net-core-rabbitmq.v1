using consoleapp_estudos_net_core_rabbitmq.v1.CrossCutting;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Resources;
using consoleapp_estudos_net_core_rabbitmq.v1.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Services.Commands;
using consoleapp_estudos_net_core_rabbitmq.v1.Services.Queries;
using System;

namespace consoleapp_estudos_net_core_rabbitmq.v1
{
    public class Program
    {
        private static IRabbitMqInfra _rabbitMqInfra = new RabbitMqInfra();
        private static IIncludeRegionNorthwindEventCommand _includeRegionNorthwindEventCommand;
        private static IGetRegionNorthwindEventQuery _getRegionNorthwindEventQuery;
        private static IConsumeRegionNorthwindEventQuery _consumeRegionNorthwindEventQuery;



        private static DateTime _startProcessing;
        private static DateTime _finishProcessing;



        static void Main(string[] args)
        {
            try
            {
                WriteLogUtil.WriteLog(Messages.INICIO_EXECUCAO_PROGRAMA);
                _startProcessing = DateTime.Now;

                EventViewerUtil.ConfiguraEventViewer();

                /* TESTE DE INCLUSÃO DE MENSAGEM NA FILA */
                _includeRegionNorthwindEventCommand = new IncludeRegionNorthwindEventCommand(_rabbitMqInfra);
                _includeRegionNorthwindEventCommand.Handle();

                /* TESTE DE CONSUMO DE UMA MENSAGEM DA FILA */
                //_getRegionNorthwindEventQuery = new GetRegionNorthwindEventQuery(_rabbitMqInfra);
                //_getRegionNorthwindEventQuery.Handle();

                ///* TESTE DE CONSUMO DE TODAS AS MENSAGEM DA FILA */
                //_consumeRegionNorthwindEventQuery = new ConsumeRegionNorthwindEventQuery(_rabbitMqInfra);
                //_consumeRegionNorthwindEventQuery.Handle();
            }
            catch (Exception exception)
            {
                var exceptionDetails = exception.InnerException == null
                   ? string.Empty
                   : $" - {exception.InnerException.Message}";

                var exceptionMessage = string.Concat(exception.Message, exceptionDetails);

                var messageError = string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.ERRO_EXECUCAO_PROGRAMA, exceptionMessage);
                WriteLogUtil.WriteLog(messageError);
            }
            finally
            {
                _finishProcessing = DateTime.Now;

                var messageTimeExecution = string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.TEMPO_EXECUCAO_TOTAL, (_finishProcessing - _startProcessing));
                WriteLogUtil.WriteLog(messageTimeExecution);

                WriteLogUtil.WriteLog(Messages.TERMINO_EXECUCAO_PROGRAMA);
                WriteLogUtil.WriteLog(Messages.PRESSIONE_QUALQUER_TECLA_SAIR);
                Console.ReadKey();
            }
        }
    }
}
