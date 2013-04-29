using UnityEngine;
using System.Collections;

public class BaseScoreText : MonoBehaviour {
  public Game game;
  void Update() {
    guiText.text = string.Format("{0}", game.baseHP * game.basePoints);
  }
}
