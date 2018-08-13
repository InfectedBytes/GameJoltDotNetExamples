using System;
using System.Collections;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;
using SpaceShooter.Utils;
using SpaceShooter.Utils.External;

namespace SpaceShooter.Core.Systems {
	internal sealed class EnemyController : BaseSystem {
		private readonly CoroutineRunner runner = new CoroutineRunner();
		private PlayerShip player;
		private readonly MissileDef defaultMissile = new MissileDef("spaceMissiles_002", 1, 2f);
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
			var ship = new EnemyShip(2, 1, defaultMissile);
			runner.Run(SimpleEnemy(ship));
			runner.Run(ConstantFire(ship));
			EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(x, y)));
		}

		private EnemyShip SpawnBoss() {
			var ship = new EnemyShip(7, 5, bossMissile);
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
			float enemyDelay = 1f;
			const int enemiesPerWave = 10;
			while(true) {
				for(int i = 0; i < enemiesPerWave; i++) {
					SpawnWeakEnemy();
					yield return enemyDelay;
				}
				yield return 2f;
				var boss = SpawnBoss();
				while(!boss.IsDestroyed) {
					yield return null;
				}
			}
			// ReSharper disable once IteratorNeverReturns, intended behavior
		}

		private void OnSpawned(SpawnEvent e) {
			if(e.Entity is PlayerShip ship) {
				player = ship;
			}
		}

		private IEnumerator SimpleEnemy(EnemyShip enemy) {
			var viewport = Assets.GraphicsDevice.Viewport;
			float left = -viewport.Width / 2f + 20f;
			float right = viewport.Width / 2f - 20f;
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
			while(!enemy.IsDestroyed) {
				enemy.Fire();
				yield return null;
			}
		}

		private IEnumerator BossBehavior(EnemyShip enemy) {
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
	}
}
