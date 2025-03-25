using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Application.Users;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Students.AddStudent;

internal sealed class AddStudentCommandHandler(
    IStudentRepository studentRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
) : ICommandHandler<AddStudentCommand, StudentResponse>
{
    public async Task<Result<StudentResponse>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.ToLowerCase();
        
        var isExists = await userRepository.ExistsAsync(x => x.Email == new Email(email), cancellationToken);
        if (isExists)
        {
            return Result.Failure<StudentResponse>(UserErrors.UserExists);
        }

        var hashedPassword = passwordHasher.Hash(request.Password).Value;
        var user = User.Create(
            new Email(email),
            Role.Student,
            new Password(hashedPassword)
        );
        userRepository.Add(user);

        var student = Student.Create(
            user.Id,
            request.FirstName,
            request.LastName,
            request.AddedById
        );
        studentRepository.Add(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var response = new StudentResponse(
            student.Id,
            user.Id,
            user.Email.Value,
            student.FirstName,
            student.LastName,
            request.AddedById,
            student.CreatedAt
        );

        return Result.Success(response);
    }
}
