using chat.shared;

namespace message.models;
public class Message:AuditTable
{
    public string Body { get; set; }
}
