using UnityEngine;
using System.Collections;

public class MovementStateHandler : MonoBehaviour
{
    public Vector3 movementVector;
    public float speedFoward;
    private float calculatedSpeed;
    public Vector3 foward;
    public Vector3 side;
    public Vector3 down;
    private Rigidbody rg;
    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        down = this.transform.up * -0.2f;
        Camera.main.transform.rotation = Camera.main.GetComponentInParent<Transform>().rotation;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position = this.transform.position;
        calculatedSpeed = speedFoward * Time.deltaTime;
        foward = this.transform.forward * calculatedSpeed;
        side = this.transform.right * (Camera.main.transform.localRotation.z) * calculatedSpeed * -5;
        rg.MovePosition(this.transform.position + (foward + side + down) * calculatedSpeed);
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "RigthBoundary") {

        }
        else if (other.tag == "LeftBoundary") {

        }
        else if(other.tag == "UpBoundary") {
        }
        else if(other.tag == "DownBoundary")
        {
            Debug.Log("DFA FH");
            rg.AddForce(this.transform.up * 100);
        }
    }

}