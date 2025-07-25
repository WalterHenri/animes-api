﻿using Animes.Application.DTOs;
using MediatR;

namespace Animes.Application.Features.Directors.Commands;

public class CreateDirectorCommand : IRequest<DirectorDto>
{
    public required string Name { get; set; }
}