using System.Globalization;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants
{
    public static class ApplicationConstants
    {
        public readonly static CultureInfo CULTURE_INFO_PT_BR = new CultureInfo("pt-BR");
        public const string FORMAT_DATE_TIME_LOG = "yyyy-MM-dd HH:mm:ss.fff";
    }
}
