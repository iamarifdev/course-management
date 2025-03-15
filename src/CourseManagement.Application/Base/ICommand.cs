using CourseManagement.Domain.Base;
using MediatR;

namespace CourseManagement.Application.Base;

public interface IBaseCommand
{
}

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}
