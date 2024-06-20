using CleanArchitecture.Application.UserAccounts.Command.DeleteUser;
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
    public class DeleteUserCommandTests : BaseTest
    {
        private readonly DeleteUserCommandHandler _sutHandler;
        private readonly DeleteUserCommandValidator _sutValidator;

        public DeleteUserCommandTests()
        {
            _sutHandler = new DeleteUserCommandHandler(_context, _jwtServices, _dateTimeService);
            _sutValidator = new DeleteUserCommandValidator();
        }


        [Fact]
        public async void DeleteUser_ShouldDeleteUser_WhenAllParametersAreValid()
        {
            // Arrange
            var command = new DeleteUserCommand(1);

            // Act
            var response = await _sutHandler.Handle(command, CancellationToken.None);

            // Assert
            response.Error
                .Should().BeFalse();

            _context.UserAccount
                .FirstOrDefault(u => u.Id == command.Id)
                .Should().BeNull();
        }

        [Fact]
        public async void DeleteUser_ShouldValidate_WhenUserIdDoesNotExist()
        {
            // Arrange
            var command = new DeleteUserCommand(7);

            // Act
            var response = await _sutHandler.Handle(command, CancellationToken.None);

            // Assert
            response.Error
                .Should().BeTrue();

            _context.UserAccount
                .FirstOrDefault(u => u.Id == command.Id)
                .Should().BeNull();
        }

        [Fact]
        public async void DeleteUser_ShouldValidate_WhenUserIdIsInvalid()
        {
            // Arrange
            var command = new DeleteUserCommand(-1);

            // Act
            var response = await _sutValidator.Validate(command);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }
    }
}
