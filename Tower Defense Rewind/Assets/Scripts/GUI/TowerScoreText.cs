using UnityEngine;
using System.Collections;

public class TowerScoreText : MonoBehaviour {
  public Game game;
  void Update() {
    guiText.text = string.Format("TOWERS {0} X {1} = {2}", game.towers, game.towerPoints,
      game.towers * game.towerPoints);
  }
}
