namespace ApiMvno.Application.Attributes;

public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
{
    public string ErrorCode { get; set; }
}