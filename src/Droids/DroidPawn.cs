using System;
using Verse.AI;
namespace Verse
{
	public class DroidPawn : Pawn
	{
		public float storedEnergy = 0; // set from the energy stored in the inactive droid
		public const float emergencyShutdownThreshold = 25;
		public const float powerConsumption = 50; // a powered door uses 50.
		public const float storedEnergyMax = 100; // A normal battery holds 1000.

		public DroidPawn () : base()
		{
		}

		public float EnergyPerTick
		{
			get
			{
				return DroidPawn.powerConsumption * CompPower.WattsToWattDaysPerTick;
			}
		}

		public void shutdown() {
			this.Destroy(DestroyMode.Vanish);
			String kindName = base.kindDef.defName + "Inactive";
			ThingDef def = ThingDef.Named (kindName);
			DroidInactive thing = (DroidInactive) ThingMaker.MakeThing (def);
			thing.health = base.health;
			thing.storedEnergy = this.storedEnergy;
			GenSpawn.Spawn (thing, base.Position);
		}

		public override void Tick () {
//			if (Find.MapConditionManager.ConditionIsActive (MapConditionDefOf.SolarFlare)) {
//			}

			storedEnergy -= this.EnergyPerTick;
			if (storedEnergy <= 0f) {
				shutdown ();
				return;
			}

			base.Tick ();
		}

		public override TipSignal GetTooltip () {
			TipSignal tip = base.GetTooltip ();
			tip.text += string.Concat (new string[] {
				"\n",
				"PowerBatteryStored".Translate (),
				": ",
				this.storedEnergy.ToString ("######0.0"),
				" / ",
				DroidPawn.storedEnergyMax.ToString ("######0.0"),
				" Wd"
			});
			return tip;
		}
	}
}