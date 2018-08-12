using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Core.Entities {
	internal sealed class Missile : Entity {
		private const float Speed = 350f;
		private readonly MissileDef missileDef;
		private readonly Team team;

		public Missile(MissileDef missileDef, Team team) {
			this.missileDef = missileDef;
			this.team = team;
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
			if(team != other.Team && other is Ship ship) {
				ship.Damage(missileDef.Damage);
				World.Add(new Explosion {Position = Position});
				Destroy();
			}
		}
	}
}
