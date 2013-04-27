using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
  public GameObject enemyPrefab;
  public int numEnemies;
  public float spawnTime;

  private float spawnCooldown;

  void Start() {
    spawnCooldown = 0;
  }

  // Update is called once per frame
  void Update() {
    if (numEnemies > 0) {
      spawnCooldown += Time.deltaTime;
      if (spawnCooldown > spawnTime) {
        spawnCooldown -= spawnTime;
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, transform.position,
          enemyPrefab.transform.localRotation);
        enemy.transform.parent = transform;
        numEnemies--;
      }
    }
  }
}
