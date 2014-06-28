using System;
using RimWorld;
using Verse;

public class Building_DroidCharger : Building_Storage
{
	public Building_DroidCharger ()
	{
	}

	public CompDroidCharger GetCharger() {
		return this.GetComp<CompDroidCharger>();
	}

	public bool IsOnAndAvailable(Pawn pawn) {
		return GetComp<CompPowerTrader>().PowerOn && GetCharger().IsAvailable(pawn);
	}
}
