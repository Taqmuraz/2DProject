using UnityEngine;
using System.Collections;

public class Delegates
{
	public delegate void MachineDo (Character character);
	public delegate bool MachineBool (Character character);
	public delegate void ToDo ();
}