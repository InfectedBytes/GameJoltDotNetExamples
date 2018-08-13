using Microsoft.Xna.Framework;

namespace SpaceShooter.Core.Entities {
	internal sealed class EnemyShip : Ship {
		public EnemyShip(int shipId, int health, MissileDef missileDef, params Vector2[] missileSpawns) : 
			base(shipId, health, missileDef, missileSpawns, Team.Enemy) { }
	}
}
