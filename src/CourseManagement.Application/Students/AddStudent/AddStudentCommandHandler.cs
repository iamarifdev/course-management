using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Users;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Students.AddStudent;

internal sealed class AddStudentCommandHandler(
    IUserContext userContext,
    IStudentRepository studentRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
) : ICommandHandler<AddStudentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var isExists = await userRepository.ExistsAsync(x => x.Email == new Email(request.Email), cancellationToken);
        if (isExists)
        {
            return Result.Failure<Guid>(UserErrors.UserExists);
        }

        var hashedPassword = passwordHasher.Hash(request.Password).Value;
        var user = User.Create(
            new Email(request.Email),
            Role.Student,
            new Password(hashedPassword)
        );
        userRepository.Add(user);

        var student = Student.Create(
            user.Id,
            request.FirstName,
            request.LastName,
            userContext.StaffId
        );
        studentRepository.Add(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(student.Id);
    }
}
