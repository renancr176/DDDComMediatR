namespace ApiMvno.Domain.Attributes;

public class SubscriptionTypeAttribute : NameForDatabaseAttribute
{
    public int NumasyId { get; set; }

    public SubscriptionTypeAttribute(string name, int numasyId) 
        : base(name)
    {
        NumasyId = numasyId;
    }
}