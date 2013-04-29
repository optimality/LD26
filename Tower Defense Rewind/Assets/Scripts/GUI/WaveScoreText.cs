using UnityEngine;
using System.Collections;

public class WaveScoreText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("{0}", game.WaveScore());
  }
}
