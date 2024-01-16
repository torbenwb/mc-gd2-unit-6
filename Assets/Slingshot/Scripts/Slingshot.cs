using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    Animator animator;
    private bool pull = false;

    // The ratio of how pulled back the slingshot is
    // 0 being not pulled back at all
    // 1 being fully pulled back
    public float pullRatio = 0f;
    // The minimum pull ration required to fire 
    // a projectile
    public float minPullRatio = 0.5f;
    // How fast the slingshot gets pulled from 0 to 1
    public float pullRate = 2f;
    // How fast a projectile should go if fired at max pull strength
    public float fireStrength = 25f;


    public ObjectPool projectilePool;

    void Awake() => animator = GetComponent<Animator>();

    // Begin pulling back slingshot
    public void StartPull()
    {
        pull = true;
        pullRatio = 0f;
    }

    // Release pulled back slingshot
    public void StopPull()
    {
        pull = false;
        if (pullRatio >= minPullRatio) Release();
        pullRatio = 0f;
    }

    
    void Release()
    {
        if (animator) animator.SetTrigger("Release");

        
        GameObject instance = projectilePool.GetInstance();
        
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        // if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit))
        // {
        //     rotation = Quaternion.LookRotation(hit.point - transform.position);
        // }
        // else
        // {
        //     Vector3 origin = Camera.main.transform.position;
        //     Vector3 forward = Camera.main.transform.forward;
        //     Vector3 point = origin + forward * 20f;
        //     rotation = Quaternion.LookRotation(point - transform.position);
        // }

        Projectile projectile = instance.GetComponent<Projectile>();
        projectile.Fire(position, rotation, fireStrength * pullRatio);
        projectile.onHit.AddListener(OnHit);

        pullRatio = 0f;
    }

    void OnHit(GameObject hitObject)
    {
        Debug.Log($"Hit {hitObject.name}");
        if (hitObject.TryGetComponent<IHit>(out var hit))
        {
            Debug.Log("Hit a hit interface");
            hit.Hit(gameObject);
        }
        else
        {
            Debug.Log("Did not hit a hit interface");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartPull();
        if (Input.GetMouseButtonUp(0)) StopPull();
        if (pull) pullRatio = Mathf.Clamp(pullRatio + pullRate * Time.deltaTime, 0f, 1f);

        if (animator) animator.SetFloat("Pull", pullRatio);
    }
}
