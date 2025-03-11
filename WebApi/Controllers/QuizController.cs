using BackendLab01;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IQuizUserService _service;

    public QuizController(IQuizUserService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var quiz = _service.FindQuizById(id);

        if (quiz != null)
        {
            var quizDto = QuizDto.of(quiz);
            return Ok(quizDto);
        }

        return NotFound();
    }
    
    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        var quizzes = _service.FindAllQuizzes();
        var dtoList = quizzes.Select(QuizDto.of);
        return dtoList;
    }
    
    [HttpPost]
    [Route("{quizId}/items/{itemId}")]
    public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    {
        _service.SaveUserAnswerForQuiz(dto.UserId, quizId, itemId, dto.Answer);
    }
    
    [HttpGet]
    [Route("{quizId}/users/{userId}/result")]
    public ActionResult<QuizResultDto> GetQuizResult(int quizId, int userId)
    {
        var correctAnswers = _service.CountCorrectAnswersForQuizFilledByUser(userId, quizId);

        var result = new QuizResultDto
        {
            UserId = userId,
            QuizId = quizId,
            CorrectAnswersCount = correctAnswers
        };

        return Ok(result);
    }
}