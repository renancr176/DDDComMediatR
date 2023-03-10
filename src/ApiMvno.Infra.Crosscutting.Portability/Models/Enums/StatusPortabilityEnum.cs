namespace ApiMvno.Infra.CrossCutting.Portability.Models.Enums;

public enum StatusPortabilityEnum
{
    Unknown,
    Conflict,
    Pending,
    CancelInProgress,
    Cancelled,
    Active,
    Error,
}