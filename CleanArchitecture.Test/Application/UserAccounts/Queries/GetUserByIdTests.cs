using CleanArchitecture.Application.UserAccounts.Queries.GetUserById;
using FluentAssertions;
using System.Threading;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Queries
{
    public class GetUserByIdTests : BaseTest
    {
        private readonly GetUserByIdQueryHandler _sutHandler;

        public GetUserByIdTests()
        {
            _sutHandler = new GetUserByIdQueryHandler(_context);
        }


        [Fact]
        public async void GetUserById_ShouldGetUser_WhenParametersAreValid()
        {
            // Arrange
            var id = 1;
            var query = new GetUserByIdQuery(id);

            // Act
            var response = await _sutHandler.Handle(query, CancellationToken.None);

            // Assert
            var expected = new GetUserByIdDto()
            {
                Id = 1,
                Username = "User1",
                Password = "Password1",
            };

            response.Data
                .Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void GetUserById_ShouldReturnError_WhenUserDoesNotExist()
        {
            // Arrange
            var id = 10;
            var query = new GetUserByIdQuery(id);

            // Act
            var response = await _sutHandler.Handle(query, CancellationToken.None);

            // Assert
            response.Error
                .Should().BeTrue();
        }
    }
}
