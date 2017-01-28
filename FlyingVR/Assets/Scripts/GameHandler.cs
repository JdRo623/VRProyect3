using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    private float pause;
    public bool isPauseActive;
    public float timeToStart;
    public bool isEnableToMove;
    public Text text;
    public GameObject panel;

    // Use this for initialization
    void Start () {
        pause = 1;
        isEnableToMove = false;
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = pause;

        if (timeToStart <= 0)
        {
            isEnableToMove = true;
            text.text = "Buena Suerte!";
        }
        else {
            timeToStart -= Time.deltaTime;
            text.text = (int)timeToStart + "";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pause.Equals(0))
            {
                pause = 1;
            }
            else
            {
                pause = 0;
            }
        }
    }
    void initGame() {
        panel.SetActive(false);

    }
}
