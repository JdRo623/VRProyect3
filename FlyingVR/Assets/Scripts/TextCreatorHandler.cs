using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextCreatorHandler : MonoBehaviour {
	private string coinsPositions;
	private int count;
	// Use this for initialization
	void Start () {
		count = 0;
		coinsPositions = "";
		initFile ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			count++;
			coinsPositions += "Coin "+count+": "+this.transform.localPosition +"/n";
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			System.IO.File.WriteAllText ("C:/FlyingVRText/Prueba.txt",coinsPositions);
		}
	}
	void initFile(){
		if(!Directory.Exists ("C:/FlyingVRText")){
			Directory.CreateDirectory ("C:/FlyingVRText");
		}

	}
}
