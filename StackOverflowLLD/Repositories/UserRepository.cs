
using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowLLD.Models;

namespace StackOverflowLLD.Repositories;

public interface IUserRepository
{
    User GetUserById(int userId);
    User GetUserByName(string name);
    int AddUser(User user);
    void UpdateUser(User user);
}

public class UserRepository : IUserRepository
{
    private readonly Dictionary<int, User> _users = new();
    private int _userCounter = 0;

    public int AddUser(User user)
    {
        _userCounter++;
        user.Id = _userCounter;
        _users[_userCounter] = user;
        return _userCounter;
    }

    public User GetUserById(int userId) => _users.GetValueOrDefault(userId);

    public User GetUserByName(string name) =>
        _users.Values.FirstOrDefault(user => user.Name == name);

    public void UpdateUser(User user)
    {
        if (_users.ContainsKey(user.Id))
        {
            _users[user.Id] = user;
        }
    }
}
