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
		private readonly Vector2 healthPosition;
		private readonly Vector2 pointsPosition;
		private readonly Vector2 gameoverPosition;
		private readonly Vector2 exitTextPosition;
		private bool gameover;

		private Color HealthColor {
			get {
				if(player.Health >= player.MaxHealth * 3 / 4) return Color.Green;
				if(player.Health > player.MaxHealth / 4) return Color.Yellow;
				return Color.Red;
			}
		}

		public GameController() {
			EventBroker.Register<ShipDestroyedEvent>(OnDestroyed);
			player = new PlayerShip();
			EventBroker.Dispatch(new SpawnEvent(player, Vector2.Zero));
			healthPosition = new Vector2(-Consts.ScreenWidth / 2f, -Consts.ScreenHeight / 2f);
			pointsPosition = new Vector2(healthPosition.X, healthPosition.Y + 30);
			var gameoverSize = Assets.FontHuge.MeasureString(GameoverText);
			gameoverPosition = -gameoverSize / 2f;
			var exitTextSize = Assets.FontMedium.MeasureString(ExitText);
			exitTextPosition = new Vector2(-exitTextSize.X / 2f, gameoverSize.Y / 2f + 20);
		}

		private void OnDestroyed(ShipDestroyedEvent e) {
			if(e.DestroyedBy == Team.Player && e.Ship is EnemyShip enemy) {
				Points += enemy.MaxHealth; // we just use the max health of the enemy as his "value"
			} else if(e.Ship is PlayerShip) {
				gameover = true;
			}
		}

		public override void Update(GameTime gameTime) {
			if(gameover && Input.IsKeyJustPressed(Keys.Escape)) {
				Game.Instance.PopScreen();
			}
		}

		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.DrawString(Assets.FontSmall, $"Health: {player.Health}", healthPosition, HealthColor);
			spriteBatch.DrawString(Assets.FontSmall, $"Points: {Points}", pointsPosition, Color.White);
			if(gameover) {
				spriteBatch.DrawString(Assets.FontHuge, GameoverText, gameoverPosition, Color.White);
				spriteBatch.DrawString(Assets.FontMedium, ExitText, exitTextPosition, Color.White);
			}
		}
	}
}
