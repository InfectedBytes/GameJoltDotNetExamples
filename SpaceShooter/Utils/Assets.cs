using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;

namespace SpaceShooter.Utils {
	public static class Assets {
		public static GraphicsDevice GraphicsDevice { get; private set; }
		public static SpriteSheetAnimationFactory ExplosionAnimation { get; private set; }
		public static TextureAtlas Sprites { get; private set; }

		public static void Load(GraphicsDevice device, ContentManager content) {
			GraphicsDevice = device;
			ExplosionAnimation = content.Load<SpriteSheetAnimationFactory>("explosion-animation");
			Sprites = content.Load<TextureAtlas>("spacesprites-sheet");
		}

		public static void Dispose() {
			GraphicsDevice = null;
			ExplosionAnimation = null;
			Sprites = null;
		}
	}
}
