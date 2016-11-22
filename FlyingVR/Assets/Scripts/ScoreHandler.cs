using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour {
    public int points;
    public Text textPoint;
	// Use this for initialization
	void Start () {
        points =0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Coin") {
            points += other.GetComponent<CoinHandler>().point;
            textPoint.text = points+"";
        }
    }
}
