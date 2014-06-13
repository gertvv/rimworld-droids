using System;
using Verse;

namespace Verse.AI
{
	public class WorkGiver_RepairDroidsInactive : WorkGiver
	{
		//
		// Properties
		//
		public override PathMode PathMode
		{
			get
			{
				return PathMode.Touch;
			}
		}
		public override ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForGroup (ThingRequestGroup.HaulableAlways);
			}
		}
		//
		// Constructors
		//
		public WorkGiver_RepairDroidsInactive (WorkGiverDef giverDef) : base (giverDef)
		{
		}
		//
		// Methods
		//
		public override Job StartingJobForOn (Pawn pawn, Thing t)
		{
			if (!(t is DroidInactive))
			{
				return null;
			}
			if (!Find.HomeRegionGrid.Get (t.Position))
			{
				return null;
			}
			DroidInactive droid = (DroidInactive) t;
			if (!droid.def.useStandardHealth || droid.health >= droid.def.maxHealth)
			{
				return null;
			}
			if (!pawn.CanReserve (droid, ReservationType.Total))
			{
				Log.Message ("Failed to reserve!");
				return null;
			}
			if (droid.IsBurning ())
			{
				Log.Message ("On fire!");
				return null;
			}
			Log.Message ("Giving Job");
			return new Job (JobDefOf.Repair, new TargetPack (t));
		}
	}
}

