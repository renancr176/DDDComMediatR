using ApiMvno.Domain.Attributes;

namespace ApiMvno.Domain.Enums;

public enum PhoneTypeEnum
{
    [NameForDatabase("Fixo")]
    LandLine,
    [NameForDatabase("Móvel")]
    Mobile
}