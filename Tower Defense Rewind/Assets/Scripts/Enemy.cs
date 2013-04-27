using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  public float speed;

  [HideInInspector]
  public float age;

  private Game game;
  private Vector3 target;

  public void Remove() {
    game.RemoveEnemy();
    Destroy(this.gameObject);
  }

  void Start() {
    target = new Vector3(transform.position.x, transform.position.y, 0);
    game = (Game)FindObjectOfType(typeof(Game));
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
