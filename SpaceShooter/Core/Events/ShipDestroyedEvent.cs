using SpaceShooter.Core.Entities;

namespace SpaceShooter.Core.Events {
	internal sealed class ShipDestroyedEvent {
		public Ship Ship { get; }
		public Team DestroyedBy { get; }

		public ShipDestroyedEvent(Ship ship, Team destroyedBy) {
			Ship = ship;
			DestroyedBy = destroyedBy;
		}
	}
}
