using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using System;

namespace consoleapp_estudos_net_core_rabbitmq.v1.CrossCutting
{
    public static class WriteLogUtil
    {
        public static void WriteLog(string message, bool includeDateTime = true,  bool writeInEventViewer = true)
        {
            var formattedMessage = message;
            if (includeDateTime)
            {
                formattedMessage = $"{DateTime.Now.ToString(ApplicationConstants.FORMAT_DATE_TIME_LOG, ApplicationConstants.CULTURE_INFO_PT_BR)} - {message}";
            }


            Console.WriteLine(formattedMessage);

            if (writeInEventViewer)
            {
                EventViewerUtil.WriteMessage(formattedMessage);
            }
        }
    }
}
