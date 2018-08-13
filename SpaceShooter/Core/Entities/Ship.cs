using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpaceShooter.Core.Events;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal abstract class Ship : Entity {
		public int ShipId { get; }
		public int MaxHealth { get;}
		public int Health { get; private set; }
		public float MaxSpeed { get; protected set; } = 250f;
		public MissileDef CurrentMissile { get; private set; }
		private float cooldown;
		private readonly Vector2[] missileSpawns;

		protected Ship(int shipId, int health, MissileDef missileDef, Vector2[] missileSpawns, Team team = Team.None) :
			base(team) {
			ShipId = shipId;
			this.missileSpawns = missileSpawns;
			Region = Assets.Sprites[$"spaceShips_{shipId:D3}"];
			MaxHealth = Health = health;
			ChangeMissile(missileDef);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			cooldown -= gameTime.GetElapsedSeconds();
		}

		public void Move(float dx, float dy) {
			Velocity = new Vector2(dx * MaxSpeed, dy * MaxSpeed);
		}

		public void ChangeMissile(MissileDef missileDef) {
			CurrentMissile = missileDef ?? throw new ArgumentNullException(nameof(missileDef));
		}

		public void Damage(Team team, int damage) {
			if(team == Team) return;
			if(damage <= 0) return;
			Health -= damage;
			if(Health <= 0) {
				Destroy();
				EventBroker.Dispatch(new ShipDestroyedEvent(this, team));
				// spawn a few explosions
				for(int i = 0; i < 2; i++) {
					var x = Assets.Random.Next(-Region.Width / 2, Region.Width / 2);
					var y = Assets.Random.Next(-Region.Height / 2, Region.Height / 2);
					EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position + new Vector2(x, y)));
				}
			}
		}

		public void Heal(int heal) {
			if(heal <= 0) return;
			Health = Math.Min(MaxHealth, Health + heal);
		}

		public void Fire() {
			if(cooldown > 0) return;
			cooldown = CurrentMissile.Cooldown;
			var pos = Position + missileSpawns[Assets.Random.Next(0, missileSpawns.Length)];
			EventBroker.Dispatch(new SpawnEvent(new Missile(CurrentMissile, Team), pos));
		}

		public override void OnCollide(Entity other) {
			if(Team != other.Team && other is Ship ship) {
				ship.Damage(Team, 5);
				EventBroker.Dispatch(new SpawnEvent(new Explosion(), Position));
			}
		}
	}
}
