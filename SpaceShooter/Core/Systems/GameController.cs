using System.Linq;
using GameJolt.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Systems {
	internal sealed class GameController : BaseSystem {
		private const string GameoverText = "Gameover";
		private const string ExitText = "Press ESC to exit";
		public int Points { get; private set; }
		private readonly PlayerShip player;
		private readonly Vector2 topLeft;
		private readonly Vector2 gameoverPosition;
		private readonly Vector2 exitTextPosition;
		private Score userHighscore, globalHighscore;
		private bool gameover;
		private bool hasTrophyAchieved;
		private int waveCount;
		private string message;

		public GameController() {
			EventBroker.Register<ShipDestroyedEvent>(OnDestroyed);
			player = new PlayerShip();
			EventBroker.Dispatch(new SpawnEvent(player, Vector2.Zero));
			topLeft = new Vector2(-Consts.ScreenWidth / 2f, -Consts.ScreenHeight / 2f);
			var gameoverSize = Assets.FontHuge.MeasureString(GameoverText);
			gameoverPosition = -gameoverSize / 2f;
			var exitTextSize = Assets.FontMedium.MeasureString(ExitText);
			exitTextPosition = new Vector2(-exitTextSize.X / 2f, gameoverSize.Y / 2f + 20);
			// we want to get the best score of the signed in player,
			// therefore we have to provide the user's credentials as the first argument
			Game.Jolt.Scores.Fetch(Game.User, callback: response => {
				if(response.Success) userHighscore = response.Data.FirstOrDefault();
			});
			// we also want to get the globally best score
			Game.Jolt.Scores.Fetch(callback: response => {
				if(response.Success) globalHighscore = response.Data.FirstOrDefault();
			});
			Game.Jolt.Trophies.Fetch(Game.User, ids: new[] {Game.Settings.FirstBossTrophy}, callback: response => {
				if(response.Success) hasTrophyAchieved = response.Data.First().Achieved;
			});
		}

		private void OnDestroyed(ShipDestroyedEvent e) {
			if(e.DestroyedBy == Team.Player && e.Ship is EnemyShip enemy) {
				Points += enemy.MaxHealth; // we just use the max health of the enemy as his "value"
				if(enemy.ShipId == EnemyController.BossId) {
					if(waveCount == 0 && !hasTrophyAchieved) {
						Game.Jolt.Trophies.SetAchieved(Game.User, Game.Settings.FirstBossTrophy);
						message = "Unlocked trophy!";
					}
					waveCount++; // wave finished
				}
			} else if(e.Ship is PlayerShip) {
				gameover = true;
				Game.Jolt.Scores.Add(Game.User, Points, $"{Points}P");
			}
		}

		public override void Update(GameTime gameTime) {
			if(gameover && Input.IsKeyJustPressed(Keys.Escape)) {
				Game.Instance.PopScreen();
			}
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if(message != null)
				spriteBatch.DrawString(Assets.FontSmall, message, new Vector2(0, -Consts.ScreenHeight / 2f), Color.White);
			var userScoreText = userHighscore != null ? userHighscore.Text : "-";
			var globalScoreText = globalHighscore != null ? $"{globalHighscore.Text} ({globalHighscore.UserName})" : "-";
			spriteBatch.DrawString(Assets.FontSmall,
$@"Health: {player.Health}
Points: {Points}
Your highscore: {userScoreText}
Global highscore: {globalScoreText}", topLeft, Color.White);
			if(gameover) {
				spriteBatch.DrawString(Assets.FontHuge, GameoverText, gameoverPosition, Color.White);
				spriteBatch.DrawString(Assets.FontMedium, ExitText, exitTextPosition, Color.White);
			}
		}
	}
}
