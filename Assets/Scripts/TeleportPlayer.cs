using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void TeleportUp()
    {
       int currentLvl= SceneManager.GetActiveScene().buildIndex;
        int nextLvl = currentLvl + 1;
        SceneManager.LoadScene(nextLvl);
    }

    public static void TeleportDown()
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;
        int beforeLvl = currentLvl - 1;
        SceneManager.LoadScene(beforeLvl);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
