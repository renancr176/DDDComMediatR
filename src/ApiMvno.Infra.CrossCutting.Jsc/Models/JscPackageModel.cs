namespace ApiMvno.Infra.CrossCutting.Jsc.Models;

public class JscPackageModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public object PricePlan { get; set; }
    public object PackagePrices { get; set; }
    public string BillingType { get; set; }
    public string Category { get; set; }
    public string PkgName { get; set; }
    public string Type { get; set; }
    public DateTime PublishData { get; set; }
    public bool Published { get; set; }
    public string Scope { get; set; }
    public IEnumerable<string> Links { get; set; }
}