using UnityEngine;
using System.Collections;

public class CopyFromTextFile : MonoBehaviour {
  public TextAsset instructions;

  void Start() {
    guiText.text = instructions.text;
  }
}
