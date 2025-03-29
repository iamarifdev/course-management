using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Extensions;
using NSubstitute;
using CourseManagement.Application.Students.AddStudent;
using CourseManagement.Application.Users;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;
using FluentAssertions;

namespace CourseManagement.Application.UnitTests.Students;

public class AddStudentTests
{
    private static readonly AddStudentCommand Command = new(
        "Test",
        "Student",
        "test@example.com",
        "password",
        Guid.NewGuid()
    );

    private readonly AddStudentCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public AddStudentTests()
    {
        var studentRepositoryMock = Substitute.For<IStudentRepository>();
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new AddStudentCommandHandler(
            studentRepositoryMock,
            _userRepositoryMock,
            _passwordHasherMock,
            _unitOfWorkMock
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenStudentExists()
    {
        // Arrange
        _userRepositoryMock
            .ExistsByEmailAsync(new Email(Command.Email.ToLowerCase()), Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.UserExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WithNewStudent()
    {
        // Arrange
        _userRepositoryMock
            .ExistsByEmailAsync(new Email(Command.Email.ToLowerCase()), Arg.Any<CancellationToken>())
            .Returns(false);
        _passwordHasherMock.Hash(Arg.Any<string>()).Returns(Result.Success(Command.Password));
        _unitOfWorkMock.SaveChangesAsync().Returns(1);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AddedById.Should().Be(Command.AddedById);
        result.Value.FirstName.Should().Be(Command.FirstName);
        result.Value.LastName.Should().Be(Command.LastName);
        result.Value.Email.Should().Be(Command.Email);
        result.Value.CreatedAt.Should().NotBe(default);
        
    }
}
