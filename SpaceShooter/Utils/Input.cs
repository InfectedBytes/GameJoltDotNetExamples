using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter.Utils {
	internal sealed class Input : GameComponent {
		private static KeyboardState lastKeyboardState;
		private static KeyboardState currentKeyboardState;

		private static MouseState lastMouseState;
		private static MouseState currentMouseState;

		public static Vector2 MousePos => new Vector2(
			currentMouseState.X - Consts.ScreenWidth / 2,
			currentMouseState.Y - Consts.ScreenHeight / 2);

		public Input(Game game) : base(game) { }

		public override void Update(GameTime gameTime) {
			lastKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			lastMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();
		}

		public static bool IsKeyDown(Keys key) {
			return currentKeyboardState.IsKeyDown(key);
		}

		public static bool IsKeyUp(Keys key) {
			return currentKeyboardState.IsKeyUp(key);
		}

		public static bool IsKeyJustPressed(Keys key) {
			return currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
		}

		public static bool IsKeyJustReleased(Keys key) {
			return !currentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyDown(key);
		}

		public static bool IsLeftMouseJustPressed() {
			return currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed;
		}

		public static bool IsLeftMouseJustReleased() {
			return currentMouseState.LeftButton != ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Pressed;
		}
	}
}
