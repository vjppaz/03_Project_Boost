﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Rocket : MonoBehaviour
{
    [SerializeField]
    float RotationSpeed = 100;

    [SerializeField]
    float ThrustSpeed = 10000;

    [SerializeField] AudioClip mainThrust;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip complete;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem completeParticles;

    [SerializeField] float levelLoadDelay = 2;

    [SerializeField] bool lightsOn = false;

    public static int currentLevel = 1;

    Rigidbody rigidBody;
    AudioSource audioSource;

    Light[] lights;

    enum State { Alive, Dying, Transending }
    State state = State.Alive;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        lights = GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thruster();
            Rotation();
        }

        foreach (var l in lights) l.enabled = lightsOn;

        if (Input.GetKeyUp(KeyCode.F))
        {
            lightsOn = !lightsOn;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;

        switch (collision.gameObject.tag)
        {
            case "Mud":
                Debug.Log("slows down");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            case "Friendly":
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("ResetGame", levelLoadDelay);
    }

    private void StartFinishSequence()
    {
        state = State.Transending;
        audioSource.PlayOneShot(complete);
        completeParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        currentLevel += 1;
        SceneManager.LoadScene(currentLevel - 1);
    }

    private void ResetGame()
    {
        currentLevel = 1;
        SceneManager.LoadScene(currentLevel - 1);
    }

    private void Thruster()
    {
        var speed = ThrustSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * speed);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(mainThrust);
            mainThrustParticles.Play();
        }
        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
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
