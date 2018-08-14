using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	/// <summary>
	/// Basic settings for missile.
	/// </summary>
	internal sealed class MissileDef {
		public TextureRegion2D Texture { get; }
		public int Damage { get; }
		public float Cooldown { get; }
		public float Speed { get; }

		public MissileDef(string assetName, int damage, float cooldown, float speed = 500f) {
			Texture = Assets.Sprites[assetName];
			Damage = damage;
			Cooldown = cooldown;
			Speed = speed;
		}
	}
}
