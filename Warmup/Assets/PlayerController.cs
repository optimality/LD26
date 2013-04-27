using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  public float jumpSpeed;
  public float maxJumpSpeed;

  private bool onGround = false;
  private bool jumping = false;

  void OnCollisionEnter(Collision collision) {
    onGround = true;
  }

  void OnCollisionExit(Collision collision) {
    onGround = false;
  }

  void Update() {
    if (onGround && Input.GetButtonDown("Jump") || jumping && Input.GetButton("Jump")) {
      rigidbody.velocity += jumpSpeed * Vector3.up * Time.deltaTime;
      jumping = rigidbody.velocity.y < maxJumpSpeed;
    } else if (jumping) {
      jumping = false;
    }
  }
}
