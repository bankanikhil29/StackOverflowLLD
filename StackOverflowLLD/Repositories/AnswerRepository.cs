
using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowLLD.Models;

namespace StackOverflowLLD.Repositories;

public interface IAnswerRepository
{
    Answer GetAnswerById(int answerId);
    List<Answer> GetAnswersByQuestionId(int questionId);
    int AddAnswer(Answer answer);
    void UpdateAnswer(Answer answer);
}

    public class AnswerRepository : IAnswerRepository
    {
        private readonly List<Answer> _answers = new List<Answer>();

        public int AddAnswer(Answer answer)
        {
            answer.Id = _answers.Count + 1; // Simple id generation logic
            _answers.Add(answer);
            return answer.Id;
        }

        public Answer GetAnswerById(int id)
        {
            return _answers.FirstOrDefault(a => a.Id == id);
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            return _answers.Where(a => a.QuestionId == questionId).ToList();
        }

        public void UpdateAnswer(Answer answer)
        {
            var existingAnswer = GetAnswerById(answer.Id);
            if (existingAnswer != null)
            {
                existingAnswer.Content = answer.Content;
                existingAnswer.Votes = answer.Votes;
                existingAnswer.IsAccepted = answer.IsAccepted;
            }
        }

        public void DeleteAnswer(int id)
        {
            var answer = GetAnswerById(id);
            if (answer != null)
            {
                _answers.Remove(answer);
            }
        }
    }
