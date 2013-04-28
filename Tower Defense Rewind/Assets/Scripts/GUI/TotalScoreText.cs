using UnityEngine;
using System.Collections;

public class TotalScoreText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("TOTAL SCORE: {0}", game.score);
  }
}
