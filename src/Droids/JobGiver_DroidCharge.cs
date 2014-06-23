using System;
using System.Collections.Generic;
using Verse;

namespace Verse.AI
{
	public class JobGiver_DroidCharge : ThinkNode_JobGiver
	{
		public float chargeThreshold;
		public float maxDistance;

		public JobGiver_DroidCharge (float chargeThreshold, float maxDistance) : base()
		{
			this.chargeThreshold = chargeThreshold;
			this.maxDistance = maxDistance;
		}

		protected override Job TryGiveTerminalJob (Pawn pawn)
		{
			if (!(pawn is DroidPawn)) {
				return null;
			}

			DroidPawn droid = (DroidPawn) pawn;
			if (droid.storedEnergy < this.chargeThreshold * DroidPawn.storedEnergyMax) {
				IEnumerable<Building_DroidCharger> buildings = Find.ListerBuildings.AllBuildingsColonistOfClass<Building_DroidCharger> ();
				Thing target = GenClosest.ClosestThingReachableGlobal (pawn.Position,
				                                                       buildings,
				                                                       PathMode.OnSquare,
				                                                       RegionTraverseParameters.For(pawn),
				                                                       this.maxDistance,
				                                                       (Thing thing) => { return ((Building_DroidCharger) thing).IsOnAndAvailable(); });

				if (target != null) {
					return new Job (DefDatabase<JobDef>.GetNamed ("DroidCharge"), new TargetPack (target));
				}
			}

			return null;
		}
	}
}
