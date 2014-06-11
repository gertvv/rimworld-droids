using System;
using Verse;

public class CompDroidCharger : ThingComp
{
	public CompDroidCharger ()
	{
	}

	public override void CompTick() {
		base.CompTick ();

		// Find a DroidPawn exactly on our position
		//Thing found = GenClosest.ClosestThingGlobal(this.parent.Position, Find.ListerPawns.AllPawns, 1);
		Thing found = Find.ThingGrid.ThingAt<DroidPawn> (this.parent.Position);
		if (found == null) {
			found = Find.ThingGrid.ThingAt<DroidInactive> (this.parent.Position);
		}

		// Find the CompPowerTrader
		CompPowerTrader comp = this.parent.GetComp<CompPowerTrader> ();
		if (comp == null) {
			Log.Error ("CompDroidCharger in " + parent.def.defName + " needs a CompPowerTrader to function");
			return;
		}

		float rate = 0.01f; // use 1% when not charging
		if (found != null) {
			rate = 1f;
			IDroid droid = (IDroid)found;
			if (droid.StoredEnergy < DroidPawn.storedEnergyMax) {
				droid.StoredEnergy += comp.def.basePowerConsumption * comp.def.efficiency * CompPower.WattsToWattDaysPerTick;
			}
		}

		// set power consumption
		comp.powerOutput = -rate * comp.def.basePowerConsumption;
	}
}
