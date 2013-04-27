using UnityEngine;
using System.Collections;

public class SpeedChanger : MonoBehaviour {
  public Vector3 speedChange;

  void OnCollisionStay(Collision collision) {
    collision.collider.rigidbody.velocity += speedChange * Time.deltaTime;
  }

}
