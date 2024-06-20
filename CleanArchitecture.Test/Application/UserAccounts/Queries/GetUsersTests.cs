using CleanArchitecture.Application.UserAccounts.Queries.GetUsers;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Queries
{
    public class GetUsersTests : BaseTest
    {
        private readonly GetUsersQueryHandler _sutHandler;

        public GetUsersTests()
        {
            _sutHandler = new GetUsersQueryHandler(_context);
        }

        [Fact]
        public async void GetUsers_ShouldGetAllUsers()
        {
            // Arrange
            var query = new GetUsersQuery();

            // Act
            var response = await _sutHandler.Handle(query, CancellationToken.None);

            // Assert
            var expected = new List<UserAccount>()
            {
                new UserAccount() { Id = 1, Username = "User1", Password = "Password1", CreatedBy = _jwtServices.GetLoggedUser().UserId, CreatedDate = _dateTimeService.Now },
                new UserAccount() { Id = 2, Username = "User2", Password = "Password1", CreatedBy = _jwtServices.GetLoggedUser().UserId, CreatedDate = _dateTimeService.Now },
                new UserAccount() { Id = 3, Username = "User3", Password = "Password1", CreatedBy = _jwtServices.GetLoggedUser().UserId, CreatedDate = _dateTimeService.Now },
                new UserAccount() { Id = 4, Username = "User4", Password = "Password1", CreatedBy = _jwtServices.GetLoggedUser().UserId, CreatedDate = _dateTimeService.Now },
                new UserAccount() { Id = 5, Username = "User5", Password = "Password1", CreatedBy = _jwtServices.GetLoggedUser().UserId, CreatedDate = _dateTimeService.Now },
            };

            response.Data.Count()
                .Should().Be(5);

            response.Data
                .Should().BeEquivalentTo(expected);
        }
    }
}
