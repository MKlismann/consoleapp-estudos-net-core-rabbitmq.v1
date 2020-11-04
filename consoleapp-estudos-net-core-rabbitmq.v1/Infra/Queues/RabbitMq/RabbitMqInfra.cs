using consoleapp_estudos_net_core_rabbitmq.v1.CrossCutting;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Constants;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using consoleapp_estudos_net_core_rabbitmq.v1.Domain.Resources;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace consoleapp_estudos_net_core_rabbitmq.v1.Infra.Queues.RabbitMq
{
    public class RabbitMqInfra : IRabbitMqInfra
    {
        private IConnection _connection { get; set; }


        public void PublishMessage<T>(T message, string queueName)
        {
            if (_connection == null
                || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            using (_connection)
            {
                QueueDeclare(queueName);

                using (var channel = _connection.CreateModel())
                {
                    var exchange = string.Empty;

                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    var serializedMessage = JsonConvert.SerializeObject(message);
                    channel.BasicPublish(exchange, queueName, basicProperties, Encoding.UTF8.GetBytes(serializedMessage));
                }
            }
        }

        public string GetMessage(string queueName)
        {
            if (_connection == null
                || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            BasicGetResult message;

            using (_connection)
            {
                QueueDeclare(queueName);

                using (var channel = _connection.CreateModel())
                {
                    message = channel.BasicGet(queueName, true);
                }
            }

            return (message != null)
                ? Encoding.UTF8.GetString(message.Body.ToArray())
                : null;
        }

        public void ConsumeMessages(string queueName)
        {
            if (_connection == null
                || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            QueueDeclare(queueName);

            var channel = _connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var message = eventArgs.Body;
                if (!message.IsEmpty)
                {
                    var retrievedMessage = Encoding.UTF8.GetString(message.ToArray());

                    var formattedMessage = string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.MENSAGEM_RECUPERADA, retrievedMessage);
                    WriteLogUtil.WriteLog(formattedMessage);

                    channel.BasicAck(eventArgs.DeliveryTag, true);
                }
            };

            channel.BasicConsume(queueName, false, consumer);
        }

        private IConnection CreateConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = RabbitMqConstants.RABBIT_MQ_HOST_NAME,
                UserName = RabbitMqConstants.RABBIT_MQ_USER_NAME,
                Password = RabbitMqConstants.RABBIT_MQ_PASSWORD
            };

            return connectionFactory.CreateConnection();
        }

        private QueueDeclareOk QueueDeclare(string queueName)
        {
            QueueDeclareOk queueDeclared;

            using (var channel = _connection.CreateModel())
            {
                queueDeclared = channel.QueueDeclare(queueName, false, false, false, null);
            }

            return queueDeclared;
        }
    }
}
