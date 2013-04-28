using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
  public GameObject projectilePrefab;
  public float range;
  public LayerMask enemyLayer;
  public float fireTime;
  public float fireHeight;

  public AudioSource laser;

  private float fireCooldown;
  private Game game;

  public void Remove() {
    game.towerRemove.Play();
    Destroy(this.gameObject);
  }

  void Start() {
    game = transform.parent.GetComponent<Game>();
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
        Fire(FindInterceptPoint(projectilePrefab.GetComponent<Projectile>().speed, oldestEnemy));
      }
    }
  }

  // Math helpfully taken from Alvaro's work on gamedev.net.
  // http://www.gamedev.net/topic/401165-target-prediction-system--target-leading/
  Vector3 FindInterceptPoint(float projectileSpeed, Enemy enemy) {
    Vector3 enemyVelocity = (enemy.target - enemy.transform.position).normalized * enemy.speed;
    Vector3 vectorToEnemy = enemy.transform.position - transform.position;
    float a = projectileSpeed * projectileSpeed - enemy.speed * enemy.speed;
    float b = -2 * Vector3.Dot(enemyVelocity, vectorToEnemy);
    float c = -vectorToEnemy.sqrMagnitude;
    float timeToIntercept = (b + Mathf.Sqrt(b * b - 4 * a *c)) / (2 * a);
    return timeToIntercept * enemyVelocity + enemy.transform.position;
  }

  void Fire(Vector3 location) {
    laser.Play();
    fireCooldown = 0;
    GameObject projectile = (GameObject)Instantiate(projectilePrefab,
          transform.position + new Vector3(0, 0, fireHeight),
          Quaternion.LookRotation(location - transform.position));
    projectile.transform.parent = transform;
  }
}
