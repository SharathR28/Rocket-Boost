using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction Thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float ThrustStrength = 100f;
    [SerializeField] float RotationStrength = 100f;
    [SerializeField] AudioClip EnginethrustSFX;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem rightParticle;
    [SerializeField] ParticleSystem leftParticle;
    Rigidbody rb;
    AudioSource audiosource;
    private void OnEnable()
    {
        Thrust.Enable();
        Rotation.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    private void ProcessThrust(){
        if (Thrust.IsPressed())
        {
            Startthrusting();
        }
        else
        {
            Stopthrusting();
        }
    }
    private void Startthrusting()
    {
        rb.AddRelativeForce(Vector3.up * ThrustStrength * Time.fixedDeltaTime);
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(EnginethrustSFX);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }
    private void Stopthrusting()
    {
        audiosource.Stop();
        mainEngineParticle.Stop();
    }
    private void ProcessRotation()
    {
        float rotationInput=Rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            Stoprotation();
        }
    }
    private void RotateRight()
    {
        ApplyRoataion(RotationStrength);
        if (!rightParticle.isPlaying)
        {
            leftParticle.Stop();
            rightParticle.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRoataion(-RotationStrength);
        if (!leftParticle.isPlaying)
        {
            rightParticle.Stop();
            leftParticle.Play();
        }
    }
    private void Stoprotation()
    {
        rightParticle.Stop();
        leftParticle.Stop();
    }
    private void ApplyRoataion(float rotatethisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotatethisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
        
    }
}