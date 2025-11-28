namespace ToDo_backend.Web.API.Controllers.Users;

public sealed record RegisterRequest(
    string Email,
    string Password);