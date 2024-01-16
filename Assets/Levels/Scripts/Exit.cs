using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public int loadSceneIndex;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) SceneManager.LoadScene(loadSceneIndex);
    }
}
