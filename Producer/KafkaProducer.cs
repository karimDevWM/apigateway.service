using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace apigateway.service.Producer
{
    public class KafkaProducer
    {
        private ProducerConfig? _config;

        public KafkaProducer(ProducerConfig? config)
        {
            _config = config;
        }

        public void ProduceMessage(string topic, string message)
        {
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    var result = producer.ProduceAsync(
                        topic, new Message<Null, string> { Value = message }
                    )
                    .GetAwaiter()
                    .GetResult();
                    Console.WriteLine($"Message has been send {result.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException!.Message);
                throw;
            }
        }
    }
}