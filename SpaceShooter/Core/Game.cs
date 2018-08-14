using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameJolt;
using GameJolt.Objects;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SpaceShooter.Core.Screens;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	/// <summary>
	/// This is the main entry point for the game, managing a stack of screens (like menu and gamescreen)
	/// </summary>
	internal class Game : Microsoft.Xna.Framework.Game {
		/// <summary>
		/// GameJolt settings containing things like GameId and TrophyId.
		/// </summary>
		public static Settings Settings { get; private set; }
		/// <summary>
		/// Singleton instance used to push/pop screens.
		/// </summary>
		public static Game Instance { get; private set; }
		/// <summary>
		/// GameJoltApi singleton used to sent requests.
		/// </summary>
		public static GameJoltApi Jolt { get; private set; }
		/// <summary>
		/// Signed in user.
		/// </summary>
		public static Credentials User { get; set; }

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
			if(screens.Count > 0) screens.Peek().Hide();
			screens.Push(screen);
			screen.Load();
			screen.Show();
		}

		public void PopScreen() {
			var screen = screens.Pop();
			screen.Hide();
			screen.Unload();
			if(screens.Count > 0) screens.Peek().Show();
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
			screens.Peek().Draw(gameTime);
		}
	}
}
