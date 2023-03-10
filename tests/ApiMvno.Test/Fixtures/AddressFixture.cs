using System;
using System.Collections.Generic;
using ApiMvno.Domain.Entities;
using Bogus;

namespace ApiMvno.Test.Fixtures
{
    public class AddressFixture : IDisposable
    {
        public Faker Faker { get; private set; }

        private List<string> States = new List<string>()
        {
            "AC",
            "AL",
            "AP",
            "AM",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MT",
            "MS",
            "MG",
            "PA",
            "PB",
            "PR",
            "PE",
            "PI",
            "RJ",
            "RN",
            "RS",
            "RO",
            "RR",
            "SC",
            "SP",
            "SE",
            "TO"
        };

        private List<string> Neighborhoods = new List<string>()
        {
            "JD São José",
            "Vila Amelia",
            "Parque Ipiranga",
            "Centro",
            "Residencial das flores"
        };

        private List<string?> Details = new List<string?>()
        {
            null,
            "Ap 12",
            "Bloco 2 Apartamento 225",
            "Condiminio Vila Romanda 2 casa 25",
            "Bloco C Ap 33"
        };

        public AddressFixture()
        {
            Faker = new Faker("pt_BR");
        }

        public Address Valid()
        {
            return new Address(
                Guid.NewGuid(), 
                1,
                Faker.Address.ZipCode(),
                Faker.PickRandom(States),
                Faker.Address.City(),
                Faker.PickRandom(Neighborhoods),
                Faker.Address.StreetName(),
                int.Parse(Faker.Address.BuildingNumber()),
                Faker.PickRandom(Details)
            );
        }

        public Address Valid(long addressTypeId)
        {
            return new Address(
                Guid.NewGuid(),
                addressTypeId,
                Faker.Address.ZipCode(),
                Faker.PickRandom(States),
                Faker.Address.City(),
                Faker.PickRandom(Neighborhoods),
                Faker.Address.StreetName(),
                int.Parse(Faker.Address.BuildingNumber()),
                Faker.PickRandom(Details)
            );
        }

        public Address Invalid()
        {
            return new Address(
                Guid.Empty,
                1,
                "",
                "",
                "",
                "",
                "",
                0,
                ""
            );
        }

        public void Dispose()
        {
        }
    }
}
