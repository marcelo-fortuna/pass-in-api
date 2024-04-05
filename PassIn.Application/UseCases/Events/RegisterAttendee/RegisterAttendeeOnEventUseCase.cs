using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee
{
    public class RegisterAttendeeOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;

        public RegisterAttendeeOnEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
        {
            Validate(eventId, request);

            var entity = new Infrastructure.Entities.Attendee
            {
                Name = request.Name,
                Email = request.Email,
                Event_Id = eventId,
                Created_At = DateTime.UtcNow
            };

            _dbContext.Attendees.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id
            };
        }

        private void Validate(Guid eventId, RequestRegisterEventJson request)
        {
            //var eventExist = _dbContext.Events.Any(ev => ev.Id == eventId);
            var eventEntity = _dbContext.Events.Find(eventId);

            //if (eventExist == false)
            if (eventEntity is null)
                {
                throw new NotFoundException("An event with this id dont exists.");
            }

            if (string.IsNullOrWhiteSpace(request.Name) == true)
            {
                throw new ErrorOnValidationException("The name is invalid.");
            }

            if (EmailIsValid(request.Email) == false)
            {
                throw new ErrorOnValidationException("The e-mail is invalid.");
            }

            var attendeeAlreadyRegistred = _dbContext
                .Attendees
                .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);

            if (attendeeAlreadyRegistred == true)
            {
                throw new ConflictException("You can not register twice on the same event.");
            }

            var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);

            if(attendeesForEvent > eventEntity.Maximum_Attendees)
            {
                throw new ErrorOnValidationException("There is no space left for registration on this event.");
            }
        }

        private bool EmailIsValid(string email)
        {
            try
            {
                new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
