using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Events;

namespace SpaceShooter.Core.Entities {
	internal sealed class Missile : Entity {
		private const float Speed = 500f;
		private readonly MissileDef missileDef;

		public Missile(MissileDef missileDef, Team team) : base(team) {
			this.missileDef = missileDef;
			Region = missileDef.Texture;
			switch(team) {
				case Team.Player:
					Velocity = new Vector2(0, -Speed);
					break;
				case Team.Enemy:
					Velocity = new Vector2(0, Speed);
					Effects = SpriteEffects.FlipVertically;
					break;
				default:
					throw new ArgumentException(nameof(team));
			}
		}

		public override void OnCollide(Entity other) {
			if(Team != other.Team) {
				if(other is Ship ship) {
					ship.Damage(Team, missileDef.Damage);
					EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position));
					Destroy();
				} else if(other is Missile missile) {
					Destroy();
					missile.Destroy();
					EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position));
				}
			}
		}
	}
}
