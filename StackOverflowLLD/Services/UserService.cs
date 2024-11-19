using StackOverflowLLD.Models;
using StackOverflowLLD.Repositories;

namespace StackOverflowLLD.Services;

public interface IUserService
{
    int Signup(string name, string profession);
    void Login(string name);
    void Logout();
    void SubscribeToTopic(string topic);
    void UnsubscribeFromTopic(string topic);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionService _sessionService;

    public UserService(IUserRepository userRepository, ISessionService sessionService)
    {
        _userRepository = userRepository;
        _sessionService = sessionService;
    }

    public int Signup(string name, string profession)
    {
        var user = new User { Name = name, Profession = profession };
        var userId = _userRepository.AddUser(user);
        _sessionService.SetCurrentUser(userId); // Automatically log in the user after signup
        return userId;
    }

    public void Login(string name)
    {
        var user = _userRepository.GetUserByName(name);
        if (user == null) throw new ArgumentException("User not found.");
        _sessionService.SetCurrentUser(user.Id);
    }

    public void Logout()
    {
        _sessionService.ClearCurrentUser();
    }

    public void SubscribeToTopic(string topic)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var currentUserId = _sessionService.GetCurrentUserId().Value;
        var user = _userRepository.GetUserById(currentUserId);
        user.SubscribedTopics.Add(topic);
        _userRepository.UpdateUser(user);
    }

    public void UnsubscribeFromTopic(string topic)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var currentUserId = _sessionService.GetCurrentUserId().Value;
        var user = _userRepository.GetUserById(currentUserId);
        user.SubscribedTopics.Remove(topic);
        _userRepository.UpdateUser(user);
    }
}
