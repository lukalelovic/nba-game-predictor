using Microsoft.ML;

public class GamePredictionModel {
  private readonly MLContext ctx;
  private ITransformer? model;

  public GamePredictionModel() {
    ctx = new MLContext();
    TrainModel("data/game_data.csv");
  }

  private void TrainModel(string dataFilePath) {
    var teamDataView = ctx.Data.LoadFromTextFile<Game>(dataFilePath, hasHeader: true, separatorChar: ',');

    var splitData = ctx.Data.TrainTestSplit(teamDataView, testFraction: 0.2);

    // Prepare the data
    var pipeline = ctx.Transforms.Text.FeaturizeText("H_TEAM", "HOME_TEAM_NAME")
        .Append(ctx.Transforms.Text.FeaturizeText("V_TEAM", "VISITOR_TEAM_NAME"))
        .Append(ctx.Transforms.Concatenate("Features", "H_TEAM", "V_TEAM"))
        .Append(ctx.Transforms.NormalizeMinMax("Features"))
        .Append(ctx.BinaryClassification.Trainers.LbfgsLogisticRegression());

    // Train the model
    model = pipeline.Fit(splitData.TrainSet);

    // Make predictions with the model
    // var predictions = model.Transform(splitData.TestSet);

    // Retrieve prediction metrics
    // var metrics = ctx.BinaryClassification.Evaluate(predictions);
    // Console.WriteLine($"Accuracy - {metrics.Accuracy}");
    // Console.WriteLine();
  }

  public bool PredictGameOutcome(string homeTeamName, string visitorTeamName) {
    var gameData = new Game {
      HOME_TEAM_NAME = homeTeamName,
      VISITOR_TEAM_NAME = visitorTeamName
    };

    var predictionEngine = ctx.Model.CreatePredictionEngine<Game, PredictionResult>(model);
    var prediction = predictionEngine.Predict(gameData);

    return prediction.HomeTeamWins;
  }
}