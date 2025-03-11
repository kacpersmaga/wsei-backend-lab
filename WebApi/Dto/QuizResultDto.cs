namespace WebApi.Dto;

public class QuizResultDto
{
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int CorrectAnswersCount { get; set; }
}