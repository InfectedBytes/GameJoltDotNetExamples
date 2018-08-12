using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal sealed class EnemyShip : Ship {
		public EnemyShip() : base(1, Assets.DefaultMissile, Team.Enemy) {
			Region = Assets.Sprites["spaceShips_002"];
		}
	}
}
