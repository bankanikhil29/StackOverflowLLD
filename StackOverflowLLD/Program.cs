using StackOverflowLLD.Repositories;
using StackOverflowLLD.Services;

public class Program
{
    public static void Main()
    {
        // Repositories
        var userRepo = new UserRepository();
        var questionRepo = new QuestionRepository();
        var answerRepo = new AnswerRepository();

        // Session Service
        var sessionService = new SessionService();

        // Services
        var userService = new UserService(userRepo, sessionService);
        var questionService = new QuestionService(questionRepo, sessionService);
        var answerService = new AnswerService(answerRepo, questionRepo, sessionService);

        // Simulation
        userService.Signup("Sachin", "Developer");
        userService.SubscribeToTopic("java");
        int questionId = questionService.AddQuestion("What is the latest version of Java?", new HashSet<string> { "java" });

        userService.Logout();

        userService.Signup("Kalyan", "Developer");
        int answerId = answerService.AddAnswer(questionId, "The latest version is Java 21.");
        answerService.UpvoteAnswer(answerId);

        Console.WriteLine("Demo complete!");
    }
}
