namespace PassIn.Application.UseCases.Checkins.DoChekin
{
    public class DoAttendeeCheckinUseCase
    {
        private readonly PassInDbContext _dbContext;

        public DoAttendeeCheckinUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(Guid attendeeId)
        {
            Validate(attendeeId);

            var entity = new Checkin
            {
                Attendee_Id = attendeeId,
                Created_At = DateTime.UtcNow,
            };

            _dbContext.Checkins.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id,
            };
        }

        private void Validate(Guid attendeeId)
        {
            var existAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);

            if(existAttendee == false)
            {
                throw new NotFoundException("The attendee with this Id was not found.");
            }

            var existCheckin = _dbContext.Checkins.Any(ch => ch.Attendee_Id == attendeeId);

            if(existCheckin == true)
            {
                throw new ConflictException("The attendee can not do checking twice in the same event.");
            }
        }

    }
}
