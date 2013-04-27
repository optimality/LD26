using UnityEngine;
using System.Collections;

public class Scorekeeper : MonoBehaviour {
  public GameObject player;

  void Update () {
    guiText.text = ((int)player.transform.position.x).ToString();
  }
}
