
using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowLLD.Models;

namespace StackOverflowLLD.Repositories;

public interface IQuestionRepository
{
    Question GetQuestionById(int questionId);
    List<Question> GetQuestions();
    int AddQuestion(Question question);
    void UpdateQuestion(Question question);
}

    public class QuestionRepository : IQuestionRepository
    {
        private readonly List<Question> _questions = new List<Question>();

        public int AddQuestion(Question question)
        {
            question.Id = _questions.Count + 1; // Simple id generation logic
            _questions.Add(question);
            return question.Id;
        }

        public Question GetQuestionById(int id)
        {
            return _questions.FirstOrDefault(q => q.Id == id);
        }

        public List<Question> GetQuestions()
        {
            return _questions;
        }

        public void UpdateQuestion(Question question)
        {
            var existingQuestion = GetQuestionById(question.Id);
            if (existingQuestion != null)
            {
                existingQuestion.Content = question.Content;
                existingQuestion.Topics = question.Topics;
                existingQuestion.AcceptedAnswerId = question.AcceptedAnswerId;
            }
        }

        public void DeleteQuestion(int id)
        {
            var question = GetQuestionById(id);
            if (question != null)
            {
                _questions.Remove(question);
            }
        }
    }

