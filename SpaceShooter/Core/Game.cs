using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameJolt;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SpaceShooter.Core.Screens;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	internal class Game : Microsoft.Xna.Framework.Game {
		public static Settings Settings { get; private set; }
		public static Game Instance { get; private set; }
		public static GameJoltApi Jolt { get; private set; }

		private readonly Stack<Screen> screens = new Stack<Screen>();

		public Game() {
			var resourceName = "SpaceShooter.Core.Settings.json";
			using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)) {
				using(var reader = new StreamReader(stream ?? throw new InvalidOperationException())) {
					Settings = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd());
				}
			}
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
			screen.Load();
		}

		public void PopScreen() {
			var screen = screens.Pop();
			screen.Unload();
		}

		protected override void LoadContent() {
			Assets.Load(GraphicsDevice, Content);
			if(!Settings.IsValid()) {
				PushScreen(new ErrorScreen());
			} else {
				Jolt = new GameJoltApi(Settings.GameId, Settings.PrivateKey);
				PushScreen(new LoginScreen());
			}
		}

		protected override void UnloadContent() {
			Assets.Dispose();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);
			screens.Peek().Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			//GraphicsDevice.Clear(Color.CornflowerBlue);
			screens.Peek().Draw(gameTime);
		}
	}
}
