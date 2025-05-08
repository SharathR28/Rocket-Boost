using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction Thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float ThrustStrength = 100f;
    [SerializeField] float RotationStrength = 100f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Thrust.Enable();
        Rotation.Enable();
    }
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    private void ProcessThrust(){
        if (Thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up* ThrustStrength* Time.fixedDeltaTime);
        }
    }
    private void ProcessRotation()
    {
        float rotationInput=Rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRoataion(RotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRoataion(-RotationStrength);
        }
    }
    private void ApplyRoataion(float rotatethisFrame)
    {
        transform.Rotate(Vector3.forward * rotatethisFrame * Time.fixedDeltaTime);
    }
}