using System;

using Verse;

namespace RimWorld
{
	public class DroidInactive : Thing, IDroid
	{
		private const String endStr = "Inactive";
		public int age;
		public float storedEnergy = 0;
		public int activateDelay = 100;

		public DroidInactive ()
		{
			age = 0;
		}

		public float StoredEnergy {
			get {
				return storedEnergy;
			}
			set {
				storedEnergy = value;
			}
		}

		private void Activate() {
			this.Destroy(DestroyMode.Vanish);
			String kindName = base.def.defName;
			if (kindName.EndsWith(endStr)) {
				String pawnKindName = kindName.Remove (kindName.Length - endStr.Length);
				PawnKindDef pawnKind = DefDatabase<PawnKindDef>.GetNamed (pawnKindName);
				DroidPawn droid = (DroidPawn) PawnGenerator.GeneratePawn (pawnKind, Find.FactionManager.FirstFactionOfDef(DefDatabase<FactionDef>.GetNamed("Droids")));
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
			if (this.age > this.activateDelay && this.health >= this.def.maxHealth && this.storedEnergy > 0.99 * DroidPawn.storedEnergyMax)
			{
				Activate ();
			}
		}
		
		public override TipSignal GetTooltip () {
			TipSignal tip = base.GetTooltip ();
			tip.text += "\n" + DroidPawn.StoredEnergyText(this);
			return tip;
		}

		public override string GetInspectString () {
			return base.GetInspectString () + DroidPawn.StoredEnergyText (this) + "\n";
		}
	}
}

