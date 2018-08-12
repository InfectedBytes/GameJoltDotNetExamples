using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal sealed class PlayerShip : Ship {
		private readonly Rectangle bounds;

		public PlayerShip() : base(1, 20, Assets.DefaultMissile, Team.Player) {
			Region = Assets.Sprites["spaceShips_001"];
			var w = Assets.GraphicsDevice.Viewport.Width;
			var h = Assets.GraphicsDevice.Viewport.Height;
			// the player shall be able to move inside of the lower half of the viewport
			var rect = new Rectangle(-w / 2, 0, w, h / 2); // whole bottom half
			rect.Inflate(-Region.Width / 2, -Region.Height / 2); // shrink region by ship size
			bounds = rect;
			Effects = SpriteEffects.FlipVertically; // player should look up
		}

		public override void Update(GameTime gameTime) {
			var keyboard = Keyboard.GetState();
			// check movement input
			var velocity = Vector2.Zero;
			int dx = 0;
			int dy = 0;
			if(keyboard.IsKeyDown(Keys.A)) dx = -1;
			else if(keyboard.IsKeyDown(Keys.D)) dx = 1;
			if(keyboard.IsKeyDown(Keys.W)) dy = -1;
			else if(keyboard.IsKeyDown(Keys.S)) dy = 1;
			Move(dx, dy);
			// apply movement
			base.Update(gameTime);
			// clamp to bounds
			Position = Position.Clamp(bounds);

			// shoot
			if(keyboard.IsKeyDown(Keys.Space))
				Fire();
		}
	}
}
