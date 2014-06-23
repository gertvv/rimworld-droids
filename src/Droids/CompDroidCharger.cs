using System;
using Verse;

public class CompDroidCharger : ThingComp
{
	public CompDroidCharger ()
	{
	}

	Thing found = null;

	public bool IsAvailable() {
		return found == null;
	}

	public override void CompTick() {
		base.CompTick ();

		// Find a Droid exactly on our position
		//  - keep charging the same droid if there are multiple
		if (found != null && (found.Position != this.parent.Position || found.destroyed)) {
			found = null;
		}
		if (found == null) {
			found = Find.ThingGrid.ThingAt<DroidPawn> (this.parent.Position);
		}
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
			if (comp.PowerOn && droid.StoredEnergy < DroidPawn.storedEnergyMax) {
				droid.StoredEnergy += comp.def.basePowerConsumption * comp.def.efficiency * CompPower.WattsToWattDaysPerTick;
			}
		}

		// set power consumption
		comp.powerOutput = -rate * comp.def.basePowerConsumption;
	}
}
