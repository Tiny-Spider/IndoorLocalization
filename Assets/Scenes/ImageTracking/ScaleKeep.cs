using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleKeep : MonoBehaviour
{
	public Vector3 scale;

    void LateUpdate()  {
		Transform parent = transform.parent;

		transform.parent = null;
		transform.localScale = scale;
		transform.parent = parent;
    }
}
