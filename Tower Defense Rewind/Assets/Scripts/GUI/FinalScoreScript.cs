using UnityEngine;
using System.Collections;

public class FinalScoreScript : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("FINAL SCORE: {0}", game.score);
  }
}
