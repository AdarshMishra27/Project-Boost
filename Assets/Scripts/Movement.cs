using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float roationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem boosterParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!boosterParticle.isPlaying)
            boosterParticle.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        boosterParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(roationThrust);
        if (!rightThrustParticle.isPlaying)
            rightThrustParticle.Play();
    }
    private void RotateRight()
    {
        ApplyRotation(-roationThrust);
        if (!leftThrustParticle.isPlaying)
            leftThrustParticle.Play();
    }
    private void StopRotation()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }

    private void ApplyRotation(float roationThrust)
    {
        rb.freezeRotation = true; //freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * roationThrust * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so that physics system of rigdbody can take place again
    }
}
