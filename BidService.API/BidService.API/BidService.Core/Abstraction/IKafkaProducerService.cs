namespace BidService.API.Abstraction
{
    public interface IKafkaProducerService
    {
        
            Task ProduceAsync(string key, string value);

        
    }
}
