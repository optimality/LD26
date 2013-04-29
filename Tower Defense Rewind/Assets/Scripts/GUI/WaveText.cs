using UnityEngine;
using System.Collections;

public class WaveText : MonoBehaviour {
  public Game game;
  public int wave;

  void Update() {
    if (game.wave - wave < 1) {
      guiText.text = "";
    } else if (wave == 0) {
      guiText.text = string.Format("{0}", game.enemies);
    } else {
      guiText.text = string.Format("{0}", game.waves[game.wave - wave - 1]);
    }
  }
}
