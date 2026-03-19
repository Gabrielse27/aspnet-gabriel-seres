namespace CoreFitness.Infrastructure.Persistence.Entities.Members
{
    public class MemberEntity
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; } 
         public string? PhoneNumber { get; set; }

        public string? ProfileImageUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifieAt { get; set; }
       



    }
}
