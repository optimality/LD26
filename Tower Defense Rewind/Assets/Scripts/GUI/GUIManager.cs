using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
  public GameObject[] startScreen;
  public GameObject[] levelScreen;
  public GameObject[] gameScreen;
  public GameObject[] surviveScreen;
  public GameObject[] loseScreen;
  public GameObject[] waveScreen;

  void EnableScreen(GameObject[] screen) {
    foreach (Transform t in transform) {
      t.gameObject.SetActive(false);
    }
    foreach (GameObject o in screen) {
      o.SetActive(true);
    }
  }

  public void StartGame() {
    EnableScreen(startScreen);
  }

  public void SelectLevel() {
    EnableScreen(levelScreen);
  }

  public void PlayGame() {
    EnableScreen(gameScreen);
  }

  public void SurviveGame() {
    EnableScreen(surviveScreen);
  }

  public void LoseGame() {
    EnableScreen(loseScreen);
  }

  public void NextWave() {
    EnableScreen(waveScreen);
  }
}
