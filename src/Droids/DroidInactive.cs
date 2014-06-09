using System;

namespace Verse
{
	public class DroidInactive : Thing
	{
		private const String endStr = "Inactive";
		public int age;
		public int activateDelay = 10;

		public DroidInactive ()
		{
			age = 0;
		}

		private void Activate() {
			this.Destroy(DestroyMode.Vanish);
			String kindName = this.def.defName;
			if (this.def.defName.EndsWith(endStr)) {
				String pawnKindName = kindName.Remove (kindName.Length - endStr.Length);
				PawnKindDef pawnKind = DefDatabase<PawnKindDef>.GetNamed (pawnKindName);
				DroidPawn droid = (DroidPawn) PawnGenerator.GeneratePawn (pawnKind, null);
				GenSpawn.Spawn (droid, base.Position);
			} else {
				Log.Error("The defName must end in Inactive; is " + kindName);
			}
		}

		public override void Tick ()
		{
			this.age++;
			if (this.age > this.activateDelay)
			{
				Activate ();
			}
		}
	}
}

