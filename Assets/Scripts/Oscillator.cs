using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 stratingPosition;
    [SerializeField] Vector3 movementPosition;
    [SerializeField][Range(0, 1)] float movementFactor;

    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        stratingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if(period == 0f) return; //divide by zero avoided // this is wrong practice as float cannot be compared to high precision
        //so use this instead
        if (period <= Mathf.Epsilon) return; //epsilon is very small no. nearly equal to zero
        float cycles = Time.time / period; //continually frowing over time

        const float tau = Mathf.PI * 2; //constant value of pi that is 6.283

        float rawSineWave = Mathf.Sin(cycles * tau); //going from -1 to +1

        movementFactor = (rawSineWave + 1f) / 2f; //recalculated to go from 0 to +1

        Vector3 offset = movementPosition * movementFactor;
        transform.position = stratingPosition + offset;
    }
}
