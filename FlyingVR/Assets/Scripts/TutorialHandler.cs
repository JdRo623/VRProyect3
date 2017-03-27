using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour {
    public GameObject leftHUD;
    public GameObject rigthHUD;
    public float showTime;
    public float firstShowTime;
    public float secondShowTime;
    private float counter;
    private float activeCounter;
    bool tutorialFin;
	// Use this for initialization
	void Start () {
        tutorialFin = false;
        counter = 0;
        activeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!tutorialFin) {
            counter += Time.deltaTime;
            if (firstShowTime<=counter) {
                ActivatePanel(leftHUD);
            }

        }
	}
    void ActivatePanel(GameObject panel) {
        activeCounter += Time.deltaTime;
        if (!panel.active) {
            panel.SetActive(true);
        }
        if (activeCounter >= showTime) {
            panel.SetActive(false);
            activeCounter = 0;
            firstShowTime = secondShowTime;
            if (leftHUD==rigthHUD) {
                tutorialFin = true;
            }
            leftHUD = rigthHUD;
            counter = 0;
        }
        
    }
}
