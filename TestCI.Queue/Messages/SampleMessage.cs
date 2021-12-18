namespace TestCI.Queue.Messages
{
    public class SampleMessage : BaseMessage
    {
        public SampleMessage() : base(Priority.High) { }
    }
}
