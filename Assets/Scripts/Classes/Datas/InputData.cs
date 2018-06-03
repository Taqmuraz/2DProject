using UnityEngine;
using System.Collections;

public class InputData
{
	public static Vector2 move
	{
		get {
			return new Vector2 (Input.GetAxis ("Horizontal"), 0);
		}
	}
}