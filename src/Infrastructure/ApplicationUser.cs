using System;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Platform.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();
}

public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();
}
