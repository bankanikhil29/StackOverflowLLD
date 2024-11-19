namespace StackOverflowLLD.Services;
public interface ISessionService
{
    int? GetCurrentUserId();
    void SetCurrentUser(int userId);
    void ClearCurrentUser();
    bool IsUserLoggedIn();
}

public class SessionService : ISessionService
{
    private int? _currentUserId;

    public int? GetCurrentUserId() => _currentUserId;

    public void SetCurrentUser(int userId)
    {
        if (_currentUserId != null)
            throw new InvalidOperationException("Another user is already logged in.");
        _currentUserId = userId;
    }

    public void ClearCurrentUser()
    {
        if (_currentUserId == null)
            throw new InvalidOperationException("No user is currently logged in.");
        _currentUserId = null;
    }

    public bool IsUserLoggedIn() => _currentUserId != null;
}
