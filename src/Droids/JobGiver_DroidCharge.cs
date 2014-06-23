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

	public class JobGiver_DroidCharge_Emergency : JobGiver_DroidCharge
	{
		public JobGiver_DroidCharge_Emergency () : base(0.08f, 9999f)
		{
		}
	}
	
	public class JobGiver_DroidCharge_Safeguard : JobGiver_DroidCharge
	{
		public JobGiver_DroidCharge_Safeguard () : base(0.5f, 50f)
		{
		}
	}

	public class JobGiver_DroidCharge_Opportunistic : JobGiver_DroidCharge
	{
		public JobGiver_DroidCharge_Opportunistic () : base(0.9f, 10f)
		{
		}
	}

}
