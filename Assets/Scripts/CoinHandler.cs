using UnityEngine;
using System.Collections;

public class CoinHandler : MonoBehaviour {
    public int point;
	public Vector3 thisCoinPosition;
	void Start(){
		thisCoinPosition = this.transform.position;
	}
	void Update(){
		this.transform.position = thisCoinPosition;
		thisCoinPosition = this.transform.position;
	}
}
