using System;
using ApiMvno.Domain.Entities;
using Bogus;

namespace ApiMvno.Test.Fixtures;

public class CompanyFixture : IDisposable
{
    public Faker Faker { get; private set; }

    public CompanyFixture()
    {
        Faker = new Faker("pt_BR");
    }

    public Company Valid()
    {
        return new Company(Faker.Company.CompanyName());
    }

    public Company Invalid()
    {
        return new Company(String.Empty);
    }

    public void Dispose()
    {
    }
}