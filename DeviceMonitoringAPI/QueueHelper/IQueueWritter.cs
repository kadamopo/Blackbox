namespace QueueHelpers
{
    public interface IQueueWritter
    {
        void AddMessage(string messageBody);
    }
}