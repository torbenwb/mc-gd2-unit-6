using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    // Direction and magnitude of projectile's velocity
    public Vector3 velocity;
    // The rate at which gravity affects this projectile's velocity
    public float gravityRate = 1f;
    // How long to stay alive before deactivating
    public float lifetime = 2f;
    // Render a trail behind projectile to show trajectory
    TrailRenderer trailRenderer;
    // Invoked when this projectile hits something in Update
    public UnityEvent<GameObject> onHit = new UnityEvent<GameObject>();

    public void Fire(Vector3 startPosition, Quaternion startRotation, float speed, float lifetime = 2f)
    {
        
        this.lifetime = lifetime;
        transform.position = startPosition;
        transform.rotation = startRotation;
        velocity = startRotation * Vector3.forward * speed;
        gameObject.SetActive(true);
        trailRenderer.Clear();
    }

    void Awake() => trailRenderer = GetComponent<TrailRenderer>();

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            gameObject.SetActive(false);
            return;
        }
        // Update velocity
        velocity += Physics.gravity * gravityRate * Time.deltaTime;

        // Raycast in direction of velocity
        if (Physics.Raycast(transform.position, velocity, out var hit, velocity.magnitude * Time.deltaTime))
        {
            // This projectile hit something
            onHit.Invoke(hit.collider.gameObject);
            gameObject.SetActive(false);
        }
        else 
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

}
