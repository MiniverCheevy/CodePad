namespace CodePad.Server.Models;

public class Reply
{
    public bool IsOk { get; set; } = true;

    public string? Message { get; set; }

    public List<KeyValuePair<string, string>> Details { get; set; } = new();
}

public class Reply<T> : Reply
{
    public T? Data { get; set; }
}