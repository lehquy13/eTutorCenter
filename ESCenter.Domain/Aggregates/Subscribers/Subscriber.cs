﻿using ESCenter.Domain.Aggregates.Users.ValueObjects;
using Matt.SharedKernel.Domain.Primitives.Auditing;

namespace ESCenter.Domain.Aggregates.Subscribers;
public class Subscriber : CreationAuditedAggregateRoot<int>
{
    public IdentityGuid TutorId { get; private set; } = null!;

    private Subscriber()
    {
    }
    
    public static Subscriber Create(IdentityGuid tutorId)
    {
        return new Subscriber()
        {
            TutorId = tutorId
        };
    }
}

