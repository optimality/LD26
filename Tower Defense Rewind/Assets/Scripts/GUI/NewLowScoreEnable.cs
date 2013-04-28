using UnityEngine;
using System.Collections;

public class NewLowScoreEnable : MonoBehaviour {
  public Game game;
  public GameObject newLowScore;

  void Update() {
    newLowScore.SetActive(game.newLowestScore);  
  }
}
