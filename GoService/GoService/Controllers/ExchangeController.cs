using GoService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Text;

namespace GoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController: ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary= "Exchange Type is an Enum", Description = "<h2>ExchangeType Values :</h2> <hr><br> <b>DOLAR : 0</b> </br><b>EURO : 1</b><br><b>STERLİN : 2</b></br><b>GRAM ALTIN : 3")]
        public bool Insert([FromBody]Exchange exchange)
        {
            try
            {
                exchange.ExchangeName = exchange.ExchangeType.ToString();
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "rabbitmq",
                    Password = "rabbitmq",
                    Port = 1881,
                    VirtualHost = "/",
                };

                using(var connection = factory.CreateConnection())
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "product",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null
                        );

                    var productData = JsonConvert.SerializeObject(exchange);
                    var body = Encoding.UTF8.GetBytes(productData);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "product",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($"{exchange.Name} is send to queue");
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        
    }
}
