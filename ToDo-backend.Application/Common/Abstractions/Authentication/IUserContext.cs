namespace ToDo_backend.Application.Common.Abstractions.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
};