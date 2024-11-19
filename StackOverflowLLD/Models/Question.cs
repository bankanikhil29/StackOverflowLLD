

using System;

namespace StackOverflowLLD.Models;
public class Question
{
    public int Id { get; set; }
    public string Content { get; set; }
    public HashSet<string> Topics { get; set; } = new();
    public int AskedBy { get; set; }
    public int? AcceptedAnswerId { get; set; }
    public List<int> Answers { get; set; } = new();
}
