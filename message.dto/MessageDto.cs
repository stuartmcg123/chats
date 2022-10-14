namespace message.dto
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public DateTime Created { get; set; }
    }
}