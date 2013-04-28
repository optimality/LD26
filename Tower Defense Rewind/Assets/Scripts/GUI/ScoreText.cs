using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("{0}", game.score);
  }
}
