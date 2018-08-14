using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Screens {
	/// <summary>
	/// Simple main menu with Play and Exit buttons. 
	/// Additionally this screen is showing some infos fetched from GameJolt, like highscores.
	/// </summary>
	internal sealed class MenuScreen : Screen {
		private readonly TextureRegion2D cursor = Assets.Sprites["spaceMissiles_001"];
		private readonly SimpleButton play = new SimpleButton(Assets.FontMedium, "Play", new Point(0, -40), new Point(300, 60), OnClickPlay);
		private readonly SimpleButton exit = new SimpleButton(Assets.FontMedium, "Exit", new Point(0, 40), new Point(300, 60), OnClickExit);

		private readonly List<Label> scores = new List<Label>();
		private readonly List<Label> trophies = new List<Label>();
		private readonly List<Label> friends = new List<Label>();

		public override void Show() {
			/****************************\
			|* GameJoltDotNet API usage *|
			\****************************/
			var scorePos = new Vector2(-400, -300);
			scores.Clear();
			scores.Add(new Label(".:Scores:.", scorePos));
			// fetch the top 10 scores
			Game.Jolt.Scores.Fetch(callback: response => {
				if(!response.Success) return; // error
				// iterate over the received scores and store the username and score to a label
				foreach(var score in response.Data) {
					scorePos.Y += 40;
					scores.Add(new Label($"{score.UserName}: {score.Text}", scorePos));
				}
			});
			var trophyPos = new Vector2(0, -300);
			trophies.Clear();
			trophies.Add(new Label(".:Unlocked Trophies:.", trophyPos));
			// fetch a list of achieved trophies
			Game.Jolt.Trophies.Fetch(Game.User, true, callback: response => {
				if(!response.Success) return; // error
				// iterate over all achieved trophies and store the title and difficulty to a label
				foreach(var trophy in response.Data) {
					trophyPos.Y += 40;
					trophies.Add(new Label($"{trophy.Title} ({trophy.Difficulty})", trophyPos));
				}
			});
			var friendsPos = new Vector2(400, -300);
			friends.Add(new Label(".:Friends:.", friendsPos));
			// fetch a list of the users friends.
			// First we fetch the list of user ids
			Game.Jolt.Friends.Fetch(Game.User, ids => {
				if(!ids.Success) return; // failed to fetch friend's ids
				if(ids.Data.Length == 0) return; // no friends -> nothing to do
				// now we fetch the actual user info for the user's friends
				Game.Jolt.Users.Fetch(ids.Data, response => {
					if(!response.Success) return; // error
					// iterate over all friends and store their name in a label
					foreach(var user in response.Data) {
						friendsPos.Y += 40;
						friends.Add(new Label(user.Name, friendsPos));
					}
				});
			});
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			SpriteBatch.Begin(transformMatrix: Camera.Transform);
			play.Draw(SpriteBatch);
			exit.Draw(SpriteBatch);
			foreach(var score in scores)
				score.Draw(SpriteBatch);
			foreach(var trophy in trophies)
				trophy.Draw(SpriteBatch);
			foreach(var friend in friends)
				friend.Draw(SpriteBatch);
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
