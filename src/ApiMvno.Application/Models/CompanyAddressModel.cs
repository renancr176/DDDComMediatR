namespace ApiMvno.Application.Models
{
    public class CompanyAddressModel : EntityModel
    {
        public Guid CompanyId { get; set; }
        public Guid AddressId { get; set; }
        public AddressModel Address { get; set; }
    }
}
