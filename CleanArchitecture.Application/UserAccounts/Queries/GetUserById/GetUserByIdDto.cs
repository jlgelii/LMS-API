namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserById
{
    public class GetUserByIdDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}