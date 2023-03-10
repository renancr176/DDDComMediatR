namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

public class JscPagedResponse<TData> : JscBaseResponse<IEnumerable<TData>>
{
    public Page Page { get; set; }
}

public class Page
{
    public int Size { get; set; }
    public int TotalElements { get; set; }
    public int TotalPages { get; set; }
    public int Number { get; set; }
}
