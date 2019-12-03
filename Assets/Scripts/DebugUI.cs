using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour {
	public List<GameObject> debugGameObjects = new List<GameObject>();

	void Start() {
		ToggleOff();
	}

	public void ToggleOff() {
		foreach (GameObject debugGameObject in debugGameObjects) {
			debugGameObject.SetActive(false);
		}
	}

	public void ToggleDebug() {
		foreach (GameObject debugGameObject in debugGameObjects) {
			debugGameObject.SetActive(!debugGameObject.activeInHierarchy);
		}
	}
}