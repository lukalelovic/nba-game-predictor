using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller {
  private readonly GamePredictionModel predictionModel;

  public HomeController() {
    predictionModel = new GamePredictionModel();
  }

  [HttpGet]
  public IActionResult Index() {
    return View();
  }

  [HttpPost]
  public IActionResult Predict(string homeTeamName, string visitorTeamName) {
    var prediction = predictionModel.PredictGameOutcome(homeTeamName, visitorTeamName);
    return Content($"{homeTeamName} are predicted to {(prediction ? "win" : "lose")} against the {visitorTeamName}");
  }

  public IActionResult Error() {
    var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;

    Console.WriteLine();
    Console.Error.WriteLine(exception);

    return View("Error");
  }
}