﻿using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter (Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly": print("Okay");
                break;
            case "Fuel": print("Fueling up");
                break;
            default: print("Dead");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))    // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)     // so it doesn't layer
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;    // take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
            //rigidBody.AddRelativeTorque(agility * Vector3.forward * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
            //rigidBody.AddRelativeTorque(agility * -Vector3.forward * Time.deltaTime);
        }

        rigidBody.freezeRotation = false;   // resume physics control of rotation
    }
}