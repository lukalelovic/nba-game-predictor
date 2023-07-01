using Microsoft.ML.Data;

public class PredictionResult {
  [ColumnName("PredictedLabel")]
  public bool HomeTeamWins;
}