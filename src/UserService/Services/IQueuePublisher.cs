namespace UserService.Services
{
    public interface IQueuePublisher
    {
        void PublishMessage(string integrationEvent, string eventData);
    }
}
