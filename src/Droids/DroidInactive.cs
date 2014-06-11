using System;

namespace Verse
{
	public class DroidInactive : Thing
	{
		private const String endStr = "Inactive";
		public int age;
		public float storedEnergy = DroidPawn.storedEnergyMax / 10;
		public int activateDelay = 100;

		public DroidInactive ()
		{
			age = 0;
		}

		private void Activate() {
			this.Destroy(DestroyMode.Vanish);
			String kindName = base.def.defName;
			if (kindName.EndsWith(endStr)) {
				String pawnKindName = kindName.Remove (kindName.Length - endStr.Length);
				PawnKindDef pawnKind = DefDatabase<PawnKindDef>.GetNamed (pawnKindName);
				DroidPawn droid = (DroidPawn) PawnGenerator.GeneratePawn (pawnKind, null);
				droid.health = base.health;
				droid.storedEnergy = this.storedEnergy;
				GenSpawn.Spawn (droid, base.Position);
			} else {
				Log.Error("The defName must end in Inactive; is " + kindName);
			}
		}

		public override void Tick ()
		{
			this.age++;
			if (this.age > this.activateDelay && this.health > DroidPawn.emergencyShutdownThreshold && this.storedEnergy > 0f)
			{
				Activate ();
			}
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

