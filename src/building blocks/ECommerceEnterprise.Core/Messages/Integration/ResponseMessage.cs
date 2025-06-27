
using FluentValidation.Results;

namespace ECommerceEnterprise.Core.Messages.Integration;

public class ResponseMessage : Message
{
    public ValidationResult ValidationResult { get; set; }

    public ResponseMessage(ValidationResult validatioResult)
    {
        ValidationResult = validatioResult;
    }
}
