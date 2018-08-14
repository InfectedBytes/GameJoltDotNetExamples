using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Utils {
	/// <summary>
	/// Simple label element which is automatically sized to fit the given text.
	/// </summary>
	internal sealed class Label {
		public string Text { get; set; }
		private readonly Vector2 center;

		public Label(string text, Vector2 center) {
			Text = text;
			this.center = center;
		}

		public void Draw(SpriteBatch spriteBatch) {
			if(string.IsNullOrEmpty(Text)) return;
			var size = Assets.FontSmall.MeasureString(Text);
			var pos = center - size / 2f;
			spriteBatch.DrawString(Assets.FontSmall, Text, pos, Color.White);
		}
	}
}
