using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {
	public static Camera mainCamera { get; private set; }
	public static Transform mainCameraTrans { get; private set; }

	public static List<Delegates.ToDo> onPreRender { get; private set; }

	private void Awake () {
		onPreRender = new List<Delegates.ToDo> ();
		mainCamera = Camera.main;
		mainCameraTrans = mainCamera.transform;
	}

	private void OnPreRender () {
		mainCameraTrans.position = (Vector3)Character.player.position + Vector3.back * 10 + Vector3.up * mainCamera.orthographicSize - Vector3.up * 1.5f;
		foreach (var item in onPreRender) {
			item ();
		}
	}
}
