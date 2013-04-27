using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("SCORE: {0}", game.score);
  }
}
