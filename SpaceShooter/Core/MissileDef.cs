using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	internal sealed class MissileDef {
		public TextureRegion2D Texture { get; }
		public int Damage { get; }
		public float Cooldown { get; }

		public MissileDef(string assetName, int damage, float cooldown) {
			Texture = Assets.Sprites[assetName];
			Damage = damage;
			Cooldown = cooldown;
		}
	}
}
