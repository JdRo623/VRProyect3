using UnityEngine;
using System.Collections;

public class RotationHandler: MovementState {
    public Vector3 movementVector;
    public float speedSides;
	// Use this for initialization
	void OnEnable () {
       movementVector=new Vector3(0,0,0);
}

// Update is called once per frame
   void Update () {
        SetRotation();
        MoveSides();
    }
    void SetRotation() {
        this.transform.rotation = Camera.main.transform.rotation;
        /*Debug.Log(this.transform.rotation);
        Debug.Log("Camera"+new Quaternion(Camera.main.transform.rotation.x
    , Camera.main.transform.rotation.y
    , this.transform.rotation.z
    , this.transform.rotation.w));*/
    //this.transform.Translate(movementVector);
    }
    void MoveSides() { 
        movementVector.y = Camera.main.transform.rotation.x * speedSides * -1 * Time.deltaTime;
        movementVector.x = Camera.main.transform.rotation.z * speedSides*-1*Time.deltaTime;
    }
    void Move2CenterFromSides(int side) {

    }
    void OnTriggerEnter(Collider other) {
    }
}
