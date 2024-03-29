﻿using System;
using Bogus;
using Xunit;

namespace ApiMvno.Test.Fixtures;

[CollectionDefinition(nameof(EntityColletion))]
public class EntityColletion : ICollectionFixture<EntityFixture>
{ }

public class EntityFixture : IDisposable
{
    public Faker Faker { get; private set; }
    public AddressFixture AddressFixture { get; private set; }
    public PhoneFixture PhoneFixture { get; private set; }
    public CompanyFixture CompanyFixture { get; private set; }

    public EntityFixture()
    {
        Faker = new Faker("pt_BR");

        AddressFixture = new AddressFixture();
        PhoneFixture = new PhoneFixture();
        CompanyFixture = new CompanyFixture();
    }

    public void Dispose()
    {
    }
}