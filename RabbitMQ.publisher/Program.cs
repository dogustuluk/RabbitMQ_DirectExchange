﻿using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://nxdranwu:n_3nr-xZlXx0NoCWuFP05gTqZfp7_hwK@sparrow.rmq.cloudamqp.com/nxdranwu ");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs-direct", durable: true, type: ExchangeType.Direct);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
             {
                 var message = $"log {x}";
                 var messageBody = Encoding.UTF8.GetBytes(message);

                 channel.BasicPublish("logs-direct", "", null, messageBody);

                 Console.WriteLine($"Mesaj Gönderilmiştir {message}");
             });

            Console.ReadLine();
        }
    }
}
