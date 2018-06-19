using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject optionsMenu;

    private bool isInMainMenu = true;
    private bool isFullScreen = true;

	// Use this for initialization
	void Start () {
		
	}

    public void ChangeMenuStates (bool goToMainMenu) {
        if (goToMainMenu) {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            isInMainMenu = true;
        } else {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            isInMainMenu = false;
        }
    }

    public void GoToLevel (string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void Quit () {
        Application.Quit();
    }

    public void SetGFXQuality (int i) {
        QualitySettings.SetQualityLevel(i);
        print(QualitySettings.GetQualityLevel());
    }

    public void SetResolution (int i) {
        switch (i) {
            case 1: Screen.SetResolution(1920, 1080, isFullScreen);
                break;
            case 2: Screen.SetResolution(1280, 720, isFullScreen);
                break;
            case 3: Screen.SetResolution(1024, 576, isFullScreen);
                break;
        }
        
    }
}
