using StackOverflowLLD.Models;
using StackOverflowLLD.Repositories;

namespace StackOverflowLLD.Services;

public interface IQuestionService
{
    int AddQuestion(string content, HashSet<string> topics);
    List<Question> GetFeed(HashSet<string> topics = null, bool? answered = null);
    Question GetQuestionDetails(int questionId);
}

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionService _sessionService;

    public QuestionService(IQuestionRepository questionRepository, ISessionService sessionService)
    {
        _questionRepository = questionRepository;
        _sessionService = sessionService;
    }

    public int AddQuestion(string content, HashSet<string> topics)
    {
        if (!_sessionService.IsUserLoggedIn())
            throw new InvalidOperationException("No user is logged in.");

        var currentUserId = _sessionService.GetCurrentUserId().Value;
        var question = new Question
        {
            Content = content,
            Topics = topics,
            AskedBy = currentUserId
        };

        return _questionRepository.AddQuestion(question);
    }

    public List<Question> GetFeed(HashSet<string> topics = null, bool? answered = null)
    {
        return _questionRepository
            .GetQuestions()
            .Where(q =>
                (topics == null || q.Topics.Overlaps(topics)) &&
                (!answered.HasValue || (answered.Value == q.AcceptedAnswerId.HasValue)))
            .ToList();
    }

    public Question GetQuestionDetails(int questionId) => _questionRepository.GetQuestionById(questionId);
}
