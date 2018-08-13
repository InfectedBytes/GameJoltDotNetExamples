using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Core;

namespace SpaceShooter.Utils {
	internal static class Assets {
		public static Random Random { get; } = new Random();

		public static GraphicsDevice GraphicsDevice { get; private set; }
		public static SpriteSheetAnimationFactory ExplosionAnimation { get; private set; }
		public static TextureAtlas Sprites { get; private set; }

		public static SpriteFont FontSmall { get; private set; }
		public static SpriteFont FontMedium { get; private set; }
		public static SpriteFont FontHuge { get; private set; }

		public static MissileDef DefaultMissile => new MissileDef("spaceMissiles_001", 1, 0.5f);
		public static MissileDef DefaultEnemyMissile => new MissileDef("spaceMissiles_002", 1, 2f);

		public static void Load(GraphicsDevice device, ContentManager content) {
			GraphicsDevice = device;
			ExplosionAnimation = content.Load<SpriteSheetAnimationFactory>("explosion-animation");
			Sprites = content.Load<TextureAtlas>("spacesprites-sheet");
			FontSmall = content.Load<SpriteFont>("fonts/small");
			FontMedium = content.Load<SpriteFont>("fonts/medium");
			FontHuge = content.Load<SpriteFont>("fonts/huge");
		}

		public static void Dispose() {
			GraphicsDevice = null;
			ExplosionAnimation = null;
			Sprites = null;
			FontSmall = FontMedium = FontHuge = null;
		}
	}
}
