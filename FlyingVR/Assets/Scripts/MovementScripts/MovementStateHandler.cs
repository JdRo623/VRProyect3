using UnityEngine;
using System.Collections;

public class MovementStateHandler : MonoBehaviour
{
    public Vector3 movementVector;
    public float speedFoward;
    private float calculatedSpeedFoward;
    private Vector3 foward;
    private Vector3 side;
    private Vector3 down;
    private Rigidbody player;
    public float fallingSpeed;
    private float calculatedSpeedFalling;
    public float smokeImpulse;
    private float smokeSpeed;
    public float sideSpeed;
    private float calculatedSideSpeed;
    public float sideBoundaryImpulse;
    private float sideBoundarySpeed;
    private int sideBoundaryDirection;
    delegate void MovementState();
    MovementState YaxisMovementState;
    MovementState XaxisMovementState;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody>();
        YaxisMovementState += FreeFallMovement;
        XaxisMovementState += FreeStyleMovement;
        Camera.main.transform.rotation = Camera.main.GetComponentInParent<Transform>().rotation;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position = this.transform.position;
        calculatedSpeedFoward = speedFoward * Time.deltaTime;
        calculatedSpeedFalling = -fallingSpeed * Time.deltaTime;
        calculatedSideSpeed = sideSpeed * Time.deltaTime;
        foward = this.transform.forward * calculatedSpeedFoward;
        YaxisMovementState();
        XaxisMovementState();

        player.MovePosition(this.transform.position + (foward + side + down) * calculatedSpeedFoward);
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "RigthBoundary") {
            sideBoundaryDirection = -1;
            sideBoundarySpeed = sideBoundaryImpulse * Time.deltaTime;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.tag == "LeftBoundary") {
            sideBoundarySpeed = sideBoundaryImpulse *Time.deltaTime ;
            sideBoundaryDirection = 1;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.tag == "UpBoundary") {
            SmokeImpulse();
        } else if (other.tag == "Smoke") {
            
            smokeSpeed = (smokeImpulse+fallingSpeed)*Time.deltaTime;
            YaxisMovementState -= FreeFallMovement;
            YaxisMovementState += SmokeImpulse;
        }
        else if (other.tag == "DownBoundary")
        {
                  ReloadLevel();
        }
    }
    void SmokeImpulse() {
        Debug.Log("Smoke" + smokeSpeed);
        smokeSpeed = Mathf.Lerp(smokeSpeed, calculatedSpeedFalling, Time.deltaTime);
        Debug.Log("Smoke"+smokeSpeed);
        down = this.transform.up * smokeSpeed;
        if (smokeSpeed <= (calculatedSpeedFalling - 0.1f)) {
            Debug.Log("Entro");
            YaxisMovementState -= SmokeImpulse;
            YaxisMovementState += FreeFallMovement;
        }
    }
    void FreeStyleMovement() {
        side = this.transform.right * (Camera.main.transform.localRotation.z) * -calculatedSideSpeed;
    }
    void FreeFallMovement() {
        down = this.transform.up * calculatedSpeedFalling;
    }
    void SideBoundaryImpulse() {
        Debug.Log("Falling");
        sideBoundarySpeed = Mathf.Lerp(sideBoundarySpeed, 0, Time.deltaTime);
        side = this.transform.right * sideBoundarySpeed * sideBoundaryDirection;
        if (sideBoundarySpeed <= 0.1f) {
            XaxisMovementState -= SideBoundaryImpulse;
            XaxisMovementState += FreeStyleMovement;
        }
    }
    void ReloadLevel() {
        Application.LoadLevel(Application.loadedLevelName);
    }
}