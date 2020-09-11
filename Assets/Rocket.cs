using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float RotationSpeed = 100;

    [SerializeField]
    float ThrustSpeed = 10000;

    [SerializeField]
    AudioClip mainThrust;

    [SerializeField]
    AudioClip deathAudio;

    [SerializeField]
    AudioClip completeAudio;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transending }
    State state = State.Alive;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thruster();
            Rotation();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Mud":
                Debug.Log("slows down");
                break;
            case "Finish":
                state = State.Transending;
                audioSource.PlayOneShot(completeAudio);
                Invoke("LoadNextLevel", 1);
                break;
            case "Friendly":
                Debug.Log("OK");
                break;
            default:
                state = State.Dying;
                audioSource.PlayOneShot(deathAudio);
                Invoke("ResetGame", 1);
                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Thruster()
    {
        var speed = ThrustSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * speed);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrust);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotation()
    {
        rigidBody.freezeRotation = true;

        var speed = RotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * speed);
        }

        rigidBody.freezeRotation = false;
    }
}
