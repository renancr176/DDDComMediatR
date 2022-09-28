namespace ApiMvno.Domain.Attributes;

public class LineTypeAttribute : Attribute
{
    public string Name { get; set; }
    public int NumasyId { get; set; }

    public LineTypeAttribute(string name, int numasyId)
    {
        Name = name;
        NumasyId = numasyId;
    }
}