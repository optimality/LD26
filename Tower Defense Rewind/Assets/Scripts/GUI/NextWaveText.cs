using UnityEngine;
using System.Collections;

public class NextWaveText : MonoBehaviour {
  public Game game;
  public GameObject totalScore;

  void Update() {
    if (game.wave == game.waves.Length) {
      totalScore.SetActive(false);
    } else {
      totalScore.SetActive(true);
    }
    guiText.text = string.Format("WAVE {0} IN {1:D}", game.wave, (int)game.waveTimer);
  }
}
