namespace CustomerOperations.Application.Dto
{
    public record CustomerDto : CustomerCreateDto
    {
        public short State { get; init; }
    }

    public record CustomerCreateDto : CustomerUpdateDto
    {
        public string IdentityNumber { get; init; }
        public string Password { get; init; }
    }

    public record CustomerUpdateDto
    {
        public string Name { get; init; }
        public string Gender { get; init; }
        public short Age { get; init; }
        public string Address { get; init; }
        public string PhoneNumber { get; init; }
    }
}
