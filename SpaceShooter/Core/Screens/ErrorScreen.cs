using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Screens {
	internal sealed class ErrorScreen : Screen {
		private readonly List<Label> messages = new List<Label>();

		public ErrorScreen() {
			var lineHeight = 40;
			var pos = new Vector2(0, 0);
			messages.Add(new Label("Invalid GameId/PrivateKey!", pos));
			pos.Y += lineHeight;
			messages.Add(new Label("Please edit the 'Settings.json' file to continue.", pos));
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			SpriteBatch.Begin(transformMatrix: Camera.Transform);
			foreach(var msg in messages) {
				msg.Draw(SpriteBatch);
			}
			SpriteBatch.End();
		}
	}
}
