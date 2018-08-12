using System;
using Microsoft.Xna.Framework;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;

namespace SpaceShooter.Core.Systems {
	internal sealed class GameController {
		public int Points { get; private set; }

		public GameController() {
			EventBroker.Register<ShipDestroyedEvent>(OnDestroyed);
			EventBroker.Dispatch(new SpawnEvent(new PlayerShip(), Vector2.Zero));
		}

		private void OnDestroyed(ShipDestroyedEvent e) {
			if(e.DestroyedBy == Team.Player && e.Ship is EnemyShip enemy) {
				Points += enemy.MaxHealth; // we just use the max health of the enemy as his "value"
				Console.WriteLine($"Points: {Points}");
			}
		}
	}
}
