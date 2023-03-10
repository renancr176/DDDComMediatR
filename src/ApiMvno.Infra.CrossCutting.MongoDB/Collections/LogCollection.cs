namespace ApiMvno.Infra.CrossCutting.MongoDB.Collections;

public class LogCollection
{
    public string Index { get; set; }
    public DateTime Date { get; set; }
    public object? Data { get; set; }

    public LogCollection()
    {
    }
}