using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceShooter.Core.Screens;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	internal class Game : Microsoft.Xna.Framework.Game {
		public static Game Instance { get; private set; }
		private readonly Stack<Screen> screens = new Stack<Screen>();

		public Game() {
			var graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferWidth = Consts.ScreenWidth,
				PreferredBackBufferHeight = Consts.ScreenHeight
			};
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
			Instance = this;
			Components.Add(new Input(this));
		}

		public void PushScreen(Screen screen) {
			screens.Push(screen);
		}

		public void PopScreen() {
			screens.Pop();
		}

		protected override void LoadContent() {
			Assets.Load(GraphicsDevice, Content);
			PushScreen(new GameScreen());
		}

		protected override void UnloadContent() {
			Assets.Dispose();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);
			screens.Peek().Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			screens.Peek().Draw();
		}
	}
}
