using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class LockerScript : Interactable {

    public bool isOpen;
    public bool canBeOpened = true;
    [HideInInspector]
    public bool currentStatus;

    private Animator animator;
    private bool isInteractable = true;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentStatus = isOpen;
        ChangeState(isOpen);
	}
	
    public void ChangeState (bool _isOpen) {
        if (isInteractable && canBeOpened) {
            isInteractable = false;
            animator.SetBool("IsOpen", _isOpen);
            currentStatus = _isOpen;
            StartCoroutine(AnimationDelay());
        }
    }

    IEnumerator AnimationDelay () {
        yield return new WaitForSeconds(0.5f);
        isInteractable = true;
    }
}
