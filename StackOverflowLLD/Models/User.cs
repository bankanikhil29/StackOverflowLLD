

using System;

namespace StackOverflowLLD.Models;
public class User
{
            
    public int Id { get; set; }
    public string Name { get; set; }
    public string Profession { get; set; }
    public HashSet<string> SubscribedTopics { get; private set; } = new();
    public List<int> QuestionsAsked { get; private set; } = new();
    public List<int> AnswersGiven { get; private set; } = new();
    public bool IsLoggedIn { get; set; } = false;
}
