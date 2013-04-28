using UnityEngine;
using System.Collections;

public class LowestScore : MonoBehaviour {
  public Game game;

  void Update() {
    if (game.lowestScore.HasValue) {
      guiText.text = string.Format("{0}", game.lowestScore);
    } else {
      guiText.text = "none yet!";
    }
  }
}
