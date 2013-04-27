using UnityEngine;
using System.Collections;

public class BaseText : MonoBehaviour {

  public Game game;

  // Update is called once per frame
  void Update() {
    guiText.text = string.Format("BASE: {0}", game.baseHP);
  }
}
