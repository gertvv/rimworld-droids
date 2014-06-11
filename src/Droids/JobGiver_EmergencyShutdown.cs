using System;
using Verse;

namespace Verse.AI
{
	public class JobGiver_EmergencyShutdown : ThinkNode_JobGiver
	{
		public JobGiver_EmergencyShutdown () : base()
		{
		}

		protected override Job TryGiveTerminalJob (Pawn pawn)
		{
			if (!(pawn is DroidPawn)) {
				return null;
			}

			if (pawn.health < DroidPawn.emergencyShutdownThreshold) {
				return new Job (DefDatabase<JobDef>.GetNamed("EmergencyShutdown"));
			}

			return null;
		}
	}
}

