using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public Animator animator;
	public DestinationSearcher searcher;

	public void CloseMenu() {
		animator.SetTrigger("Close");
		searcher.ToggleOpen();
	}
}