using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    CapsuleCollider capsule;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float jumpForce = 10f;

    public Vector3 moveDirection;

    float traceDistance => capsule.height * 0.5f;
    bool grounded => Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out var hit, traceDistance);
    Vector3 flatVelocity => new Vector3(rigidbody.velocity.x, 0f , rigidbody.velocity.z);
    Vector3 moveOutput => Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized * moveSpeed - flatVelocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }
    
    void FixedUpdate() => rigidbody.AddForce(moveOutput * acceleration);
    public void Jump() => rigidbody.AddForce(Vector3.up * (grounded ? jumpForce : 0f), ForceMode.Impulse);
}
