
using StackOverflowLLD.Models;
using StackOverflowLLD.Repositories;

namespace StackOverflowLLD.Services;

public interface IAnswerService
{
    int AddAnswer(int questionId, string content);
    void AcceptAnswer(int questionId, int answerId);
    void UpvoteAnswer(int answerId);
}

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionService _sessionService;

    public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository, ISessionService sessionService)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _sessionService = sessionService;
    }

    public int AddAnswer(int questionId, string content)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var currentUserId = _sessionService.GetCurrentUserId().Value;
        var question = _questionRepository.GetQuestionById(questionId);
        if (question == null)
            throw new ArgumentException("Question not found.");

        var answer = new Answer
        {
            Content = content,
            QuestionId = questionId,
            GivenBy = currentUserId
        };

        var answerId = _answerRepository.AddAnswer(answer);
        question.Answers.Add(answerId);
        _questionRepository.UpdateQuestion(question);

        return answerId;
    }

    public void AcceptAnswer(int questionId, int answerId)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var currentUserId = _sessionService.GetCurrentUserId().Value;
        var question = _questionRepository.GetQuestionById(questionId);

        if (question == null || question.AskedBy != currentUserId)
            throw new InvalidOperationException("You can only accept answers for your own questions.");

        question.AcceptedAnswerId = answerId;
        _questionRepository.UpdateQuestion(question);
    }

    public void UpvoteAnswer(int answerId)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var answer = _answerRepository.GetAnswerById(answerId);
        if (answer == null)
            throw new ArgumentException("Answer not found.");

        answer.Votes++;
        _answerRepository.UpdateAnswer(answer);
    }
}
