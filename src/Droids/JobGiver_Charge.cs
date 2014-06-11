using System;
using System.Collections.Generic;
using Verse;

namespace Verse.AI
{
	public class JobGiver_DroidCharge : ThinkNode_JobGiver
	{
		public float chargedThreshold = 0.08f;

		public JobGiver_DroidCharge () : base()
		{
		}

		protected override Job TryGiveTerminalJob (Pawn pawn)
		{
			if (!(pawn is DroidPawn)) {
				return null;
			}

			DroidPawn droid = (DroidPawn) pawn;
			if (droid.storedEnergy < this.chargedThreshold * DroidPawn.storedEnergyMax) {
				IEnumerable<Building_DroidCharger> buildings = Find.ListerBuildings.AllBuildingsColonistOfClass<Building_DroidCharger> ();
				Thing target = GenClosest.ClosestThingReachableGlobal (pawn.Position, buildings, PathMode.OnSquare, RegionTraverseParameters.For(pawn));

				if (target != null) {
					return new Job (DefDatabase<JobDef>.GetNamed ("DroidCharge"), new TargetPack (target));
				}
			}

			return null;
		}
	}
}
