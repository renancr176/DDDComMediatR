namespace ApiMvno.Domain.Attributes;

public class NameForDatabaseAttribute : Attribute
{
    public string Name { get; set; }

    public NameForDatabaseAttribute(string name)
    {
        Name = name;
    }
}