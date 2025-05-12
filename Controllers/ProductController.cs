using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using apigateway.service.DTOs;
using apigateway.service.Producer;
using Microsoft.AspNetCore.Mvc;

namespace apigateway.service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly string TopicName = Environment.GetEnvironmentVariable("Kafka_Topic_Name")!;
        private readonly KafkaProducer _kafkaProducer;

        public ProductController(ILogger<ProductController> logger, KafkaProducer kafkaProducer)
        {
            _logger = logger;
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        public ActionResult PostProduct(CreateProductDto createProductDto)
        {
            var productJson = JsonSerializer.Serialize(createProductDto);
            _kafkaProducer?.ProduceMessage(TopicName, productJson);

            return Ok("product Added");
        }
    }
}