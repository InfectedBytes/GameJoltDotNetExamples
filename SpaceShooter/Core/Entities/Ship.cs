using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal abstract class Ship : Entity {
		public int MaxHealth { get;}
		public int Health { get; private set; }
		public MissileDef CurrentMissile { get; private set; }
		private float cooldown;

		protected Ship(int health, MissileDef missileDef, Team team = Team.None) : base(team) {
			MaxHealth = Health = health;
			ChangeMissile(missileDef);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			cooldown -= gameTime.GetElapsedSeconds();
		}

		public void ChangeMissile(MissileDef missileDef) {
			CurrentMissile = missileDef ?? throw new ArgumentNullException(nameof(missileDef));
		}

		public void Damage(int damage) {
			if(damage <= 0) return;
			Health -= damage;
			if(Health <= 0) Destroy();
		}

		public void Heal(int heal) {
			if(heal <= 0) return;
			Health = Math.Min(MaxHealth, Health + heal);
		}

		public void Fire() {
			if(cooldown > 0) return;
			cooldown = CurrentMissile.Cooldown;
			World.Add(new Missile(CurrentMissile, Team) {Position = Position});
		}

		public override void Destroy() {
			base.Destroy();
			// spawn a few explosions
			for(int i = 0; i < 2; i++) {
				var x = Assets.Random.Next(-Region.Width / 2, Region.Width / 2);
				var y = Assets.Random.Next(-Region.Height / 2, Region.Height / 2);
				World.Add(new Explosion {Position = Position + new Vector2(x, y)});
			}
		}
	}
}
