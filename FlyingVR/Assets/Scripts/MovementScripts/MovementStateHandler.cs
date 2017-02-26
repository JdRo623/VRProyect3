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
    private float smokeImpulse;
    private float smokeSpeed;
    public float sideSpeed;
    private float calculatedSideSpeed;
    public float sideBoundaryImpulse;
    private float sideBoundarySpeed;
    private int sideBoundaryDirection;
    delegate void MovementState();
    MovementState YaxisMovementState;
    MovementState XaxisMovementState;
    private float timeCounter;
    private float initialTime;
    private GameHandler gh;
	string LBoundary="LeftBoundary";
	string RBoundary = "RigthBoundary";
	string UBoundary= "UpBoundary";
	string DBoundary="DownBoundary";
    // Use this for initialization
    void Start()
    {

        gh = FindObjectOfType<GameHandler>();
        timeCounter = 0;
        initialTime = 0;
        player = GetComponent<Rigidbody>();
        YaxisMovementState += FreeFallMovement;
        XaxisMovementState += FreeStyleMovement;
        Camera.main.transform.rotation =Camera.main.GetComponentInParent<Transform>().rotation;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (gh.isEnableToMove) {
            MovementCalculations();
        }
    }
	void Update(){
	    Vector3 playerPosition = this.transform.position;
	    Camera.main.transform.position = playerPosition;
	}
    void MovementCalculations() {
        calculatedSpeedFoward = speedFoward * Time.deltaTime;
        calculatedSpeedFalling = -fallingSpeed * Time.deltaTime;
        calculatedSideSpeed = sideSpeed * Time.deltaTime;
        foward = this.transform.forward * calculatedSpeedFoward;
        YaxisMovementState();
        XaxisMovementState();

        player.MovePosition(this.transform.position + (foward + side + down) * calculatedSpeedFoward);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(RBoundary)){
            sideBoundaryDirection = -1;
            sideBoundarySpeed = sideBoundaryImpulse * Time.deltaTime;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.gameObject.CompareTag(LBoundary)) {
            sideBoundarySpeed = sideBoundaryImpulse *Time.deltaTime ;
            sideBoundaryDirection = 1;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.gameObject.CompareTag(UBoundary)) {
            SmokeImpulse();
        } else if (other.tag == "Smoke") {
            smokeImpulse = other.GetComponent<SmokeAtributes>().smokeImpulse; 
            smokeSpeed = (smokeImpulse+fallingSpeed)*Time.deltaTime;
            YaxisMovementState -= FreeFallMovement;
            YaxisMovementState += SmokeImpulse;
        }
        else if (other.gameObject.CompareTag(DBoundary) )
        {
                  ReloadLevel();
        }
    }
    void SmokeImpulse() {
        timeCounter += Time.deltaTime;
        smokeSpeed = Mathf.Lerp(smokeSpeed, calculatedSpeedFalling, Time.deltaTime);
        down = this.transform.up * smokeSpeed;
        if (smokeSpeed <= (calculatedSpeedFalling - 0.1f) || (timeCounter>1.7)) {
            smokeSpeed = 0;
            calculatedSpeedFalling = 0;
            timeCounter = 0; 
            YaxisMovementState -= SmokeImpulse;
            YaxisMovementState += FreeFallMovement;
        }
    }
    void FreeStyleMovement() {
		side = new Vector3(0,0,1) * (Camera.main.transform.localRotation.z) * -calculatedSideSpeed;
    }
    void FreeFallMovement() {
        down = this.transform.up * calculatedSpeedFalling;
    }
    void SideBoundaryImpulse() {
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
