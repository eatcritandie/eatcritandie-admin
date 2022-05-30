namespace EatCritAndDie.Admin.Application.Services.User;

public interface IUserService
{
    Task<Domain.Models.User> GetUserAsync();
}