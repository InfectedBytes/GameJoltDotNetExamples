using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceShooter.Core.Systems;

namespace SpaceShooter.Core.Screens {
	internal sealed class GameScreen : Screen {
		private readonly List<BaseSystem> systems;

		public GameScreen() : base(500) {
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
