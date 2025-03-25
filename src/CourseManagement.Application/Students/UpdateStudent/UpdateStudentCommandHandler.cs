using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.UpdateStudent;

internal sealed class UpdateStudentCommandHandler(
    IStudentRepository repository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateStudentCommand, StudentResponse>
{
    public async Task<Result<StudentResponse>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await repository.GetQueryable()
            .Include(x => x.User)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (student is null)
        {
            return Result.Failure<StudentResponse>(StudentErrors.NotFound);
        }

        // if email is in the request then check the email whether it is already used by other
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var email = request.Email.ToLowerCase();

            var studentByEmail = await repository.GetByEmailAsync(email, cancellationToken);
            if (studentByEmail is not null && studentByEmail.Id != student.Id)
            {
                return Result.Failure<StudentResponse>(StudentErrors.AlreadyExistsByEmail);
            }

            student.User.UpdateEmail(email);
        }

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var hashedPassword = passwordHasher.Hash(request.Password);
            student.User.UpdatePassword(hashedPassword.Value);
        }

        student.Update(request.FirstName, request.LastName);
        repository.Update(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new StudentResponse(
            student.Id,
            student.UserId,
            student.User.Email.Value,
            student.FirstName,
            student.LastName,
            student.StaffId,
            student.CreatedAt
        );

        return Result.Success(result);
    }
}
