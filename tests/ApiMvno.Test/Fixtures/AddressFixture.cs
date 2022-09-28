using Bogus;
using Bogus.DataSets;
using System;

namespace ApiMvno.Test.Fixtures
{
    public class AddressFixture
    {
        public Faker Faker { get; private set; }

        public AddressFixture()
        {
            Faker = new Faker("pt_BR");
        }

        public Address Valid()
        {
            return new Address(Faker.Address.ZipCode());
        }

        public Address Invalid()
        {
            return new Address(String.Empty);
        }

        public void Dispose()
        {
        }
    }
}
