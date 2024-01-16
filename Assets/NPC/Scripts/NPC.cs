using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour, IHit
{
    GameObject target;
    Sight sight;
    PlayerMovement playerMovement;
    public float rotationRate = 180f;
    public UnityEvent OnTagPlayer = new UnityEvent();
    public int health = 1;

    Animator animator;
    public GameObject explosionPrefab;

    void Awake()
    {
        sight = GetComponentInChildren<Sight>();
        playerMovement = GetComponent<PlayerMovement>();
        target = GameObject.FindWithTag("Player");
        sight.target = target;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sight.CanSeeTarget())
        {
            Vector3 lookDirection = Vector3.ProjectOnPlane(target.transform.position - transform.position, Vector3.up);
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationRate * Time.deltaTime);
            playerMovement.moveDirection = lookDirection;
        }
        else
        {
            playerMovement.moveDirection = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target) 
        {
            Debug.Log("Hit Target!");
            OnTagPlayer.Invoke();

            if (collision.gameObject.TryGetComponent<IHit>(out var hit))
            {
                hit.Hit(gameObject);
            }
        }
    }

    public void Hit(GameObject other)
    {
        Debug.Log($"I {gameObject.name} got hit by {other.name}");
        animator.SetTrigger("Hit");
        health--;

        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.LookRotation(Vector3.up, transform.forward));
            Destroy(gameObject);
        }
    }
}
