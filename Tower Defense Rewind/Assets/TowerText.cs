using UnityEngine;
using System.Collections;

public class TowerText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("TOWERS: {0}", game.towers);
  }
}
