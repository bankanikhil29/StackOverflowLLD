

using System;

namespace StackOverflowLLD.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuestionId { get; set; }
        public int GivenBy { get; set; }  // User ID who provided the answer
        public bool IsAccepted { get; set; } = false;
        public int Votes { get; set; } = 0;
    }
}
