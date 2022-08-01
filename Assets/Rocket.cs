using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    new Rigidbody rigidbody;
    AudioSource audioSource;
    bool m_Play;
    bool m_ToggleChange;
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        m_ToggleChange = true;
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
        //PlayAudio();
	}

    private void PlayAudio()
    {
        if (m_Play == true && m_ToggleChange == true)
        {
            audioSource.Play();
            m_ToggleChange = false;
        }
        if (m_Play == false && m_ToggleChange == true)
        {
            audioSource.Stop();
            m_ToggleChange = false;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime);
        }
    }
}
