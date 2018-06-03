using UnityEngine;
using System.Collections;
using System.Linq;

public class SpritesData
{
	public static Sprite[] LoadSpritesAtPath (string path) {
		return Resources.LoadAll<Sprite> (path).OrderBy ((Sprite sp) => sp.name).ToArray ();
	}
}