using Microsoft.ML.Data;

public class Game {
  [LoadColumn(0)]
  public string? GAME_DATE_EST;

  [LoadColumn(1)]
  public float GAME_ID;

  [LoadColumn(2)]
  public string? GAME_STATUS_TEXT;

  [LoadColumn(3)]
  public int SEASON;

  [LoadColumn(4)]
  public int HOME_TEAM_ID;

  [LoadColumn(5)]
  public float PTS_home;

  [LoadColumn(6)]
  public float FG_PCT_home;

  [LoadColumn(7)]
  public float FT_PCT_home;

  [LoadColumn(8)]
  public float FG3_PCT_home;

  [LoadColumn(9)]
  public float AST_home;

  [LoadColumn(10)]
  public float REB_home;

  [LoadColumn(11)]
  public int AWAY_TEAM_ID;

  [LoadColumn(12)]
  public float PTS_away;

  [LoadColumn(13)]
  public float FG_PCT_away;

  [LoadColumn(14)]
  public float FT_PCT_away;

  [LoadColumn(15)]
  public float FG3_PCT_away;

  [LoadColumn(16)]
  public float AST_away;

  [LoadColumn(17)]
  public float REB_away;

  [LoadColumn(18), ColumnName("Label")]
  public bool HOME_TEAM_WINS;

  [LoadColumn(19)]
  public string? HOME_TEAM_NAME;

  [LoadColumn(20)]
  public string? VISITOR_TEAM_NAME;
}