using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Application.UserAccounts.Command.CreateUser;
using CleanArchitecture.Domain.Entities;
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

namespace CleanArchitecture.Test.Application.UserAccounts.Command
{
    public class CreateUserCommandTests : BaseTest
    {
        private readonly CreateUserCommandHandler _sut;

        public CreateUserCommandTests()
        {
            _sut = new CreateUserCommandHandler(_context, _jwtServices);
        }

        [Fact]
        public async void CreateUser_ShouldCreateUser_WhenAllParametersAreValid()
        {
            // Arrange
            var command = new CreateUserCommand("user6", "password");

            // Act
            var response = await _sut.Handle(command, CancellationToken.None);

            // Assert
            var expected = new UserAccount()
            {
                CreatedBy = _jwtServices.GetLoggedUser().UserId,
                CreatedDate = _dateTimeService.Now,
                Id = 6,
                Password = command.Password,
                Username = command.Username,
            };

            response.Data
                .Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void CreateUser_ShouldReturnError_WhenUsernameExist()
        {
            // Arrange
            var command = new CreateUserCommand("User1", "password");

            // Act
            var response = await _sut.Handle(command, CancellationToken.None);

            // Assert
            _context.UserAccount
                    .ToList()
                    .Should().HaveCount(5);
        }

        [Fact]
        public async void CreateUser_ShouldValidateUser_WhenUsernameEmpty()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand(username: "", password: "password");

            // Act
            var response = await validator.Validate(command);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();
        }

        [Fact]
        public async void CreateUser_ShouldValidateUser_WhenPasswordEmpty()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand(username: "user", password: "");

            // Act
            var response = await validator.Validate(command);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();
        }

        [Fact]
        public async void CreateUser_ShouldValidateUser_WhenPasswordLess6Characters()
        {
            // Arrange
            var validator = new CreateUserCommandValidator();
            var command = new CreateUserCommand(username: "user", password: "1234");

            // Act
            var response = await validator.Validate(command);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();
        }
    }
}
