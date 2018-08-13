namespace SpaceShooter.Core.Entities {
	internal sealed class EnemyShip : Ship {
		public EnemyShip(int shipId, int health, MissileDef missileDef) : base(shipId, health, missileDef, Team.Enemy) { }
	}
}
