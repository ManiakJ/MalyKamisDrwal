using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI levelText;
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "LevelNokron")
        {
            levelText.text = "Poziom Nokron";
        }
        else if(sceneName=="Test scene")
        {
            levelText.text = "Poziom: Test";
        }
    }

   
}
