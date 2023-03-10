using ApiMvno.Infra.CrossCutting.BackServiceApi.Enums;


namespace ApiMvno.Infra.CrossCutting.BackServiceApi.Models.Requests
{
    public class CreateCustomerRequest : BackServiceBaseRequest
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string LandLinePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; }

        public CreateCustomerRequest(string requestId, Guid id, Guid companyId, CustomerTypeEnum customerType,
            string document, string email, string landLinePhone, string mobilePhone, string name, DateTime birthdate,
            string nationality) 
            : base(requestId)
        {
            Id = id;
            CompanyId = companyId;
            CustomerType = customerType;
            Document = document;
            Email = email;
            LandLinePhone = landLinePhone;
            MobilePhone = mobilePhone;
            Name = name;
            Birthdate = birthdate;
            Nationality = nationality;
        }
    }
}
