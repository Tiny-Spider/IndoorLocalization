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
	public Slider anchor1Position;

	[Header("Achor #2")]
	public Toggle anchor2Enabled;
	public Slider anchor2Position;

	[Header("Achor #3")]
	public Toggle anchor3Enabled;
	public Slider anchor3Position;

	[Header("Achor #4")]
	public Toggle anchor4Enabled;
	public Slider anchor4Position;

	private void Start() {
		GenerateImage();
	}

	public void UpdateSettings() {
		GenerateImage();
	}

	void GenerateImage() {
		roomSize = new Vector2(ToFloat(roomX.text), ToFloat(roomY.text));

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

		// Top
		if (anchor1Enabled.isOn) {
			int index = Mathf.RoundToInt((float)sizeX * anchor1Position.value);
			index = Mathf.Clamp(index, 2, sizeX - 3);
			texture.SetPixel(index - 2, sizeY - 1, Color.red);
			texture.SetPixel(index - 1, sizeY - 1, Color.red);
			texture.SetPixel(index + 0, sizeY - 1, Color.red);
			texture.SetPixel(index + 1, sizeY - 1, Color.red);
			texture.SetPixel(index + 2, sizeY - 1, Color.red);
		}

		// Bottom
		if (anchor2Enabled.isOn) {
			int index = Mathf.RoundToInt((float)sizeX * anchor2Position.value);
			index = Mathf.Clamp(index, 2, sizeX - 3);
			texture.SetPixel(index - 2, 0, Color.red);
			texture.SetPixel(index - 1, 0, Color.red);
			texture.SetPixel(index + 0, 0, Color.red);
			texture.SetPixel(index + 1, 0, Color.red);
			texture.SetPixel(index + 2, 0, Color.red);
		}

		// Left
		if (anchor3Enabled.isOn) {
			int index = Mathf.RoundToInt((float)sizeY * anchor3Position.value);
			index = Mathf.Clamp(index, 2, sizeY - 3);
			texture.SetPixel(0, index - 2, Color.red);
			texture.SetPixel(0, index - 1, Color.red);
			texture.SetPixel(0, index + 0, Color.red);
			texture.SetPixel(0, index + 1, Color.red);
			texture.SetPixel(0, index + 2, Color.red);
		}

		// Right
		if (anchor4Enabled.isOn) {
			int index = Mathf.RoundToInt((float)sizeY * anchor4Position.value);
			index = Mathf.Clamp(index, 2, sizeY - 3);
			texture.SetPixel(sizeX - 1, index - 2, Color.red);
			texture.SetPixel(sizeX - 1, index - 1, Color.red);
			texture.SetPixel(sizeX - 1, index + 0, Color.red);
			texture.SetPixel(sizeX - 1, index + 1, Color.red);
			texture.SetPixel(sizeX - 1, index + 2, Color.red);
		}

		// Apply all SetPixel calls
		texture.Apply();
		texture.filterMode = FilterMode.Point;

		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		roomImage.sprite = sprite;
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
