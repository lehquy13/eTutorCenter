﻿using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESCenter.Persistence.EntityFrameworkCore.Configs;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName(nameof(Customer.Id))
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CustomerId.Create(value)
            );

        // TODO: learn how to config one to one relationship
        //'The relationship from 'User' to 'IdentityUser' with foreign key properties {'Id' : IdentityGuid} cannot target the primary key {'Id' : string} because it is not compatible.
        // Configure a principal key or a set of foreign key properties with compatible types for this relationship.' was thrown while attempting to create an instance.

        builder.Property(r => r.FirstName)
            .IsRequired();

        builder.Property(r => r.LastName)
            .IsRequired();

        builder.HasIndex(r => r.Email)
            .IsUnique();

        builder.Property(r => r.PhoneNumber);

        builder.Property(r => r.Gender)
            .IsRequired();

        builder.Property(r => r.BirthYear)
            .IsRequired();

        builder.Property(r => r.Avatar)
            .IsRequired();

        builder.Property(r => r.Description)
            .IsRequired();
        
        builder.Property(r => r.Role)
            .IsRequired();

        builder.OwnsOne(user => user.Address,
            navigationBuilder =>
            {
                navigationBuilder.Property(address => address.Country)
                    .HasColumnName("Country");
                navigationBuilder.Property(address => address.City)
                    .HasColumnName("City");
            });
    }
}