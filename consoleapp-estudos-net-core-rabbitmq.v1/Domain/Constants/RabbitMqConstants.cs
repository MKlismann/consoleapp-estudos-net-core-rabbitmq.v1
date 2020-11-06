using System;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants
{
    public static class RabbitMqConstants
    {
        public const string RABBIT_MQ_HOST_NAME = "localhost";
        public readonly static Uri RABBIT_MQ_CUSTOM_URI_AMQP = new Uri("amqp://guest:guest@localhost:5672");

        public const string RABBIT_MQ_INCLUDE_REGION_NORTHWIND_QUEUE = "include.region.northwind.queue";
    }
}
