using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    // Use this for initialization
    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var cycles = Time.time / period;
        float movementFactor = Mathf.Sin(cycles * Mathf.PI) / 2f + 0.5f;

        var offset = movementVector * movementFactor;
        gameObject.transform.position = startingPosition + offset;
    }
}
