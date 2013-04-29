using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
  public Game game;
  public Color healthyColor;
  public Color deadColor;

  void Start() {
    game = transform.parent.GetComponent<Game>();
  }

  // Update is called once per frame
  void Update() {
    renderer.material.color = Color.Lerp(deadColor, healthyColor,
      (float)game.baseHP / (float)game.startingBaseHP);
  }
}
