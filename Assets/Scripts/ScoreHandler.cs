using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour {
    public int points;
    public Text textPoint; 
	public AudioSource coinSound;
	private string coin;

	// Use this for initialization
	void Start () {
	    coin = "Coin";
        points =0;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(coin)) {
            points += other.GetComponent<CoinHandler>().point;
            textPoint.text = points+"";
			coinSound.Play ();
        }
    }
}
