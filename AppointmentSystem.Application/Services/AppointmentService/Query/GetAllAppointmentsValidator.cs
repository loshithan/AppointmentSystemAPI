using FluentValidation;

public class GetAllAppointmentsValidator : AbstractValidator<GetAllAppointments.Query>
{
    public GetAllAppointmentsValidator()
    {
        RuleFor(x => x.SearchParam).NotEmpty().WithMessage("Search parameter is required");
    }
    
}