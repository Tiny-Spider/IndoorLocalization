using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MapManager : MonoBehaviour {
	public Camera arCamera;

	public RectTransform playerDot;
	public MapAnchor[] mapAnchors;
	public float scale = 100;
	public Text debugText;

	private TrackedObject lastTrackedObject;

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
			TrackedObject trackedObject = trackedObjects.Find(x => x.trackedImage.trackingState == TrackingState.Tracking, lastTrackedObject);
			// TrackedObject trackedObject = trackedObjects.Find(x => x.trackedImage.trackingState == TrackingState.Tracking, trackedObjects.Find(x => x.trackedImage.trackingState == TrackingState.Limited, trackedObjects[0]));

			if (trackedObject == null) {
				return;
			}

			lastTrackedObject = trackedObject;

			string name = trackedObject.trackedImage.referenceImage.name;
			MapAnchor mapAchor = mapAnchors.First(x => x.name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));

			// Get the positions
			Vector2 trackedPosition = ToVector2(trackedObject.transform.position);
			float trackedRotation = Mathf.Atan2(trackedObject.transform.forward.x, trackedObject.transform.forward.z) * Mathf.Rad2Deg;

			Vector2 cameraPosition = ToVector2(arCamera.transform.position);
			float cameraRotation = Mathf.Atan2(arCamera.transform.forward.x, arCamera.transform.forward.z) * Mathf.Rad2Deg;

			Vector2 cameraOffsetPosition = trackedPosition - cameraPosition;

			Vector2 mapAchorPosition = mapAchor.position.position;
			float mapAchorRotation = mapAchor.position.rotation.eulerAngles.z;


			Vector3 mapUserPosition = RotateAroundPivot(cameraOffsetPosition, Vector3.zero, -trackedRotation);
					mapUserPosition = RotateAroundPivot(cameraOffsetPosition, Vector3.zero, mapAchorRotation);

			mapUserPosition *= scale;
			mapUserPosition += (Vector3)mapAchorPosition;
			//Vector2 userPosition = mapAchorPosition + Rotate(scaledOffset, mapAchorRotation);

			//Vector2 offset = trackedObjectPosition - cameraPosition;
			//Vector2 scaledOffset = offset * scale;
			//float userRoation = (cameraRotation + mapAchor.position.rotation.eulerAngles.z + 180) % 360;

			//playerDot.rotation = Quaternion.Euler(0, 0, userRoation);
			playerDot.position = mapUserPosition;

			debug.AppendLine("TrackedObject name: " + name);
			debug.AppendLine("TrackedObject tracked: " + trackedObject.trackedImage.trackingState.ToString());
			debug.AppendLine("mapAchor z: " + mapAchor.position.rotation.eulerAngles.z);
			debug.AppendLine("trackedPosition: " + trackedPosition);
			debug.AppendLine("trackedRotation: " + trackedRotation);
			debug.AppendLine("cameraPosition: " + cameraPosition);
			debug.AppendLine("cameraRotation: " + cameraRotation);
			debug.AppendLine("cameraOffsetPosition: " + cameraOffsetPosition);
			debug.AppendLine("mapAchorPosition: " + mapAchorPosition);
			debug.AppendLine("mapAchorRotation: " + mapAchorRotation);
			debug.AppendLine("mapUserPosition: " + mapUserPosition);
		}

		debugText.text = debug.ToString();
	}

	Vector2 ToVector2(Vector3 vector) {
		return new Vector2(vector.x, vector.z);
	}

	public static Vector3 RotateAroundPivot(Vector3 Point, Vector3 Pivot, float rotation) {
		return Quaternion.Euler(new Vector3(0, 0, rotation)) * (Point - Pivot) + Pivot;
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