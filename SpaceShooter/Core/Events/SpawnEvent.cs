using Microsoft.Xna.Framework;
using SpaceShooter.Core.Entities;

namespace SpaceShooter.Core.Events {
	internal sealed class SpawnEvent {
		public Entity Entity { get; }

		public SpawnEvent(Entity entity, Vector2 pos) {
			entity.Position = pos;
			Entity = entity;
		}
	}
}
