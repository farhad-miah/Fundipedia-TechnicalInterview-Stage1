using Fundipedia.TechnicalInterview.Data.Context;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundipedia.TechnicalInterview.Data;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SupplierContext(
                   serviceProvider.GetRequiredService<DbContextOptions<SupplierContext>>()))
        {
            if (context.Suppliers.Any())
            {
                return;   // Data was already seeded
            }

            var emails = new List<Email>
            {
                new Email
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "test1@test.com",
                    IsPreferred = true
                },
                new Email
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "test2@test.com",
                    IsPreferred = false
                }
            };

            var phones = new List<Phone>
            {
                new Phone
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = "12341234",
                    IsPreferred = true
                },
                new Phone
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = "09870987",
                    IsPreferred = false
                }
            };

            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Spongebob",
                    LastName ="Squarepants",
                    Emails = new List<Email>{emails.First() },
                    Phones =  new List<Phone>{phones.First() }
                },
                new Supplier
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Patrick",
                    LastName ="Star",
                    Emails = new List<Email>{emails.Skip(1).First() },
                    Phones =  new List<Phone>{phones.Skip(1).First() }
                }
            };

            context.Emails.AddRange(emails);
            context.Phones.AddRange(phones);
            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();
        }
    }
}