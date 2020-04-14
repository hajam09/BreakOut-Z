using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("score");// to ensure that score resets to 0 when starting game from scratch.
        PlayerPrefs.DeleteKey("survivors");// same reason here
        PlayerPrefs.DeleteKey("rifle_availability");// same reason here
        PlayerPrefs.DeleteKey("shotgun_availability");// same reason here

        // The following codes where in the NextLevelController.cs

        PlayerPrefs.DeleteKey("Level_1_to_2_Survivors");
        PlayerPrefs.DeleteKey("Level_2_to_3_Survivors");
        PlayerPrefs.DeleteKey("2-1-Coordinates");
        PlayerPrefs.DeleteKey("3-2-Coordinates");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
    	// UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    	// SceneManager.LoadScene("Level 1");
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
    	print("done name");
    	Application.Quit();
    }

    public void ToMainMenuScreen(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        // SceneManager.LoadScene("Menu");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); need to configure the build
    }
}
