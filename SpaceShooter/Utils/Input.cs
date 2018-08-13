using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter.Utils {
	internal sealed class Input : GameComponent {
		private static KeyboardState lastKeyboardState;
		private static KeyboardState currentKeyboardState;

		public Input(Game game) : base(game) { }

		public override void Update(GameTime gameTime) {
			lastKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();
		}

		public static bool IsKeyDown(Keys key) {
			return currentKeyboardState.IsKeyDown(key);
		}

		public static bool IsKeyUp(Keys key) {
			return currentKeyboardState.IsKeyUp(key);
		}

		public static bool IsKeyJustPressed(Keys key) {
			return currentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
		}
	}
}
