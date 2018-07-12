using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite interactable;
    public Sprite grabbed;
    public Sprite empty;

    public enum IconTypes { defaultSprite, interactable, grabbed, empty}

    private Image icon;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("Icon").GetComponent<Image>()) {
            icon = GameObject.Find("Icon").GetComponent<Image>();
        }
	}

    public void ChangeSprite (IconTypes iconType) {
        if (icon) {
            switch (iconType) {
                case (IconTypes.defaultSprite):
                    icon.sprite = defaultSprite;
                    break;
                case (IconTypes.interactable):
                    icon.sprite = interactable;
                    break;
                case (IconTypes.grabbed):
                    icon.sprite = grabbed;
                    break;
                case (IconTypes.empty):
                    icon.sprite = empty;
                    break;
                default:
                    icon.sprite = defaultSprite;
                    break;
            }
        }
    }
}
