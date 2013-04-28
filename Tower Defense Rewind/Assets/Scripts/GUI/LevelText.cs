using UnityEngine;
using System.Collections;

public class LevelText : MonoBehaviour {
  public Game game;
  public int level;

  private GUILayer guiLayer;

  void Start() {
    guiLayer = Camera.main.GetComponent<GUILayer>();
  }

  // Update is called once per frame
  void Update() {
    this.guiText.text = game.levels[level - 1].name;
    if (guiLayer.HitTest(Input.mousePosition) == this.guiText && Input.GetButtonDown("Click")) {
      game.LoadLevel(level);
    }
  }
}
