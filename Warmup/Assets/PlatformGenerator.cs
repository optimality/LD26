using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour {

  public GameObject player;
  public GameObject platformPrefab;
  public int platformsToGenerate;
  public int historyDistance;

  public int minHeight;
  public int maxHeight;

  public int minWidth;
  public int maxWidth;

  public int gapSize;

  private Queue<GameObject> platforms = new Queue<GameObject>();
  private int horizontalOffset = 0;
  private int previousHeight = 0;

  void Start() {
    for (int i = 0; i < platformsToGenerate; ++i) {
      GeneratePlatform();
    }
    player.transform.position = new Vector3(0, platforms.Peek().transform.position.y + 1, 0);
  }

  void Update() {
    // Delete old platforms.
    while (player.transform.position.x > platforms.Peek().transform.position.x + historyDistance) {
      platforms.Dequeue();
    }

    // Add new platforms.
    while (platforms.Count < platformsToGenerate) {
      GeneratePlatform();
    }
  }

  void GeneratePlatform() {
    int height = Random.Range(minHeight, maxHeight) + previousHeight;
    int width = Random.Range(minWidth, maxWidth);

    GameObject platform = (GameObject)Instantiate(platformPrefab,
      new Vector3(horizontalOffset, height, 0),
      Quaternion.identity);
    platform.transform.localScale = new Vector3(width, 1, 1);
    platforms.Enqueue(platform);
    horizontalOffset += width + gapSize;
  }
}
