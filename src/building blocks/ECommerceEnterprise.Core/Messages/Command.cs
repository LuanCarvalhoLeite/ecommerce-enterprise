﻿using FluentValidation.Results;
using MediatR;

namespace ECommerceEnterprise.Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult>
{
    public DateTime TimeStamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        TimeStamp = DateTime.Now;
        ValidationResult = new ValidationResult();
    }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}
