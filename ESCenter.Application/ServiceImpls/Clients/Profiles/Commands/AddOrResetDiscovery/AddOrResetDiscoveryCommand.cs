﻿using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Application.ServiceImpls.Clients.Profiles.Commands.AddOrResetDiscovery;

public record AddOrResetDiscoveryCommand(Guid UserId, List<Guid> DiscoveryIds) : ICommandRequest;