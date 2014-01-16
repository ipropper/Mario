using UnityEngine;
using System.Collections;



public class mario_move : MonoBehaviour {

	// Use this for initialization
	// new keys
	public KeyCode Jump;


	public float JumpHeight = 3.0f;
	public float JumpMore = 3.0f;
	public float RightLeft = 3.0f;

	//used for line cast
	public GameObject GroundCheck;
	bool grounded = true;

	//used to change left/right movement
	int cap = 300;
	float origin;

	//used for holding jump
	bool more_jump = true;

	void Start()
	{
		origin = RightLeft;
	}
	
	// Update is called once per frame
	void Update () {

		// creates a line cast between player and empty object, if "Ground" layer comes in contact with line allows jump 
		Vector2 groundPos = new Vector2(GroundCheck.transform.position.x, GroundCheck.transform.position.y);
		Vector2 playePos = new Vector2(transform.position.x, transform.position.y);
		grounded = Physics2D.Linecast(playePos , groundPos , 1 << LayerMask.NameToLayer("Ground"));


		if(Input.GetKeyDown(Jump) && grounded)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpHeight);
			more_jump = true;
		}


		if(Input.GetKey(Jump) && !grounded && more_jump)
		{
			print("meow");
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpHeight + JumpMore);
			more_jump = false;
		}

		// movement left and right
		if(Input.GetButton("Horizontal"))
		{
			float axis = Input.GetAxis("Horizontal");
			// if both left and right are pressed mario does an awkward moon walk thing.
			if(axis == 0){
				rigidbody2D.velocity = new Vector2(.1f,rigidbody2D.velocity.y);
			}
			else{ rigidbody2D.velocity = new Vector2((RightLeft * axis), rigidbody2D.velocity.y); }
		}

		//refresh rightleft when no longer walking
		if(Input.GetButtonUp("Horizontal"))
		{
			RightLeft = origin;
		}
	}
}
