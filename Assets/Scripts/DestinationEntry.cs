using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationEntry : MonoBehaviour {
	public Text destinationText;
	public string text { get { return destinationText.text; } }

	public void SetText(string text) {
		destinationText.text = text;
	}
}
