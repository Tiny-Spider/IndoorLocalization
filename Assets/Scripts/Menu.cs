using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public Animator animator;
	public DestinationSearcher searcher;

	void Update() {
		TrackedObject[] trackedObjects = FindObjectsOfType<TrackedObject>();

		if (trackedObjects.Length > 0) {
			CloseMenu();
			enabled = false;
		}
	}

	public void CloseMenu() {
		animator.SetTrigger("Close");
		searcher.ToggleOpen();
		enabled = false;
	}
}