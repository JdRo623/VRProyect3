using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour {
    public int points;
    public Text textPoint;
    private float pause;
    public bool isPauseActive;
	// Use this for initialization
	void Start () {
        points =0;
        pause = 0;
        if (!isPauseActive)
            pause = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = pause;
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pause.Equals(0))
            {
                pause = 1;      
            }
            else {
                pause = 0;
            }    
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Coin") {
            points += other.GetComponent<CoinHandler>().point;
            textPoint.text = points+"";
        }
    }
}
