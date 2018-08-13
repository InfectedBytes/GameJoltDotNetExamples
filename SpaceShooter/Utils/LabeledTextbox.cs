using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SpaceShooter.Utils.External.MonoGameTextbox;

namespace SpaceShooter.Utils {
	internal sealed class LabeledTextbox {
		private readonly SpriteFont font;
		private readonly string text;
		private readonly TextBox textbox;
		private readonly Rectangle bounds;
		private readonly Vector2 labelPosition;

		public bool Active {
			get => textbox.Active;
			set => textbox.Active = value;
		}

		public string Text => textbox.Text.String;

		public LabeledTextbox(SpriteFont font, string text, Point center, Point size) {
			this.font = font;
			this.text = text;
			bounds = new Rectangle(center.X - size.X / 2, center.Y - size.Y / 2, size.X, size.Y);
			var innerBounds = bounds;
			innerBounds.Inflate(-8, -4);
			textbox = new TextBox(innerBounds, 128, "", Assets.GraphicsDevice, font, Color.Black, Color.LightBlue, 30);
			labelPosition = textbox.Area.Location.ToVector2();
			labelPosition.X -= 200;
		}

		public bool Contains(Vector2 point) {
			// ReSharper disable once ImpureMethodCallOnReadonlyValueField
			return bounds.Contains(point);
		}

		public void Update() {
			textbox.Update();
		}

		public void Draw(SpriteBatch spriteBatch) {
			spriteBatch.FillRectangle(bounds, Color.White);
			spriteBatch.DrawRectangle(bounds, Color.Black);
			textbox.Draw(spriteBatch);
			spriteBatch.DrawString(font, text, labelPosition, Color.White);
		}

		public void Dispose() {
			textbox.Dispose();
		}
	}
}
