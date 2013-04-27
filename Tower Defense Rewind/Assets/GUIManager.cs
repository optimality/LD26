using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

  public GameObject[] gameScreen;
  public GameObject[] loseScreen;
  public GameObject[] winScreen;

  public void StartGame() {
    foreach (Transform t in transform) {
      t.gameObject.SetActive(false);
    }
    foreach (GameObject o in gameScreen) {
      o.SetActive(true);
    }
  }

  public void WinGame() {
    foreach (Transform t in transform) {
      t.gameObject.SetActive(false);
    }
    foreach (GameObject o in winScreen) {
      o.SetActive(true);
    }
  }

  public void LoseGame() {
    foreach (Transform t in transform) {
      t.gameObject.SetActive(false);
    }
    foreach (GameObject o in loseScreen) {
      o.SetActive(true);
    }
  }
}
