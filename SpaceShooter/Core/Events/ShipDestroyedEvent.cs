using SpaceShooter.Core.Entities;

namespace SpaceShooter.Core.Events {
	/// <summary>
	/// Event which is dispatched whenever a ship is destroyed through damage taking.
	/// This event is not dispatched for ships leaving the visible area.
	/// </summary>
	internal sealed class ShipDestroyedEvent {
		public Ship Ship { get; }
		public Team DestroyedBy { get; }

		public ShipDestroyedEvent(Ship ship, Team destroyedBy) {
			Ship = ship;
			DestroyedBy = destroyedBy;
		}
	}
}
