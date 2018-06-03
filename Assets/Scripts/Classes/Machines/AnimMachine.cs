using UnityEngine;
using System.Collections;
using System.Linq;

public class AnimBlock
{
	public string clipName { get; private set; }
	public int pictureIndex { get; private set; }
	public AnimMachine origin { get; private set; }
	public Sprite[] sprites { get; private set; }
	private float lastSpriteChangeTime;

	public AnimBlock (string _clipName, AnimMachine _origin) {
		clipName = _clipName;
		pictureIndex = 0;
		origin = _origin;
		lastSpriteChangeTime = 0;
		sprites = SpritesData.LoadSpritesAtPath ("Import/Art/Sprites/" + origin.name + "-" + clipName + "/");
	}

	public bool canSetSprite
	{
		get {
			return (Time.timeSinceLevelLoad - lastSpriteChangeTime) > 0;
		}
	}

	public void Update () {
		if (canSetSprite) {
			pictureIndex = pictureIndex < sprites.Length ? pictureIndex : (clipName == "idle" ? sprites.Length - 1 : 0);
			origin.character.rend.sprite = sprites [pictureIndex];
			pictureIndex++;
			lastSpriteChangeTime = Time.time + 0.125f;
		}
	}
}

public class AnimMachine
{
	public string name { get; private set; }
	public Character character { get; private set; }
	public AnimBlock currentBlock  { get; private set; }

	public void SetBlock (string clipName) {
		if (currentBlock == null || currentBlock.clipName != clipName) {
			currentBlock = null;
			currentBlock = new AnimBlock (clipName, this);
		}
	}

	public void Update () {
		if (currentBlock != null) {
			currentBlock.Update ();
		}
	}

	public AnimMachine (string _name, Character _character) {
		name = _name;
		character = _character;
	}
}