using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;


public class SummaryModel : PageModel
{
    private readonly IQuizUserService _userService;

    public SummaryModel(IQuizUserService userService)
    {
        _userService = userService;
    }

    public int CorrectAnswers { get; set; }

    public void OnGet(int quizId, int userId = 1)
    {
        CorrectAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
    }
}
