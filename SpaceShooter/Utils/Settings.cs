namespace SpaceShooter.Utils {
	public sealed class Settings {
		public int GameId { get; set; }
		public string PrivateKey { get; set; }

		public bool IsValid() {
			return GameId != 0 && !string.IsNullOrEmpty(PrivateKey);
		}
	}
}
