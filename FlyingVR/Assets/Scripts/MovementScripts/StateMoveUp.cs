using UnityEngine;
using System.Collections;

public class StateMoveUp : MovementState {
    public Vector3 movementVector;
    public float speedUp;
    public float resetTime;
    // Use this for initialization
    void OnEnable() {
        movementVector = new Vector3(0, 0, 0);
        Invoke("resetState", resetTime);
    }
    void resetState()
    {
        GetComponentInChildren<MovementStateHandler>().initConfig();
    }
    // Update is called once per frame
    void Update () {
        movementVector.y = speedUp * Time.deltaTime;
        this.transform.Translate(movementVector);
    }

}
