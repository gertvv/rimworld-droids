using System;
using System.Collections.Generic;

namespace Verse.AI
{
	public class JobDriver_EmergencyShutdown : JobDriver
	{
		public JobDriver_EmergencyShutdown (Pawn pawn) : base(pawn)
		{
		}

		protected override IEnumerable<Toil> MakeNewToils() {
			Toil shutdown = new Toil ();
			shutdown.initAction = () =>
			{
				((DroidPawn)pawn).shutdown();
			};
			shutdown.defaultCompleteMode = ToilCompleteMode.Instant;
			yield return shutdown;
		}
	}
}

