using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	internal sealed class Missile : Entity {
		private const float Speed = 350f;
		private Team team;

		public Missile(Team team) {
			this.team = team;
			Region = Assets.Sprites["spaceMissiles_001"];
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
	}
}
