using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService
{
    public class ProducerWrapper
    {
        private string _topicName;
        private IProducer<string, string> _producer;
        private ProducerConfig _config;
        private static readonly Random rand = new Random();

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            _topicName = topicName;
            _config = config;
            _producer = new ProducerBuilder<string, string>(_config).Build();
        }
        public async Task userAdded(string user)
        {
            var dr = await _producer.ProduceAsync(_topicName, new Message<string, string>()
            {
                Key = rand.Next(5).ToString(),
                Value = user
            });
            Console.WriteLine($"KAFKA => Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            return;
        }
        public async Task sendLogs(string logs)
        {
            var dr = await _producer.ProduceAsync(_topicName, new Message<string, string>()
            {
                Key = rand.Next(5).ToString(),
                Value = logs
            });
            Console.WriteLine($"KAFKA => Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            return;
        }
    }
}
