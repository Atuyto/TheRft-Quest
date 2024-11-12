using System;

[Serializable]
public class SystemMessage
{
    public string title;
    public string code;
    
    public SystemMessage(string title, string code)
    {
        this.title = title;
        this.code = code;
    }

    public SystemMessage()
    {

    }

    public override string ToString()
    {
        return code;
    }
}
