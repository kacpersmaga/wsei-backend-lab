using ApplicationCore.Models.QuizAggregate;

namespace WebApi.Dto;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }
    
    public static QuizItemDto of(QuizItem quiz)
    {
        var allOptions = new List<string>(quiz.IncorrectAnswers);
        allOptions.Add(quiz.CorrectAnswer);
        
        var random = new Random();
        var shuffledOptions = allOptions.OrderBy(o => random.Next()).ToList();

        return new QuizItemDto
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Options = shuffledOptions
        };
    }

}