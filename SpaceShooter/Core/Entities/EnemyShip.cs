using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal sealed class EnemyShip : Ship {
		public EnemyShip(int shipId) : base(shipId, 1, Assets.DefaultEnemyMissile, Team.Enemy) { }
	}
}
