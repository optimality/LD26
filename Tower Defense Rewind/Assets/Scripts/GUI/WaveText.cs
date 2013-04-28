using UnityEngine;
using System.Collections;

public class WaveText : MonoBehaviour {
  public Game game;
  public int wave;

  void Update() {
    if (game.wave >= wave) {
      guiText.text = string.Format("{0}", game.waves[wave - 1]);
    } else {
      guiText.text = "";
    }
  }
}
