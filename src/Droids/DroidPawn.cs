using System;
using Verse.AI;
namespace Verse
{
	public class DroidPawn : Pawn, IDroid
	{
		public float storedEnergy = 0; // set from the energy stored in the inactive droid
		public const float emergencyShutdownThreshold = 25;
		public const float powerConsumption = 50; // a powered door uses 50.
		public const float storedEnergyMax = 100; // A normal battery holds 1000.

		public DroidPawn () : base()
		{
		}

		public float StoredEnergy {
			get {
				return storedEnergy;
			}
			set {
				storedEnergy = value;
			}
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

		public static string StoredEnergyText(IDroid droid) {
			return string.Concat (new string[] {
				"PowerBatteryStored".Translate (),
				": ",
				droid.StoredEnergy.ToString ("######0.0"),
				" / ",
				DroidPawn.storedEnergyMax.ToString ("######0.0"),
				" Wd"
			});
		}

		public override TipSignal GetTooltip () {
			TipSignal tip = base.GetTooltip ();
			tip.text += "\n" + StoredEnergyText (this);
			return tip;
		}

		public override string GetInspectString () {
			return base.GetInspectString () + StoredEnergyText (this) + "\n";
		}
	}
}