using chat.shared;

namespace message.models;
public class Message:AuditTable
{
    public string Body { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}
