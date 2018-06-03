using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	[SerializeField] private Transform town;
	[SerializeField] private Transform sky;

	private void Start () {
		CameraMotor.onPreRender.Add (delegate() {
			SetBackground();
		});
	}
	private void SetBackground () {
		Vector3 scale = Vector3.one * CameraMotor.mainCamera.orthographicSize;
		town.localScale = scale;
		sky.localScale = scale;
		town.localPosition = -(Vector2)CameraMotor.mainCameraTrans.position / 10;
	}
}
