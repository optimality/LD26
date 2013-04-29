using UnityEngine;
using System.Collections;

public class NextWaveText : MonoBehaviour {
  public Game game;
  public GameObject totalScore;

  public string bestString;
  public string goodString;
  public string badString;
  public float goodThreshold;

  void Update() {
    if (game.wave == game.waves.Length) {
      totalScore.SetActive(false);
    } else {
      totalScore.SetActive(true);
    }

    if (game.wave <= 0) {
      guiText.text = string.Format("LEVEL COMPLETE: {0}", FinalRating());
    } else {
      guiText.text = string.Format("CLICK FOR WAVE {0}", game.wave);
    }
  }

  string FinalRating() {
    int finalTarget = 0;
    foreach (int target in game.levelScores[game.level - 1].waveScore) {
      finalTarget += target;
    }
    
    if (game.score <= finalTarget) {
      return bestString;
    } else if (game.score <= finalTarget * goodThreshold) {
      return goodString;
    } else {
      return badString;
    }
  }
}
