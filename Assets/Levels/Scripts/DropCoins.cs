using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    public int coinCountMin = 1;
    public int coinCountMax = 5;

    public float forceStrengthMin = 5f;
    public float forceStrengthMax = 10f;

    public GameObject prefab;

    void Start()
    {
        for(int i = 0; i < Random.Range(coinCountMin, coinCountMax); i++)
        {
            GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody rb = instance.GetComponent<Rigidbody>();
            rb.AddExplosionForce(Random.Range(forceStrengthMin, forceStrengthMax), transform.position, 5f, 5f, ForceMode.Impulse);
        }
    }
}
