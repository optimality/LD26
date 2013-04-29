using UnityEngine;
using System.Collections;

public class TowerScoreText : MonoBehaviour {
  public Game game;
  void Update() {
    guiText.text = string.Format("{0}", game.numTowers * game.towerPoints);
  }
}
