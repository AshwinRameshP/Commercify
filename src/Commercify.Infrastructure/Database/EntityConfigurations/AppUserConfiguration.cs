using Commercify.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercify.Infrastructure.Database.EntityConfigurations;

public class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(AppUser.MaxLengths.FirstName);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(AppUser.MaxLengths.LastName);
    }
}
