using CleanArchitecture.Application.UserAccounts.Command.UpdateUser;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Command
{
    public class UpdateUserCommandTests : BaseTest
    {
        private readonly UpdateUserCommandHandler _sutHandler;
        private readonly UpdateUserCommandValidator _sutValidate;

        public UpdateUserCommandTests()
        {
            _sutHandler = new UpdateUserCommandHandler(_context, _jwtServices);
            _sutValidate = new UpdateUserCommandValidator();
        }


        [Fact]
        public async void UpdateUser_ShouldUpdateUser_WhenAllParameterAreValid()
        {
            // Arrange
            var command = new UpdateUserCommand(1, "User1000", "Password123");

            // Act
            var response = await _sutHandler.Handle(command, CancellationToken.None);

            // Assert
            var expected = new UserAccount()
            {
                Username = command.Username,
                Password = command.Password,
                Id = command.Id,
                UpdatedBy = _jwtServices.GetLoggedUser().UserId,
                UpdatedDate = _dateTimeService.Now,
                CreatedBy = _jwtServices.GetLoggedUser().UserId,
                CreatedDate = _dateTimeService.Now,
            };

            response.Error
                .Should().BeFalse();

            response.Data
                .Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void UpdateUser_ShouldValidate_WhenUserDoesNotExist()
        {
            // Arrange
            var command = new UpdateUserCommand(7, "User1000", "Password123");

            // Act
            var response = await _sutHandler.Handle(command, CancellationToken.None);

            // Assert
            response.Error
                .Should().BeTrue();

            _context.UserAccount
                    .FirstOrDefault(u => u.Username == command.Username
                                      && u.Password == command.Password)
                    .Should().BeNull();
        }

        [Fact]
        public async void UpdateUser_ShouldValidate_WhenUsernameUsed()
        {
            // Arrange
            var command = new UpdateUserCommand(1, "User2", "Password123");

            // Act
            var response = await _sutHandler.Handle(command, CancellationToken.None);

            // Assert
            response.Error
                .Should().BeTrue();

            _context.UserAccount
                    .Where(u => u.Username == command.Username
                             && u.Id != command.Id)
                    .Should().HaveCount(1);
        }

        [Fact]
        public async void UpdateUser_ShouldValidate_WhenUsernameIsEmpty()
        {
            // Arrange
            var command = new UpdateUserCommand(1, "", "Password123");

            // Act
            var response = await _sutValidate.Validate(command);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }

        [Fact]
        public async void UpdateUser_ShouldValidate_WhenPasswordIsEmpty()
        {
            // Arrange
            var command = new UpdateUserCommand(1, "dasdas", "");

            // Act
            var response = await _sutValidate.Validate(command);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }

        [Fact]
        public async void UpdateUser_ShouldValidate_WhenPasswordLess6Characters()
        {
            // Arrange
            var command = new UpdateUserCommand(1, "dsads", "1234");

            // Act
            var response = await _sutValidate.Validate(command);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }
    }
}
