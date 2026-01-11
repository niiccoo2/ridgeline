using UnityEngine;

public class DroneSetup : MonoBehaviour
{
    public float mass = 0.5f;
    public float drag = 0f;
    public float angularDrag = 1f;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}
