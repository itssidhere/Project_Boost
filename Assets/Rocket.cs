using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcs = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    enum State{ Transcending, Dying, Alive};
    State state = State.Alive;
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
        if(state == State.Alive)
        {
            HandleThrust();
            HandleRotate();
        }
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                successParticle.Play();
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                deathParticle.Play();
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%2);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
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
            rigidbody.AddRelativeForce(Vector3.up * mainThrust );
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            mainEngineParticle.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }
}
