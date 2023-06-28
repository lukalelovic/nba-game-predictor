using Microsoft.ML;
using Microsoft.ML.Data;

public class GamePrediction {
  [ColumnName("PredictedLabel")]
  public bool HomeTeamWins;
}

public class Program {
  static void Main() {
    var ctx = new MLContext();

    var teamDataView = ctx.Data.LoadFromTextFile<GameData>("data/game_data.csv", hasHeader: true, separatorChar: ',');
    
    var splitData = ctx.Data.TrainTestSplit(teamDataView, testFraction: 0.2);

    // Prepare the data
    var pipeline = ctx.Transforms.Text.FeaturizeText("H_TEAM", "HOME_TEAM_NAME")
      .Append(ctx.Transforms.Text.FeaturizeText("V_TEAM", "VISITOR_TEAM_NAME"))
      .Append(ctx.Transforms.Concatenate("Features", "H_TEAM", "V_TEAM"))
      .Append(ctx.Transforms.NormalizeMinMax("Features"))
      .Append(ctx.BinaryClassification.Trainers.LbfgsLogisticRegression());

    // Train the model
    var model = pipeline.Fit(splitData.TrainSet);
    
    // Make predictions with the model
    var predictions = model.Transform(splitData.TestSet);

    // Retrieve prediction metrics
    var metrics = ctx.BinaryClassification.Evaluate(predictions);
    Console.WriteLine($"Accuracy - {metrics.Accuracy}");
    Console.WriteLine();

    // Prompt for team names
    Console.WriteLine("Enter the home team name:");
    var homeTeamName = Console.ReadLine();

    Console.WriteLine("Enter the visitor team name:");
    var visitorTeamName = Console.ReadLine();

    // Create a new game with given names
    var newGame = new GameData {
      HOME_TEAM_NAME = homeTeamName,
      VISITOR_TEAM_NAME = visitorTeamName
    };

    // Make prediction on the new game
    var predictionEngine = ctx.Model.CreatePredictionEngine<GameData, GamePrediction>(model);
    var prediction = predictionEngine.Predict(newGame);

    Console.WriteLine($"{homeTeamName} are predicted to {(prediction.HomeTeamWins ? "win" : "lose")} against the {visitorTeamName}");
  }
}