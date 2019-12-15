using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
	public Image roomImage;
	public int roomImageMaxSize = 100;

	[Header("Room")]
	public InputField roomX;
	public InputField roomY;
	private Vector2 roomSize;

	[Header("Achor #1")]
	public Toggle anchor1Enabled;
	public InputField anchor1X;
	public InputField anchor1Y;
	private Vector2 anchor1Pos;

	[Header("Achor #2")]
	public Toggle anchor2Enabled;
	public InputField anchor2X;
	public InputField anchor2Y;
	private Vector2 anchor2Pos;

	[Header("Achor #3")]
	public Toggle anchor3Enabled;
	public InputField anchor3X;
	public InputField anchor3Y;
	private Vector2 anchor3Pos;

	private void Start() {
		UpdateSettings();
	}

	public void UpdateSettings() {
		ReadValues();

	}

	void GenerateImage() {
		int sizeX, sizeY;

		if (roomSize.x > roomSize.y) {
			sizeX = roomImageMaxSize;
			sizeY = Mathf.RoundToInt(roomImageMaxSize * (roomSize.y / roomSize.x));
		} else {
			sizeY = roomImageMaxSize;
			sizeX = Mathf.RoundToInt(roomImageMaxSize * (roomSize.x / roomSize.y));
		}

		Texture2D texture = new Texture2D(sizeX, sizeY, TextureFormat.ARGB32, false);

		// set the pixel values
		for (int x = 0; x < texture.width; x++) {
			for (int y = 0; y < texture.height; y++) {
				if ((x == 0 || x == texture.width - 1) || (y == 0 || y == texture.height - 1)) {
					texture.SetPixel(x, y, Color.black);
				}
			}
		}

		// Apply all SetPixel calls
		texture.Apply();
		texture.filterMode = FilterMode.Point;

		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		roomImage.sprite = sprite;
	}

	void ReadValues() {
		roomSize = new Vector2(ToFloat(roomX.text), ToFloat(roomY.text));
		anchor1Pos = new Vector2(ToFloat(anchor1X.text), ToFloat(anchor1Y.text));
		anchor2Pos = new Vector2(ToFloat(anchor2X.text), ToFloat(anchor2Y.text));
		anchor3Pos = new Vector2(ToFloat(anchor3X.text), ToFloat(anchor3Y.text));
	}

	float ToFloat(string text) {
		text = text.Replace(',', '.');
		try {
			return float.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
		} catch {
			return 1f;
		}
	}
}
