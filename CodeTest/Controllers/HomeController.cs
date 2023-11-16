using CodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using CodeTest.Helpers;
using System.Diagnostics;

namespace CodeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string ExampleInput = "0 1 0 0 0\r\n1 0 0 1 1\r\n1 1 0 0 1\r\n0 1 0 0 0\r\n1 0 0 0 1";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new GameViewModel()
            {
                Input = ExampleInput,
                Result = GenerateNewBoard(ExampleInput)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Process(GameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var result = GenerateNewBoard(model.Input);
            if (result == null)
            {
                ModelState.AddModelError(nameof(model.Input), "Make sure each row and each column have at least two elements, and there is the same element count in each row.");
                return View("Index", model);
            }
            
            model.Result = result;

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string? GenerateNewBoard(string oldBoard)
        {
            var cells = oldBoard.ParseGameBoard();
            if (cells == null)
            {
                return null;
            }

            var result = new bool[cells.GetLength(0), cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    result[i, j] = cells.GetCellState(i, j);
                }
            }

            return result.GameBoardToString();
        }
    }
}