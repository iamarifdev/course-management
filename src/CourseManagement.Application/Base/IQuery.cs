using MediatR;

namespace CourseManagement.Application.Base;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}