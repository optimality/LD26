using UnityEngine;
using System.Collections;

public class RatingScore : MonoBehaviour {
  public Game game;
  public float goodThreshold;

  public string bestString;
  public string goodString;
  public string badString;

  void Update() {
    int waveTarget = game.levelScores[game.level - 1].waveScore[game.waves.Length - 1 - game.wave];
    if (game.WaveScore() <= waveTarget) {
      guiText.text = bestString;
    } else if (game.WaveScore() <= waveTarget * goodThreshold) {
      guiText.text = goodString;
    } else {
      guiText.text = badString;
    }
  }
}
