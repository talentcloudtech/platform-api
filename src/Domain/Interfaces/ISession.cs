using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Auth.Interfaces;

public interface ISession
{
    public UserId UserId { get; }

    public DateTime Now { get; }
}
