using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages
{
    public class QuizList : PageModel
    {
        private readonly IQuizAdminService _quizAdminService;
        private readonly ILogger<QuizList> _logger;

        public QuizList(IQuizAdminService quizAdminService, ILogger<QuizList> logger)
        {
            _quizAdminService = quizAdminService;
            _logger = logger;
        }

        public List<ApplicationCore.Models.QuizAggregate.Quiz> Quizzes { get; set; }

        public void OnGet()
        {
            try
            {
                Quizzes = _quizAdminService.FindAllQuizzes();
                
                _logger.LogInformation("Retrieved {Count} quizzes", Quizzes.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quizzes");
                ModelState.AddModelError(string.Empty, "Error loading quizzes. Please try again later.");
            }
        }
    }
}