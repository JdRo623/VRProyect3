using UnityEngine;
using System.Collections;

public class MovementStateHandler : MonoBehaviour
{
    public static GameObject mob;
    public GameObject player;
    private StateMoveDown moveDown;
    private StateMoveUp moveUp;
    private StateMoveRigth moveRigth;
    private StateMoveLeft moveLeft;
    private RotationHandler playerMovement;
    private StateMoveForward moveFoward;
    public MovementState currentState;
    // Use this for initialization
    void Awake() {
        mob = player;
        moveFoward = GetComponent<StateMoveForward>();
        moveDown = GetComponent<StateMoveDown>();
        moveUp = GetComponent<StateMoveUp>();
        moveLeft = GetComponent<StateMoveLeft>();
        moveRigth = GetComponent<StateMoveRigth>();
        playerMovement = GetComponent<RotationHandler>();
        
        initConfig();
        
    }
    void Start()
    {
 
    }
    public void initConfig()
    {
        moveFoward.enabled = true;
        moveDown.enabled = false;
        moveUp.enabled = false;
        moveLeft.enabled = false;
        moveRigth.enabled = false;
        playerMovement.enabled = false;
        currentState = playerMovement;
        currentState.enabled = true;

    }
    void setNewState(MovementState newState) {
        currentState.enabled = false;
        currentState = newState;
        currentState.enabled = true;
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "RigthBoundary") {
            setNewState(moveLeft);
        }
        else if (other.tag == "LeftBoundary") {
            setNewState(moveRigth);
        }
        else if(other.tag == "UpBoundary")
        {
            setNewState(moveDown);
        }
        else if(other.tag == "DownBoundary")
        {
            setNewState(moveUp);
        }
    }

}