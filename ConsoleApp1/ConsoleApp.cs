using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            ConsoleApp ca = new ConsoleApp();

            int index = 200;

            var response = ca.sendMessage("test", index);

            if (response.IsCompleted)
                Console.WriteLine(ca.reciveMessage(index));
            
        }

        private async Task<List<ProduceResponse>> sendMessage(string payload,  int index)
        {
            Uri uri = new Uri("http://localhost:9092");

            string topic = "chat-message";

            KafkaNet.Protocol.Message msg = new KafkaNet.Protocol.Message(payload.Trim());

            var options = new KafkaOptions(uri);

            var router = new BrokerRouter(options);

            var client = new Producer(router);

            msg.Meta = new KafkaNet.Protocol.MessageMetadata()
            {
                Offset = index
            };

            var result = await client.SendMessageAsync(topic, new List<Message> { msg });

            return result;
        }
        public string reciveMessage( int index)
        {
            Uri uri = new Uri("http://localhost:9092");

            string topicName = "chat-message";

            var options = new KafkaOptions(uri);

            var brokerRouter = new BrokerRouter(options);

            var consumer = new Consumer(new ConsumerOptions(topicName, brokerRouter));

            string message = "";

            foreach (var msg in consumer.Consume())
            {
                if (msg.Meta.Offset == index)
                    message = Encoding.UTF8.GetString(msg.Value);
            }
                       
            return message;
        }

    }
}
