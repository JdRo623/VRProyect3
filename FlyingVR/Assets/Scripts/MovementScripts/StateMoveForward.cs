using UnityEngine;
using System.Collections;

public class StateMoveForward : MovementState {
    public Vector3 movementVector;
    public float speedFoward;

    private Rigidbody rg;
    // Use this for initialization
    void Start () {
        rg = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = this.transform.position;
        movementVector.z = speedFoward * Time.deltaTime;
        this.transform.Translate(movementVector);
    }
}
