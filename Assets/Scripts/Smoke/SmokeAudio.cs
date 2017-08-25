using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAudio : MonoBehaviour {
    
    public AudioSource audioSource;
    public AudioClip[] windSFX;

    private int counter = 0;
    
  
	// Use this for initialization
	void Start () {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windSFX[0];
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Play()
    {
        audioSource.clip = RandomClip();
        audioSource.Play();
    }

    private AudioClip RandomClip()
    {
        int index = Mathf.FloorToInt(Random.Range(0, windSFX.Length));
        return windSFX[index];
    }
}
