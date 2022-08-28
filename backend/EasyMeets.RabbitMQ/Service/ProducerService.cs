﻿using EasyMeets.RabbitMQ.Interface;
using EasyMeets.RabbitMQ.Settings;
using RabbitMQ.Client;
using System.Text;

namespace EasyMeets.RabbitMQ.Service
{
    public class ProducerService : IProducerService
    {
        private readonly IConnection _connection;
        private readonly ProducerSettings _producerSettings;

        public ProducerService(IConnection connection, ProducerSettings producerSettings)
        {
            _connection = connection;
            _producerSettings = producerSettings;
        }

        public void Send(string message, string? type)
        {
            using var channel = _connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            if (!string.IsNullOrEmpty(type))
            {
                properties.Type = type;
            }

            channel.ExchangeDeclare(_producerSettings.ExchangeName, _producerSettings.ExchangeType);

            if (!string.IsNullOrEmpty(_producerSettings.QueueName))
            {
                channel.QueueBind(
                    _producerSettings.QueueName,
                    _producerSettings.ExchangeName,
                    _producerSettings.RoutingKey);
            }

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                new PublicationAddress(
                    _producerSettings.ExchangeType,
                    _producerSettings.ExchangeName,
                    _producerSettings.RoutingKey),
                properties,
                body);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Dispose();
            }
        }
    }
}
