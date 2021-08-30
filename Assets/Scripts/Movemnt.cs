using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Incorporate jump system into movement. 
[RequireComponent(typeof(Rigidbody2D))]
public class Movemnt : MonoBehaviour {
    public float speed = 10f;
    public float damping = 6f;
    public float velocityLimit = 25f;
    private float horizontalAxis;
    private float verticalAxis;
    private bool canJump;
    private bool jumping;
    private Vector2 velocity;
	private Rigidbody2D r2d;
	// Use this for initialization
	void Start() {
		r2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
	}

    //TODO: Breakup into a vertical movement and horizontal movement function so we can limit how the user controls vertical movement
    //Create a maximum velocity y limit and then wait till it hit ground? That way jumps can be varried, small/medium/large, also to limit the input into one keydown. 
    void FixedUpdate() {
        var movementDir = new Vector2(horizontalAxis, verticalAxis).normalized;
        velocity.x = Mathf.Lerp(velocity.x, movementDir.x * speed, Time.fixedDeltaTime * damping) > velocityLimit ? velocityLimit : Mathf.Lerp(velocity.x, movementDir.x * speed, Time.fixedDeltaTime * damping);
        if (canJump) {
            if (verticalAxis <= 0) {
                canJump = false;
            }
            if (velocity.y > velocityLimit) {
                canJump = false;
            }
            var lerpRes = Mathf.Lerp(velocity.y, movementDir.y * speed, Time.fixedDeltaTime * damping);
            if (velocity.y >= velocityLimit) {
                canJump = false;
            } else {
                velocity.y = lerpRes;
            }
        } else {
            if (velocity.y > 0) {
            velocity.y = velocity.y - 0.5f;
            } else {
                if (velocity.y != 0) {
                    velocity.y = 0;
                }
            }
        }
        r2d.MovePosition(r2d.position + velocity * Time.fixedDeltaTime);
    }

    void OnCollisionStay2D() {
        canJump = true;     
    }
}