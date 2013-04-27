using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
  public float speed;

  void Update() {
    Vector3 heading = transform.rotation * Vector3.forward;
    transform.position += heading * speed * Time.deltaTime;
  }

  void OnTriggerEnter(Collider collider) {
    Enemy enemy = collider.GetComponent<Enemy>();
    enemy.Remove();
    Destroy(this.gameObject);
  }
}
