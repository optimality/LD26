using UnityEngine;
using System.Collections;

public class BaseScoreText : MonoBehaviour {
  public Game game;
  void Update() {
    guiText.text = string.Format("BASE {0} X {1} = {2}", game.baseHP, game.basePoints,
      game.baseHP * game.basePoints);
  }
}
