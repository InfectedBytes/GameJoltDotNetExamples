using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;

namespace SpaceShooter.Utils {
	internal static class Extensions {
		public static void Draw(this SpriteBatch spriteBatch, TextureRegion2D region, 
			Vector2 position, float rotation = 0f, Vector2 origin = default(Vector2),
			SpriteEffects effects = SpriteEffects.None) {
			spriteBatch.Draw(region.Texture, position, region.Bounds, Color.White, rotation, 
				origin, Vector2.One, effects, 0);
		}

		public static void Draw(this SpriteBatch spriteBatch, SpriteSheetAnimation animation,
			Vector2 position, float rotation = 0f, Vector2 origin = default(Vector2),
			SpriteEffects effects = SpriteEffects.None) {
			spriteBatch.Draw(animation.CurrentFrame, position, rotation, origin, effects);
		}

		public static Vector2 Clamp(this Vector2 vector, Rectangle bounds) {
			return new Vector2(
				MathHelper.Clamp(vector.X, bounds.Left, bounds.Right),
				MathHelper.Clamp(vector.Y, bounds.Top, bounds.Bottom)
			);
		}
	}
}
