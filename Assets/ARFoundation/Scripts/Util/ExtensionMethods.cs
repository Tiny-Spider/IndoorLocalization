using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods {
	public static bool HasFlag(this Enum variable, Enum value) {
		if (variable.GetType() != value.GetType()) {
			Debug.LogError("The checked flag is not from the same type as the checked variable.");
			return false;
		}

		ulong num = Convert.ToUInt64(value);
		ulong num2 = Convert.ToUInt64(variable);

		return (num2 & num) == num;
	}

    public static bool Contains(this LayerMask mask, int layer) {
        return mask == (mask | (1 << layer));
    }

	public static void ToggleActive(this GameObject toggleObject) {
		toggleObject.SetActive(!toggleObject.activeSelf);
	}

	public static void ToggleActive(this Component toggleObject) {
		toggleObject.SetActive(!toggleObject.gameObject.activeSelf);
	}

	public static void SetActive(this Component component, bool value) {
		component.gameObject.SetActive(value);
	}

	public static T GetComponentAll<T>(this Component component) where T : class {
		return component ? component.GetComponent<T>() ?? component.GetComponentInParent<T>() ?? component.GetComponentInChildren<T>() : null;
	}

    public static bool IsValid(this Vector3 position) {
        return position != default(Vector3) && position.x.IsValid() && position.y.IsValid() && position.z.IsValid();
    }

    public static bool IsValid(this Quaternion rotation) {
        return rotation != default(Quaternion) && rotation.x.IsValid() && rotation.y.IsValid() && rotation.z.IsValid() && rotation.w.IsValid();
    }

    public static bool IsValid(this float value) {
        return !(float.IsInfinity(value) || float.IsNaN(value));
    }

    public static bool EqualsIgnorecase(this string value, string other) {
		return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
	}

	public static bool Distance(this Vector3 origin, Vector3 target, float distance, bool greater = true) {
		return greater ? ((origin - target).sqrMagnitude > distance * distance) : ((origin - target).sqrMagnitude < distance * distance);
	}

	public static bool IsChanged(this KeyCode keyCode) {
		return Input.GetKeyDown(keyCode) || Input.GetKeyUp(keyCode);
	}
}
