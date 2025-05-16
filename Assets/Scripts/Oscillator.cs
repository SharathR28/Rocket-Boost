using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 MovementVector;
    [SerializeField] float speed;
    [SerializeField] float length;
    Vector3 StartPostion;
    Vector3 EndPostion;
    float movefactor;
    void Start()
    {
        StartPostion = transform.position;
        EndPostion = StartPostion + MovementVector;
    }

    
    void Update()
    {
        movefactor = Mathf.PingPong(Time.time * speed, length);
        transform.position = Vector3.Lerp(StartPostion, EndPostion, movefactor); 
    }
}
