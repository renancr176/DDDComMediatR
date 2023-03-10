using System;
using ApiMvno.Domain.Entities;
using ApiMvno.Domain.Enums;
using Bogus;

namespace ApiMvno.Test.Fixtures;

public class PhoneFixture : IDisposable
{
    public Faker Faker { get; private set; }

    public PhoneFixture()
    {
        Faker = new Faker("pt_BR");
    }

    public Phone Valid(PhoneTypeEnum? phoneType = null)
    {
        phoneType = phoneType ?? Faker.PickRandom<PhoneTypeEnum>();
        var number = $"55{Faker.Phone.PhoneNumber(phoneType == PhoneTypeEnum.Mobile ? "###########" : "##########")}";
        var phoneNumber = long.Parse(number);
        var phoneTypeId = $"{(int) phoneType + 1}";

        return new Phone(
            long.Parse(phoneTypeId),
            phoneNumber
        )
        {PhoneType = new PhoneType(phoneType.Value, phoneType.Value.ToString(), true)};
    }

    public Phone Invalid()
    {
        return new Phone(
            0,
            0
        );
    }

    public void Dispose()
    {
    }
}