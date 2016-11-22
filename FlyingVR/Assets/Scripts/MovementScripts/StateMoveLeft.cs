using UnityEngine;
using System.Collections;

public class StateMoveLeft : MovementState {

    public Vector3 movementVector;
    public float speedLeft;
    public float resetTime;
    // Use this for initialization
    void OnEnable()
    {
        movementVector = new Vector3(0, 0, 0);
        Invoke("resetState", resetTime);
    }
    void resetState()
    {
        GetComponent<MovementStateHandler>().initConfig();
    }
    // Update is called once per frame
    void Update()
    {
        movementVector.x = speedLeft * Time.deltaTime*-1;
        this.transform.Translate(movementVector);
    }
}
