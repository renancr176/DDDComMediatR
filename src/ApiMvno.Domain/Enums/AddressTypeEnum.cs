using ApiMvno.Domain.Attributes;

namespace ApiMvno.Domain.Enums
{
    public enum AddressTypeEnum
    {
        [NameForDatabase("Endereço de cobrança")]
        Billing,
        [NameForDatabase("Endereço de entrega")]
        Shipping
    }
}
