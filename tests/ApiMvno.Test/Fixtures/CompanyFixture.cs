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
        //return new Company();
        throw new NotImplementedException();
    }

    public Company Invalid()
    {
        //return new Company();
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}