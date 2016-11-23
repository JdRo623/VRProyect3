using UnityEngine;
using System.Collections;

public class RotationHandler: MovementState {
    public Vector3 movementVector;
    public float speedSides;
    private Rigidbody rg;
    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }
    void Awake() {
        Application.targetFrameRate = 30;
    }
	// Use this for initialization
	void OnEnable () {
       movementVector=new Vector3(0,0,0);
    }

   void Update () {
        MoveSides();
    }

    void MoveSides() { 
        movementVector.y = Camera.main.transform.rotation.x * speedSides * -1 * Time.deltaTime;
        movementVector.x = Camera.main.transform.rotation.z * speedSides*-1*Time.deltaTime;
        this.transform.Translate( movementVector);
    }
}
