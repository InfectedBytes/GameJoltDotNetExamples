using Microsoft.Xna.Framework;
using SpaceShooter.Core.Entities;

namespace SpaceShooter.Core.Events {
	/// <summary>
	/// This event is dispatched whenever a new entity shall be spawned.
	/// </summary>
	internal sealed class SpawnEvent {
		public Entity Entity { get; }

		public SpawnEvent(Entity entity, Vector2 pos) {
			entity.Position = pos;
			Entity = entity;
		}
	}
}
