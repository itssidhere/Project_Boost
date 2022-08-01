using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcs = 100f;
    [SerializeField] float mainThrust = 100f;
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
        HandleThrust();
        HandleRotate();
	}


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("uh yeaaaaaaaah");
                break;
            default:
                print("betichod i am dead");
                break;
        }
    }
    private void HandleRotate()
    {
        rigidbody.freezeRotation = true;
        
        float scalerSpeed = Time.deltaTime * rcs;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * scalerSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * scalerSpeed);
        }
        rigidbody.freezeRotation = false;
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
