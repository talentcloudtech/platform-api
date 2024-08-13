using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;
using ISession = Platform.Domain.Auth.Interfaces.ISession;

namespace Platform.Application.Auth;

public class Session : ISession
{
    public UserId UserId { get; private init; }

    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }
}
