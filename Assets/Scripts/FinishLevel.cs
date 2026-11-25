using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool levelFinished = false;
    

    void OnTriggerEnter(Collider other)
    {
        TeleportPlayer.TeleportDown();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
