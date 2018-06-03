using UnityEngine;
using System.Collections;

public struct MDMachineBlock
{
	public Delegates.MachineBool predicate { get; private set; }
	public Delegates.MachineDo func { get; private set; }

	public MDMachineBlock (Delegates.MachineBool _predicate, Delegates.MachineDo _func) {
		predicate = _predicate;
		func = _func;
	}
}

public class MDMachine
{
	public MDMachineBlock[] blocks { get; private set; }

	public Character character { get; private set; }

	public MDMachine (Character _character, params MDMachineBlock[] _blocks) {
		blocks = _blocks;
		character = _character;
	}
		
	public void MDMUpdate () {
		foreach (var item in blocks) {
			if (item.predicate(character)) {
				item.func (character);
			}
		}
	}

	public static MDMachine Humanic_Player (Character character) 
	{
		MDMachineBlock move = new MDMachineBlock ((Character ch) => true, (Character ch) => ch.MoveAt(InputData.move));
		return new MDMachine (character, move);
	}
}