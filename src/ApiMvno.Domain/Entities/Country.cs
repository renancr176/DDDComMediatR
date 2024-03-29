﻿using ApiMvno.Domain.Core.DomainObjects;

namespace ApiMvno.Domain.Entities
{
    public class Country : Entity
    {
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public bool Active { get; set; } = true;

        #region Relationships

        public virtual ICollection<Address> Addresses { get; set; }

        #endregion

        public Country()
        {
        }

        public Country(string name, string phoneCode, bool active)
        {
            Name = name;
            PhoneCode = phoneCode;
            Active = active;
        }
    }
}
