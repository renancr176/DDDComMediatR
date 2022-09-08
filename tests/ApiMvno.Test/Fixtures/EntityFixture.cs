using System;
using Bogus;
using Xunit;

namespace ApiMvno.Test.Fixtures;

[CollectionDefinition(nameof(EntityColletion))]
public class EntityColletion : ICollectionFixture<EntityFixture>
{ }

public class EntityFixture : IDisposable
{
    public Faker Faker { get; private set; }
    public CompanyFixture CompanyFixture { get; private set; }

    public EntityFixture()
    {
        Faker = new Faker("pt_BR");
        CompanyFixture = new CompanyFixture();
    }

    public void Dispose()
    {
    }
}