using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	internal sealed class EnemyShip : Ship {
		public EnemyShip() : base(Team.Enemy) {
			Region = Assets.Sprites["spaceShips_002"];
		}
	}
}
