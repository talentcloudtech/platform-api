using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;

namespace Platform.Application.Features.MemberRequests.UpdateMemberRequest;

/// <summary>
/// Represents a validator for the UpdateMemberRequestRequest class.
/// </summary>
public class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberRequestRequest>
{
    /// <summary>
    /// Valid properties for update.
    /// </summary>
    private static readonly PropertyInfo[] ValidProperties = typeof(UpdateMemberRequestRequest)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() == null)
        .ToArray();

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberRequestValidator"/> class.
    /// </summary>
    public UpdateMemberRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.FieldsToUpdate)
            .NotEmpty()
            .WithMessage("At least one field must be specified for update.")
            .Must(fields => fields.All(field => ValidProperties.Any(p => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase))))
            .WithMessage($"One or more specified fields are invalid. Valid fields are: {string.Join(", ", ValidProperties.Select(p => p.Name))}");

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.Title), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.StartDate), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.EndDate), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.EndDate)
                    .Must((request, endDate) => !endDate.HasValue || !request.StartDate.HasValue || endDate.Value >= request.StartDate.Value)
                    .WithMessage("End date must be equal to or after the start date.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.ExpectedBudget), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.ExpectedBudget).NotEmpty().WithMessage("Expected budget cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.WorkHour), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.WorkHour).NotEmpty().WithMessage("Work hour cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.Timezone), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.Timezone).NotEmpty().WithMessage("Timezone cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.RequiredSkills), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.RequiredSkills).NotEmpty().WithMessage("Required skills cannot be null when specified in FieldsToUpdate.");
            }
        );

        When(
            x => x.FieldsToUpdate.Contains(nameof(UpdateMemberRequestRequest.Status), StringComparer.OrdinalIgnoreCase),
            () =>
            {
                RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue).WithMessage("Invalid status value.");
            }
        );
    }
}
