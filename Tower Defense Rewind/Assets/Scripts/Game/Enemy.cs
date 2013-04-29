using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  public float speed;

  [HideInInspector]
  public float age;
  [HideInInspector]
  public Vector3 target;

  private Game game;
  private bool removed;

  public void Remove() {
    if (removed) {
      return;
    }
    game.RemoveEnemy();
    Destroy(this.gameObject);
    removed = true;
  }

  void Start() {
    target = new Vector3(transform.position.x, transform.position.y, 0);
    game = (Game)FindObjectOfType(typeof(Game));
    removed = false;
  }

  void Update() {
    age += Time.deltaTime;
    Vector3 vectorToTarget = target - transform.position;
    float distanceToTarget = vectorToTarget.magnitude;
    float moveDistance = speed * Time.deltaTime;
    if (distanceToTarget < Mathf.Epsilon || moveDistance >= distanceToTarget) {
      transform.position = target;
      target = transform.position + 
        game.directionToMove[(int)transform.position.x, (int)transform.position.y];
      if (target == transform.position) {
        game.DamageBase();
        Remove();
      }
    } else {
      transform.position += vectorToTarget.normalized * moveDistance;
    }
  }
}
