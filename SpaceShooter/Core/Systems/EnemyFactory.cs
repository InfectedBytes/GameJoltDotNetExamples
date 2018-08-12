using System;
using System.Collections;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Systems {
	internal sealed class EnemyFactory {
		private readonly CoroutineRunner runner = new CoroutineRunner();
		private PlayerShip player;

		public EnemyFactory() {
			EventBroker.Register<SpawnEvent>(OnSpawned);
			runner.Run(Spawner());
		}

		public void Update(GameTime gameTime) {
			runner.Update(gameTime.GetElapsedSeconds());
		}

		private IEnumerator Spawner() {
			var viewport = Assets.GraphicsDevice.Viewport;
			float enemyDelay = 1f;
			const int enemiesPerWave = 20;
			while(true) {
				for(int i = 0; i < enemiesPerWave; i++) {
					var x = Assets.Random.Next(-viewport.Width / 2, viewport.Width / 2);
					var y = -viewport.Height / 2 - 100;
					var ship = new EnemyShip(2);
					runner.Run(SimpleEnemy(ship));
					runner.Run(ConstantFire(ship));
					EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(x, y)));
					yield return enemyDelay;
				}
				yield return 2f;
				{
					var ship = new EnemyShip(7);
					runner.Run(BossBehavior(ship));
					runner.Run(ConstantFire(ship));
					EventBroker.Dispatch(new SpawnEvent(ship, new Vector2(0, -viewport.Height / 2 - 100)));
				}
			}
			// ReSharper disable once IteratorNeverReturns, intended behavior
		}

		private void OnSpawned(SpawnEvent e) {
			if(e.Entity is PlayerShip ship) {
				this.player = ship;
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
			enemy.Move(0, 1);
			while(!enemy.IsDestroyed && enemy.Position.Y < 0)
				yield return null;
			enemy.Move(0, 0);
			while(!enemy.IsDestroyed) {
				while(enemy.Position.X < player.Position.X) {
					enemy.Move(1, 0);
					yield return null;
				}
				while(enemy.Position.X > player.Position.X) {
					enemy.Move(-1, 0);
					yield return null;
				}
			}
		}
	}
}
