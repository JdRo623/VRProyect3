using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MovementStateHandler : MonoBehaviour
{
    public UnityEvent EndedDelegate;

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
    private Vector3 previousPosition;
    delegate void MovementState();
    MovementState YaxisMovementState;
    MovementState XaxisMovementState;
    private float timeCounter;
    private float initialTime;
    private bool hasFinished;
    private GameHandler gh;
	string LBoundary="LeftBoundary";
	string RBoundary = "RightBoundary";
	string UBoundary= "UpBoundary";
	string DBoundary="DownBoundary";
    string Smoke = "Smoke";
    string EndGame = "EndGame";
    public float walkingTime; 
    private float countWalkingTime;
    private Vector3 neutralVector;
    public Transform contentMover;
    public Transform freeStyleChildCameraPoint;

    // Use this for initializ
    
    void Start()
    {
        neutralVector = new Vector3(0,0,0);
        hasFinished = false;
        gh = FindObjectOfType<GameHandler>();
        timeCounter = 0;
        initialTime = 0;
        player = GetComponent<Rigidbody>();
        YaxisMovementState += FreeFallMovement;
        XaxisMovementState += FreeStyleMovement;
        
        contentMover.rotation = contentMover.parent.rotation;
        countWalkingTime = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gh.isEnableToMove && !hasFinished) 
            MovementCalculations();
    }

	void Update()
    {
	    Vector3 playerPosition = this.transform.position;
        if (previousPosition != playerPosition)
        {
            contentMover.position = playerPosition;
            previousPosition = playerPosition;
        }
	    
	}
    void MovementCalculations()
    {    
        calculatedSpeedFalling = -fallingSpeed * Time.deltaTime;
        calculatedSideSpeed = sideSpeed * Time.deltaTime;
        YaxisMovementState();
        XaxisMovementState();
        foward = this.transform.forward * calculatedSpeedFoward;
        player.MovePosition(this.transform.position + (foward + side + down) * calculatedSpeedFoward);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(RBoundary))
        {
            sideBoundaryDirection = -1;
            sideBoundarySpeed = sideBoundaryImpulse * Time.deltaTime;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.gameObject.CompareTag(LBoundary))
        {
            sideBoundarySpeed = sideBoundaryImpulse * Time.deltaTime;
            sideBoundaryDirection = 1;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += SideBoundaryImpulse;
        }
        else if (other.gameObject.CompareTag(UBoundary))
            SmokeImpulse();
        else if (other.gameObject.CompareTag(Smoke))
        {
            SmokeAtributes atributes = other.GetComponent<SmokeAtributes>();
            smokeImpulse = atributes.smokeImpulse;
            atributes.smokeAudio.Play();
            smokeSpeed = (smokeImpulse + fallingSpeed) * Time.deltaTime;
            YaxisMovementState -= FreeFallMovement;
            YaxisMovementState += SmokeImpulse;
        }
        else if (other.gameObject.CompareTag(DBoundary))
            ReloadLevel();
        else if (other.gameObject.CompareTag(EndGame))
        {
            EndedDelegate.Invoke();
            hasFinished = true;
            YaxisMovementState -= FreeFallMovement;
            YaxisMovementState += StayStill;
            XaxisMovementState -= FreeStyleMovement;
            XaxisMovementState += StayStillX;
        }
    }

    void SmokeImpulse()
    {
        timeCounter += Time.deltaTime;
        smokeSpeed = Mathf.Lerp(smokeSpeed, calculatedSpeedFalling, Time.deltaTime);
        down = this.transform.up * smokeSpeed;
        if (smokeSpeed <= (calculatedSpeedFalling - 0.1f) || (timeCounter>1.7))
        {
            smokeSpeed = 0;
            calculatedSpeedFalling = 0;
            timeCounter = 0; 
            YaxisMovementState -= SmokeImpulse;
            YaxisMovementState += FreeFallMovement;
        }
    }

    void StayStill()
    {
        if (down != neutralVector) 
            down = neutralVector;
    }

    void StayStillX()
    {
        calculatedSpeedFoward = Mathf.Lerp(calculatedSpeedFoward, 0, Time.deltaTime);
    }

    void FreeStyleMovement()
    {
        calculatedSpeedFoward = speedFoward * Time.deltaTime;
        side = new Vector3(0,0,1) * (freeStyleChildCameraPoint.parent.localRotation.z) * -calculatedSideSpeed;
    }

    void FreeFallMovement()
    {
        down = this.transform.up * calculatedSpeedFalling;
    }

    void SideBoundaryImpulse()
    {
        sideBoundarySpeed = Mathf.Lerp(sideBoundarySpeed, 0, Time.deltaTime);
        side = transform.right * sideBoundarySpeed * sideBoundaryDirection;
        
        if (sideBoundarySpeed <= 0.08f)
        {
            XaxisMovementState -= SideBoundaryImpulse;
            XaxisMovementState += FreeStyleMovement;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
