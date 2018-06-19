using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject optionsMenu;

    private bool isInMainMenu = true;

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
        
    }
}
