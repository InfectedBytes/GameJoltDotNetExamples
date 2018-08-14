using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Events;

namespace SpaceShooter.Core.Entities {
	internal sealed class Missile : Entity {
		private readonly MissileDef missileDef;

		public Missile(MissileDef missileDef, Team team) : base(team) {
			this.missileDef = missileDef;
			Region = missileDef.Texture;
			switch(team) {
				case Team.Player:
					Velocity = new Vector2(0, -missileDef.Speed);
					break;
				case Team.Enemy:
					Velocity = new Vector2(0, missileDef.Speed);
					Effects = SpriteEffects.FlipVertically;
					break;
				default:
					throw new ArgumentException(nameof(team));
			}
		}

		public override void OnCollide(Entity other) {
			// ignore entities from the same team
			if(Team != other.Team) {
				if(other is Ship ship) {
					ship.Damage(Team, missileDef.Damage);
					EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position));
					Destroy();
				} else if(other is Missile) {
					Destroy();
					EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position));
				}
			}
		}
	}
}
