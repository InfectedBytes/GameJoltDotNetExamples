using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceShooter.Core.Events;
using SpaceShooter.Core.Systems;

namespace SpaceShooter.Core.Screens {
	/// <summary>
	/// Basic Gameplay screen. Most of it's logic is done by the underlying Systems, like <see cref="GameController"/>.
	/// </summary>
	internal sealed class GameScreen : Screen {
		private readonly List<BaseSystem> systems;

		public GameScreen() : base(500) {
			EventBroker.Clear(); // just to make sure we don't have any dead event handlers
			systems = new List<BaseSystem> {
				new World(),
				new EnemyController(),
				new GameController()
			};
		}

		public override void Unload() {
			base.Unload();
			foreach(var system in systems) {
				system.Unload();
			}
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			foreach(var system in systems) {
				system.Update(gameTime);
			}
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			SpriteBatch.Begin(transformMatrix: Camera.Transform);
			foreach(var system in systems) {
				system.Draw(SpriteBatch);
			}
			SpriteBatch.End();
		}
	}
}
