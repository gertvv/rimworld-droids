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
			yield return Toils_Goto.GotoThing (TargetIndex.A, PathMode.OnSquare);

			Building building = (Building) TargetThingA;
			DroidPawn droid = (DroidPawn)this.pawn;

			Toil chargeToil = new Toil ();
			chargeToil.initAction = () => {
				if (building.Position != pawn.Position) {
					pawn.jobs.EndCurrentJob (JobCondition.Errored);
				}
			};
			chargeToil.defaultCompleteMode = ToilCompleteMode.Never;
			chargeToil.AddFailCondition (() => {
				return !building.GetComp<CompPowerTrader>().PowerOn;
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