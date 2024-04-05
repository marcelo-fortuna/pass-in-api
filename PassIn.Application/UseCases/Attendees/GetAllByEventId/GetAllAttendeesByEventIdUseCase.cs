namespace PassIn.Application.UseCases.Attendees.GetAllByEventId
{
    public class GetAllAttendeesByEventIdUseCase
    {
        private readonly PassInDbContext _dbContext;

        public GetAllAttendeesByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseAllAttendeesJson Execute(Guid eventId)
        {
            //var attendees = _dbContext.Attendees.Where(attendee => attendee.Event_Id == eventId).ToList();
            var entity = _dbContext
                .Events
                .Include(ev => ev.Attendees)
                .ThenInclude(attendee => attendee.Checkin)
                .FirstOrDefault(ev => ev.Id == eventId);

            if (entity is null)
            {
                throw new NotFoundException("An event with this id dont exists.");
            }

            return new ResponseAllAttendeesJson
            {
                Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
                {
                    Id = attendee.Id,
                    Name = attendee.Name,
                    Email = attendee.Email,
                    CreatedAt = attendee.Created_At,
                    CheckedInAt = attendee.CheckIn.Created_At
                }).ToList()
            };
        }
    }
}
