using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Screens {
	internal sealed class MenuScreen : Screen {
		private readonly TextureRegion2D cursor = Assets.Sprites["spaceMissiles_001"];
		private readonly SimpleButton play = new SimpleButton(Assets.FontMedium, "Play", new Point(0, -40), new Point(300, 60), OnClickPlay);
		private readonly SimpleButton exit = new SimpleButton(Assets.FontMedium, "Exit", new Point(0, 40), new Point(300, 60), OnClickExit);

		private readonly List<Label> scores = new List<Label>();
		private readonly List<Label> friends = new List<Label>();

		public MenuScreen() {
			var friendsPos = new Vector2(400, -300);
			friends.Add(new Label(".:Friends:.", friendsPos));
			Game.Jolt.Friends.Fetch(Game.User, ids => {
				if(!ids.Success) return; // error
				if(ids.Data.Length == 0) return; // no friends
				Game.Jolt.Users.Fetch(ids.Data, response => {
					if(!response.Success) return; // error
					foreach(var user in response.Data) {
						friendsPos.Y += 40;
						friends.Add(new Label(user.Name, friendsPos));
					}
				});
			});
		}

		public override void Show() {
			var scorePos = new Vector2(-400, -300);
			scores.Clear();
			scores.Add(new Label(".:Scores:.", scorePos));
			Game.Jolt.Scores.Fetch(callback: response => {
				if(!response.Success) return;
				foreach(var score in response.Data) {
					scorePos.Y += 40;
					scores.Add(new Label($"{score.UserName}: {score.Text}", scorePos));
				}
			});
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			SpriteBatch.Begin(transformMatrix: Camera.Transform);
			play.Draw(SpriteBatch);
			exit.Draw(SpriteBatch);
			foreach(var score in scores) {
				score.Draw(SpriteBatch);
			}
			foreach(var friend in friends) {
				friend.Draw(SpriteBatch);
			}
			SpriteBatch.Draw(cursor, Input.MousePos, MathHelper.ToRadians(-30));
			SpriteBatch.End();
		}

		private static void OnClickPlay() {
			Game.Instance.PushScreen(new GameScreen());
		}

		private static void OnClickExit() {
			Game.Instance.Exit();
		}
	}
}
