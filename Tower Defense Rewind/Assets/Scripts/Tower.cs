using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
  public GameObject projectilePrefab;
  public float range;
  public LayerMask enemyLayer;
  public float fireTime;
  public float fireHeight;

  private float fireCooldown;

  public void Remove() {
    Destroy(this.gameObject);
  }

  void Start() {
    fireCooldown = fireTime;
  }

  void Update() {
    fireCooldown += Time.deltaTime;
    if (fireCooldown >= fireTime) {
      Enemy oldestEnemy = null;
      float oldestAge = 0;
      Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, enemyLayer);
      foreach (Collider collider in collidersInRange) {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy.age > oldestAge) {
          oldestEnemy = enemy;
        }
      }

      if (oldestEnemy != null) {
        Fire(oldestEnemy.transform.position);
      }
    }
  }

  void Fire(Vector3 location) {
    fireCooldown = 0;
    GameObject projectile = (GameObject)Instantiate(projectilePrefab,
          transform.position + new Vector3(0, 0, fireHeight),
          Quaternion.LookRotation(location - transform.position));
    projectile.transform.parent = transform;
  }
}
