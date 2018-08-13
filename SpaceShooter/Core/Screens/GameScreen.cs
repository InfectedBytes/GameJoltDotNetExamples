using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Systems;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Screens {
	internal sealed class GameScreen : Screen {
		private readonly World world = new World();
		private readonly EnemyController enemyController = new EnemyController();
		private readonly GameController controller = new GameController();
		private readonly SpriteBatch spriteBatch = new SpriteBatch(Assets.GraphicsDevice);
		private readonly Camera camera = new Camera(Assets.GraphicsDevice);

		public override void Update(GameTime gameTime) {
			enemyController.Update(gameTime);
			world.Update(gameTime);
			controller.Update(gameTime);
		}

		public override void Draw() {
			spriteBatch.Begin(transformMatrix: camera.Transform);
			world.Draw(spriteBatch);
			controller.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
