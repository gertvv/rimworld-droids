using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Verse.AI
{
	public class JobGiver_Droid : JobGiver_WorkRoot
	{
		//
		// Constructors
		//
		public JobGiver_Droid () : base ()
		{
		}
		//
		// Methods
		//

		protected override Job TryGiveTerminalJob (Pawn pawn)
		{
			if (!(pawn is DroidPawn)) {
				return null;
			}

			WorkTypeDef current = DefDatabase<WorkTypeDef>.GetNamed (pawn.kindDef.backstoryCategory);

			// STOLEN FROM JobGiver_WorkRoot
			int num = -999;
			TargetPack targetPack = TargetPack.Invalid;
			WorkGiver workGiver = null;
			foreach (WorkGiver giver in this.AllWorkGivers)
			{
				if (giver.def.workType == current)
				{
					if (giver.def.priorityInType != num && targetPack.Valid)
					{
						break;
					}
					if (giver.def.scanThings)
					{
						Predicate<Thing> predicate = (Thing t) => !t.IsForbidden () && giver.StartingJobForOn (pawn, t) != null;
						Predicate<Thing> validator = predicate;
						Thing thing = GenClosest.ClosestThingReachable (pawn.Position, giver.PotentialWorkThingRequest, giver.PathMode, RegionTraverseParameters.For (pawn), 9999f, validator, giver.PotentialWorkThingsGlobal);
						if (thing != null)
						{
							targetPack = thing;
							workGiver = giver;
						}
					}
					if (giver.def.scanCells)
					{
						IntVec3 position = pawn.Position;
						float num2 = 99999f;
						foreach (IntVec3 current2 in giver.PotentialWorkLocsGlobal)
						{
							float lengthHorizontalSquared = (current2 - position).LengthHorizontalSquared;
							if (lengthHorizontalSquared < num2 && giver.StartingJobOn (pawn, current2) != null)
							{
								targetPack = current2;
								workGiver = giver;
								num2 = lengthHorizontalSquared;
							}
						}
					}
					num = giver.def.priorityInType;
				}
			}
			if (targetPack.Valid)
			{
				pawn.MindState.lastGivenWorkType = current;
				Job result;
				if (targetPack.HasThing)
				{
					result = workGiver.StartingJobForOn (pawn, targetPack.Thing);
					return result;
				}
				result = workGiver.StartingJobOn (pawn, targetPack.Loc);
				return result;
			}

			return null;
		}
	}
}
