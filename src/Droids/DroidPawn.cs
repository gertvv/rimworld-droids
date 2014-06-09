using System;
using Verse.AI;
namespace Verse
{
	public class DroidPawn : Pawn
	{
		public DroidPawn () : base()
		{
		}

		public void shutdown() {
			this.Destroy(DestroyMode.Vanish);
			String kindName = base.kindDef.defName + "Inactive";
			ThingDef def = ThingDef.Named (kindName);
			Thing thing = ThingMaker.MakeThing (def);
			thing.health = base.health;
			GenSpawn.Spawn (thing, base.Position);
		}
	}
}