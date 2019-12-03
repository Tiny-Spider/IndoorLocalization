using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationSearcher : MonoBehaviour {
	public Animator animator;

	[Header("Searching")]
	public InputField inputField;
	public List<DestinationEntry> destinations = new List<DestinationEntry>();

	public bool open = false;

	public void ToggleOpen() {
		open = !open;
		animator.SetTrigger(open ? "Open" : "Close");
	}

	public void ShowResults(string searchText) {
		foreach (DestinationEntry destination in destinations) {
			destination.gameObject.SetActive(destination.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
		}
	}

	public void NavigateTo(DestinationEntry destination) {
		Debug.Log("Navigate to: " + destination.text);
		ToggleOpen();
		inputField.text = string.Empty;
	}
}