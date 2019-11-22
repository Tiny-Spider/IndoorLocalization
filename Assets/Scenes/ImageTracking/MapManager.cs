using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MapManager : MonoBehaviour {
	public Camera arCamera;

	public RectTransform playerDot;
	public MapAnchor[] mapAnchors;
	public float scale = 100;
	public Text debugText;

	void Start() {

	}

	void Update() {
		StringBuilder debug = new StringBuilder();

		TrackedObject[] trackedObjects = FindObjectsOfType<TrackedObject>();

		//foreach (TrackedObject trackedObject in trackedObjects) {

		//}

		debug.AppendLine("scale: " + scale);
		debug.AppendLine("TrackedObjects Length: " + trackedObjects.Length);

		if (trackedObjects.Length > 0) {
			TrackedObject trackedObject = trackedObjects[0];
			string name = trackedObject.trackedImage.referenceImage.name;
			MapAnchor mapAchor = mapAnchors.First(x => x.name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));

			debug.AppendLine("TrackedObject name: " + name);
			debug.AppendLine("TrackedObject tracked: " + trackedObject.trackedImage.trackingState.ToString());

			Vector2 trackedObjectPosition = ToMapPosition(trackedObject.transform.position);
			Vector2 cameraPosition = ToMapPosition(arCamera.transform.position);

			Vector2 offset = trackedObjectPosition - cameraPosition;
			Vector2 scaledOffset = offset * scale;
			Vector2 achorPosition = mapAchor.position.position;

			float achorRotation = mapAchor.position.rotation.eulerAngles.z;
			Vector2 userPosition = achorPosition + Rotate(scaledOffset, achorRotation);
			float userRoation = (arCamera.transform.rotation.eulerAngles.y + mapAchor.position.rotation.eulerAngles.z + 180) % 360;

			playerDot.rotation = Quaternion.Euler(0, 0, userRoation);
			playerDot.position = userPosition;

			debug.AppendLine("mapAchor z: " + mapAchor.position.rotation.eulerAngles.z);
			debug.AppendLine("arCamera y: " + arCamera.transform.rotation.eulerAngles.y);
			debug.AppendLine("trackedObjectPosition: " + trackedObjectPosition);
			debug.AppendLine("cameraPosition: " + cameraPosition);
			debug.AppendLine("offset: " + offset);
			debug.AppendLine("scaledOffset: " + scaledOffset);
			debug.AppendLine("achorPosition: " + achorPosition);
			debug.AppendLine("achorRotation: " + achorRotation);
			debug.AppendLine("userPosition: " + userRoation);
			debug.AppendLine("userRoation: " + userPosition);
		}

		debugText.text = debug.ToString();
	}

	Vector2 ToMapPosition(Vector3 worldPosition) {
		return new Vector2(worldPosition.x, worldPosition.z);
	}

	Vector2 Rotate(Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}

	public void ChangeScale(int adjustment) {
		scale += adjustment;
	}

	[System.Serializable]
	public struct MapAnchor {
		public string name;
		public RectTransform position;
	}
}