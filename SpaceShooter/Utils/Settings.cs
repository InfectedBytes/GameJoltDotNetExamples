namespace SpaceShooter.Utils {
	/// <summary>
	/// GameJolt API settings class. Contains things like GameId and TrophyId.
	/// </summary>
	public sealed class Settings {
		/// <summary>
		/// This is your game's unique id.
		/// </summary>
		public int GameId { get; set; }
		/// <summary>
		/// This is your game's private key. Do not share this key with anyone!
		/// </summary>
		public string PrivateKey { get; set; }
		/// <summary>
		/// For this example project we need a trophy for the first boss.
		/// </summary>
		public int FirstBossTrophy { get; set; }
		/// <summary>
		/// The scoreboard which should be used in this example.
		/// The default scoreboard will be used if this value is 0.
		/// </summary>
		public int Scoreboard { get; set; }

		public bool IsValid() {
			return GameId != 0 && !string.IsNullOrEmpty(PrivateKey) && FirstBossTrophy != 0;
		}
	}
}
