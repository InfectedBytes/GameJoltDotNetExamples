using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Core.Entities;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game {
		public SpriteBatch SpriteBatch { get; private set; }
		private Camera camera;
		private World world = new World();

		public Game() {
			var graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferWidth = 1280,
				PreferredBackBufferHeight = 720
			};
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			base.Initialize();
		}

		protected override void LoadContent() {
			Assets.Load(GraphicsDevice, Content);
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			camera = new Camera(GraphicsDevice);
			world.Add(new EnemyShip());
			world.Add(new PlayerShip());
		}

		protected override void UnloadContent() {
			Assets.Dispose();
		}

		protected override void Update(GameTime gameTime) {
			world.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			SpriteBatch.Begin(transformMatrix: camera.Transform);
			world.Draw(SpriteBatch);
			SpriteBatch.End();
		}
	}
}
