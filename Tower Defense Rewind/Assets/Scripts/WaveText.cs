using UnityEngine;
using System.Collections;

public class WaveText : MonoBehaviour {
  public Game game;

  void Update() {
    guiText.text = string.Format("WAVE 1: {0}", game.enemies);
  }
}
