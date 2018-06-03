using UnityEngine;
using System.Collections;
using System.Linq;

public struct MotionMachineTransition
{
	public string nextState { get; private set; }
	public Delegates.MachineBool predicate { get; private set; }

	public MotionMachineTransition (string _nextState, Delegates.MachineBool _predicate) {
		nextState = _nextState;
		predicate = _predicate;
	}
}

public struct MotionMachineState
{
	public string name { get; private set; }
	public MotionMachineTransition[] transitions { get; private set; }
	public Character origin { get; private set; }
	public Delegates.MachineDo func { get; private set; }

	public MotionMachineState (string _name, Character _origin, params MotionMachineTransition[] _transitions) {
		name = _name;
		origin = _origin;
		transitions = _transitions;
		func = delegate(Character ch) {
			ch.animMachine.SetBlock (_name);
		};
	}

	public string Next () {
		string next = name;
		foreach (
				var
			item
			in
			transitions) {
			if (item.predicate (origin)) {
				next = item.nextState;
				break;
			}
		}
		return next;
	}
}

public class MotionMachine
{
	public Character character { get; private set; }
	public MotionMachineState[] states { get; private set; }
	public MotionMachineState currentState { get; private set; }

	public MotionMachine (Character _character, params MotionMachineState[] _states) {
		character = _character;
		states = _states;
	}

	public void MMUpdate () {
		if (string.IsNullOrEmpty(currentState.name)) {
			currentState = states.FirstOrDefault();
			return;
		}
		string next = currentState.Next ();
		if (currentState.name != next) {
			currentState = states.FirstOrDefault ((MotionMachineState mms) => mms.name == next);
		} else {
			currentState.func (character);
		}
	}

	public static MotionMachine Humanic (Character character) {
		MotionMachineTransition idle_to_move = new MotionMachineTransition ("walk", (Character ch) => ch.isMoving);
		MotionMachineTransition move_to_idle = new MotionMachineTransition ("idle", (Character ch) => !ch.isMoving);
		return new MotionMachine(character,
			new MotionMachineState ("idle", character, idle_to_move),
			new MotionMachineState ("walk", character, move_to_idle));
	}
}