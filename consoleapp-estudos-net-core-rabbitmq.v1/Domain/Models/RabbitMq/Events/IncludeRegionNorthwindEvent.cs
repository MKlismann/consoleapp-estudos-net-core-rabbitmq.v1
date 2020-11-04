namespace consoleapp_estudos_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events
{
    public class IncludeRegionNorthwindEvent : EventBase
    {
        public string TransactionId { get; }
        public string RegionDescription { get; }



        public IncludeRegionNorthwindEvent(string transactionId, string description)
        {
            TransactionId = transactionId;
            RegionDescription = description;
        }
    }
}
