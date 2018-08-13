using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Systems;
using SpaceShooter.Utils;
using SpaceShooter.Utils.External;

namespace SpaceShooter.Core.Screens {
	internal sealed class GameScreen : Screen {
		private readonly List<BaseSystem> systems;
		private readonly SpriteBatch spriteBatch = new SpriteBatch(Assets.GraphicsDevice);
		private readonly Camera camera = new Camera(Assets.GraphicsDevice);
		private readonly Starfield starfield;
		private float movement;
		private Vector2 StarfieldPosition => new Vector2(0, -500 * movement);

		public GameScreen() {
			systems = new List<BaseSystem> {
				new World(),
				new EnemyController(),
				new GameController()
			};
			starfield = new Starfield(StarfieldPosition, Assets.GraphicsDevice, Assets.Content);
			starfield.LoadContent();
		}

		public override void Unload() {
			starfield.UnloadContent();
			foreach(var system in systems) {
				system.Unload();
			}
		}

		public override void Update(GameTime gameTime) {
			movement += (float)gameTime.ElapsedGameTime.TotalSeconds;
			foreach(var system in systems) {
				system.Update(gameTime);
			}
		}

		public override void Draw() {
			starfield.Draw(StarfieldPosition);
			spriteBatch.Begin(transformMatrix: camera.Transform);
			foreach(var system in systems) {
				system.Draw(spriteBatch);
			}
			spriteBatch.End();
		}
	}
}
