using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using Bogus;
using Bogus.Extensions.Brazil;

namespace ApiMvno.Test.Fixtures;

public class CompanyFixture : IDisposable
{
    public Faker Faker { get; private set; }
    public AddressFixture AddressFixture { get; private set; }
    public PhoneFixture PhoneFixture { get; private set; }

    public CompanyFixture()
    {
        Faker = new Faker("pt_BR");
        AddressFixture = new AddressFixture();
        PhoneFixture = new PhoneFixture();
    }

    public Company Valid()
    {
        var name = Faker.Company.CompanyName(1);
        var emailProvider = $"{Regex.Replace(Regex.Replace(name, @"\s+", ""), "[^a-zA-Z0-9]", "")}.com.br".ToLower();

        return new Company(
            name,
            Faker.Internet.Email(provider: emailProvider),
            Faker.Company.Cnpj(),
            new List<CompanyAddress>()
            {
                new CompanyAddress(AddressFixture.Valid())
            },
            new List<CompanyPhone>()
            {
                new CompanyPhone(PhoneFixture.Valid(Faker.PickRandom<PhoneTypeEnum>()))
            }
        );
    }

    public Company Invalid()
    {
        return new Company(
            "a",
            "invalidEmail",
            Faker.Person.Cpf(),
            new List<CompanyAddress>()
            {
                new CompanyAddress(AddressFixture.Invalid())
            },
            new List<CompanyPhone>()
            {
                new CompanyPhone(PhoneFixture.Invalid())
            }
        );
    }

    public void Dispose()
    {
    }
}