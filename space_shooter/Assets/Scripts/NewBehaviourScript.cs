using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClipOne;
  

  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
       

    }
}
