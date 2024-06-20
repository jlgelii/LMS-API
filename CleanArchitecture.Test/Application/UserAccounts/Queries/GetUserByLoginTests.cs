using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin;
using CleanArchitecture.Domain.Tokens;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Test.Configurations.Database;
using CleanArchitecture.Test.Configurations.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Queries
{
    public class GetUserByLoginTests : BaseTest
    {
        private readonly GetUserByLoginQueryHandler _sut;

        public GetUserByLoginTests()
        {
            _sut = new GetUserByLoginQueryHandler(_context, _jwtServices);
        }



        [Fact]
        public async void GetUserByLogin_ShouldGetUser_WhenAllParametersAreValid()
        {
            // Arrange
            var query = new GetUserByLoginQuery("User1", "Password1");

            // Act
            var response = await _sut.Handle(query, CancellationToken.None);

            // Assert
            var expected = new GetUserByLoginDto
            {
                Id = 1,
                Password = query.Password,
                Username = query.Username,
                Token = _jwtServices.CreateToken(new TokenData { UserId = 1 }),
            };

            response.Error
                    .Should().BeFalse();

            response.Data
                .Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void GetUserByLogin_ShouldValidateUser_WhenUsernameIsEmpty()
        {
            // Arrange
            var validator = new GetUserLoginQueryValidator();
            var query = new GetUserByLoginQuery(username: "", password: "password");

            // Act
            var response = await validator.Validate(query);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }

        [Fact]
        public async void GetUserByLogin_ShouldValidateUser_WhenPasswordIsEmpty()
        {
            // Arrange
            var validator = new GetUserLoginQueryValidator();
            var query = new GetUserByLoginQuery(username: "user", password: "");

            // Act
            var response = await validator.Validate(query);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }
    }
}
