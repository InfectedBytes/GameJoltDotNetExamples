namespace SpaceShooter.Core {
	internal abstract class Ship : Entity {
		protected Ship(Team team = Team.None) : base(team) { }
	}
}
