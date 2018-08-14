using System;
using System.Collections;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;
using SpaceShooter.Utils;
using SpaceShooter.Utils.External;

namespace SpaceShooter.Core.Systems {
	/// <summary>
	/// Controller class which is responsible for spawning new enemies and their behavior.
	/// The enemy behavior is achieved by coroutines.
	/// </summary>
	internal sealed class EnemyController : BaseSystem {
		public const int BossId = 7;

		private readonly CoroutineRunner runner = new CoroutineRunner();
		private PlayerShip player;
		private readonly MissileDef defaultMissile = new MissileDef("spaceMissiles_002", 1, 2f);
		private readonly MissileDef fastMissile = new MissileDef("spaceMissiles_004", 2, 1f, 600);
		private readonly MissileDef bossMissile = new MissileDef("spaceMissiles_003", 3, 1f);

		public EnemyController() {
			EventBroker.Register<SpawnEvent>(OnSpawned);
			runner.Run(Spawner());
		}

		public override void Update(GameTime gameTime) {
			runner.Update(gameTime.GetElapsedSeconds());
		}

		#region Spawn Methods
		private void SpawnWeakEnemy() {
			var x = Assets.Random.Next(-Consts.ScreenWidth / 2, Consts.ScreenWidth / 2);
			var y = -Consts.ScreenHeight / 2 - 100;
			var ship = new EnemyShip(2, 1, defaultMissile, new Vector2(0, 20));
			runner.Run(SimpleEnemy(ship));
			runner.Run(ConstantFire(ship));
			EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(x, y)));
		}

		private void SpawnStrongEnemy() {
			var x = Assets.Random.Next(-Consts.ScreenWidth / 2, Consts.ScreenWidth / 2);
			var y = -Consts.ScreenHeight / 2 - 100;
			var ship = new EnemyShip(3, 3, fastMissile, new Vector2(0, 20));
			runner.Run(SimpleEnemy(ship));
			runner.Run(ConstantFire(ship));
			EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(x, y)));
		}

		private EnemyShip SpawnBoss() {
			var ship = new EnemyShip(BossId, 7, bossMissile, new Vector2(-25, 35), new Vector2(25, 35));
			runner.Run(BossBehavior(ship));
			runner.Run(ConstantFire(ship));
			EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(0, -Consts.ScreenHeight / 2 - 100)));
			return ship;
		}
		#endregion

		private bool PlayerReached(EnemyShip ship) {
			return Math.Abs(player.Position.X - ship.Position.X) < 20f;
		}

		private IEnumerator Spawner() {
			// this coroutine manages the spawning of the enemies.
			float enemyDelay = 1f; // with each wave, the enemies will spawn faster
			float strongEnemyProbability = 0; // with each wave, it gets more likely to spawn a stronger enemy
			const int enemiesPerWave = 10;
			while(true) {
				// first spawn the enemy waves
				for(int i = 0; i < enemiesPerWave; i++) {
					if(Assets.Random.NextSingle() < strongEnemyProbability)
						SpawnStrongEnemy();
					else
						SpawnWeakEnemy();
					yield return enemyDelay;
				}
				yield return 2f;
				// spawn boss and wait until he's dead
				var boss = SpawnBoss();
				while(!boss.IsDestroyed) {
					yield return null;
				}
				// after each wave, we increase the number of enemies and increase the strong enemy probability
				enemyDelay *= 0.9f;
				strongEnemyProbability += 0.1f;
			}
			// ReSharper disable once IteratorNeverReturns, intended behavior
		}

		private void OnSpawned(SpawnEvent e) {
			if(e.Entity is PlayerShip ship) {
				player = ship;
			}
		}

		#region Enemy Behaviors
		private IEnumerator SimpleEnemy(EnemyShip enemy) {
			// this behavior will just move from left to right and back again.
			float left = -Consts.ScreenWidth / 2f + 20f;
			float right = Consts.ScreenWidth / 2f - 20f;
			float vx = Assets.Random.NextSingle(0.75f, 1f);
			float vy = Assets.Random.NextSingle(0.25f, 0.5f);
			while(!enemy.IsDestroyed) {
				while(enemy.Position.X < right) {
					enemy.Move(vx, vy);
					yield return null;
				}
				while(enemy.Position.X > left) {
					enemy.Move(-vx, vy);
					yield return null;
				}
			}
		}

		private IEnumerator ConstantFire(EnemyShip enemy) {
			// this behavior is used to fire always.
			// the firerate is limited by the missiles cooldown
			while(!enemy.IsDestroyed) {
				enemy.Fire();
				yield return null;
			}
		}

		private IEnumerator BossBehavior(EnemyShip enemy) {
			// this behavior will first move downwards to the center of the screen
			// and then it will follow the player
			enemy.Move(0, 2); // we intentionally ignore the MaxSpeed by applying a factor of 2
			while(!enemy.IsDestroyed && enemy.Position.Y < -100)
				yield return null;
			enemy.Move(0, 0);
			while(!enemy.IsDestroyed) {
				while(enemy.Position.X < player.Position.X && !PlayerReached(enemy)) {
					enemy.Move(1, 0);
					yield return null;
				}
				while(enemy.Position.X > player.Position.X && !PlayerReached(enemy)) {
					enemy.Move(-1, 0);
					yield return null;
				}
				enemy.Move(0, 0);
				yield return null;
			}
		}
		#endregion
	}
}
