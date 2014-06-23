using System;
using System.Collections.Generic;

namespace Verse.AI
{
	public class JobDriver_DroidCharge : JobDriver
	{
		public JobDriver_DroidCharge (Pawn pawn) : base(pawn)
		{
		}

		protected override IEnumerable<Toil> MakeNewToils() {
			Building_DroidCharger building = (Building_DroidCharger) TargetThingA;

			// Go to the charging pad, but stop trying if it isn't on or is taken.
			Toil gotoToil = Toils_Goto.GotoThing (TargetIndex.A, PathMode.OnSquare);
			gotoToil.AddFailCondition (() => {
				return !building.IsOnAndAvailable(pawn);
			});
			yield return gotoToil;

			// Charge until at least 99% full, or the charger is off or taken.
			DroidPawn droid = (DroidPawn)this.pawn;
			Toil chargeToil = new Toil ();
			chargeToil.initAction = () => {
				if (building.Position != pawn.Position) {
					pawn.jobs.EndCurrentJob (JobCondition.Errored);
				}
			};
			chargeToil.defaultCompleteMode = ToilCompleteMode.Never;
			chargeToil.AddFailCondition (() => {
				return !building.IsOnAndAvailable(pawn);
			});
			chargeToil.AddEndCondition (() => {
				if (droid.storedEnergy >= 0.99 * DroidPawn.storedEnergyMax) {
					return JobCondition.Succeeded;
				}
				return JobCondition.Ongoing;
			});

			yield return chargeToil;
		}
	}
}