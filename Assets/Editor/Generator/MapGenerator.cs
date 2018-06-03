using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class MapGenerator : EditorWindow
{
	[MenuItem ("Window/MapGenerator")]
	private static void ShowWindow () {
		EditorWindow.GetWindow<MapGenerator> ();
	}

	private Transform root;
	private GameObject fillPrefab;
	private int count;
	private Vector3 startPosition;
	private Vector3 startSize = Vector3.one;
	private bool xAxis;

	private void OnGUI () {
		root = (Transform)EditorGUILayout.ObjectField ("Root : ", root, typeof(Transform), true);
		if (root) {
			fillPrefab = (GameObject)EditorGUILayout.ObjectField ("Prefab : ", fillPrefab, typeof(GameObject), true);
			count = EditorGUILayout.IntField ("Count : ", count);
			startPosition = EditorGUILayout.Vector3Field ("Start position : ", startPosition);
			startSize = EditorGUILayout.Vector3Field ("Start size : ", startSize);
			xAxis = EditorGUILayout.Toggle ("By X axis : ", xAxis);
			if (GUILayout.Button("Fill")) {
				FillObjects (fillPrefab, count, startPosition, startSize, xAxis);
			}
		}
	}
	public void FillObjects (GameObject obj, int count, Vector3 start_pos, Vector3 start_size, bool byX) {
		float lastSpace = 0;
		for (int i = 0; i < count; i++) {
			GameObject a = Instantiate (obj);
			Transform trans = a.transform;
			SpriteRenderer rend = a.GetComponent<SpriteRenderer> ();
			float space = byX ? rend.bounds.size.x : rend.bounds.size.y;
			trans.localScale = start_size;
			Vector3 dir = (byX ? Vector3.right : Vector3.down) * lastSpace * i;
			trans.SetParent (root);
			trans.localPosition = start_pos - dir.normalized * (space * count / 2) + dir;
			lastSpace = space;
		}
	}
}