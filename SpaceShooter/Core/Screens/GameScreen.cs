using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Systems;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Screens {
	internal sealed class GameScreen : Screen {
		private readonly World world = new World();
		private readonly EnemyFactory enemyFactory = new EnemyFactory();
		private readonly GameController controller = new GameController();
		private readonly SpriteBatch spriteBatch = new SpriteBatch(Assets.GraphicsDevice);
		private readonly Camera camera = new Camera(Assets.GraphicsDevice);

		public override void Update(GameTime gameTime) {
			enemyFactory.Update(gameTime);
			world.Update(gameTime);
		}

		public override void Draw() {
			spriteBatch.Begin(transformMatrix: camera.Transform);
			world.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
