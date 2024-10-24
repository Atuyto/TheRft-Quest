using System;

[Serializable]
public class Message
{
    public string message;
    public string from;
    public string to;
    public bool see;

    public Message()
    {
        see = false;
    }

    public Message(string message, string from, string to)
    {
        this.message = message;
        this.from = from;
        this.to = to;
        this.see = false;
    }

    public override string ToString()
    {
        return $"From: {from}, To: {to}, Message: {message}, See: {see}";
    }
}
