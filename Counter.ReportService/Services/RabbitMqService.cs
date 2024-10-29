using Counter.Shared.DTOs;
using Counter.Shared.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Counter.ReportService.Services
{
    public class RabbitMqService : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService(string hostName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "report_updates",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void SendMessage(ReportRequestDTO report)
        {
            var message = JsonSerializer.Serialize(report);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                routingKey: "report_updates",
                basicProperties: null,
                body: body);
        }

        public void StartListening(IReportService reportService)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var reportData = JsonSerializer.Deserialize<ReportRequestDTO>(message);

                if (reportData != null)
                {
                    reportData.Durum = RaporDurumu.Tamamlandi;
                    await reportService.UpdateReportStatus(reportData.UUID, reportData.Durum);
                }
            };

            _channel.BasicConsume(queue: "report_updates",
                autoAck: true,
                consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
