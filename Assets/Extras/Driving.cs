using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Driving : MonoBehaviour
{
    Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up);
        }
    }
}
