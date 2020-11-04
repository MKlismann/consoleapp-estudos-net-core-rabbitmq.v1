using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Resources;
using System;
using System.Diagnostics;

namespace consoleapp_estudos_net_core_rabbitmq.v1.CrossCutting
{
    public static class EventViewerUtil
    {
        public static void ConfiguraEventViewer()
        {
            try
            {
                if (!EventLog.SourceExists(EventViewerConstants.EVENT_VIEWER_SOURCE))
                {
                    EventLog.CreateEventSource(EventViewerConstants.EVENT_VIEWER_SOURCE, EventViewerConstants.EVENT_VIEWER_LOG);
                }
            }
            catch (Exception exception)
            {
                var exceptionDetails = exception.InnerException == null
                    ? string.Empty
                    : $" - {exception.InnerException.Message}";

                var exceptionMessage = string.Concat(exception.Message, exceptionDetails);

                WriteLogUtil.WriteLog(string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.ERRO_CONFIGURACAO_EVENT_VIEWER, exceptionMessage));

                //Caso ocorra erro na criação da instância no Event Viewer, ignora a exceção; Impacto: não teremos LOGs gravados no Event Viewer.
            }
        }

        public static void WriteMessage(string message)
        {
            try
            {
                var eventLog = new EventLog
                {
                    Source = EventViewerConstants.EVENT_VIEWER_SOURCE
                };

                eventLog.WriteEntry(message);
            }
            catch (Exception)
            {
                //Caso ocorra erro na gravação de mensagem no Event Viewer, ignora a exceção; Impacto: não teremos LOGs gravados no Event Viewer.
            }
        }
    }
}
