using UnityEngine;
using System.Collections;

public class StateMoveForward : MovementState {
    public Vector3 movementVector;
    public float speedFoward;
    private float speed;
    private Rigidbody rg;
    // Use this for initialization
    void Start () {
        rg = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void FixedUpdate () {
        Camera.main.transform.position = this.transform.position;
        movementVector.z = speedFoward * Time.deltaTime;
        rg.MovePosition(this.transform.position+movementVector);
    }
}
