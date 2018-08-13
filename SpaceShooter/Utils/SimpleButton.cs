using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
// ReSharper disable ImpureMethodCallOnReadonlyValueField

namespace SpaceShooter.Utils {
	internal sealed class SimpleButton {
		private readonly Color backgroundColor = Color.DarkBlue;
		private readonly Color defaultTextColor = Color.White;
		private readonly Color highlightTextColor = Color.Red;
		private readonly SpriteFont font;
		private readonly string text;
		private readonly Rectangle bounds;
		private readonly Vector2 textPosition;
		private readonly Action onClick;

		public SimpleButton(SpriteFont font, string text, Point center, Point size, Action onClick) {
			this.font = font;
			this.text = text;
			this.onClick = onClick;
			bounds = new Rectangle(center.X - size.X / 2, center.Y - size.Y / 2, size.X, size.Y);
			textPosition = center.ToVector2() - font.MeasureString(text) / 2f;
		}

		public void Draw(SpriteBatch spriteBatch) {
			var textColor = bounds.Contains(Input.MousePos) ? highlightTextColor : defaultTextColor;
			spriteBatch.FillRectangle(bounds, backgroundColor);
			spriteBatch.DrawString(font, text, textPosition, textColor);
			if(bounds.Contains(Input.MousePos) && Input.IsLeftMouseJustReleased()) {
				onClick();
			}
		}
	}
}
