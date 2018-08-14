using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
	internal sealed class PlayerShip : Ship {
		private readonly Rectangle bounds; // the region where the player can move

		public PlayerShip() : base(1, 20, new MissileDef("spaceMissiles_001", 1, 0.5f),
			new[] {new Vector2(0, -10)}, Team.Player) {
			MaxSpeed += 100; // player is a bit faster than enemies
			var w = Consts.ScreenWidth;
			var h = Consts.ScreenHeight;
			// the player shall be able to move inside of the lower half of the viewport
			var rect = new Rectangle(-w / 2, 0, w, h / 2); // whole bottom half
			rect.Inflate(-Region.Width / 2, -Region.Height / 2); // shrink region by ship size
			bounds = rect;
			Effects = SpriteEffects.FlipVertically; // player should look up
		}

		public override void Update(GameTime gameTime) {
			// check movement input
			int dx = 0;
			int dy = 0;
			if(Input.IsKeyDown(Keys.A)) dx = -1;
			else if(Input.IsKeyDown(Keys.D)) dx = 1;
			if(Input.IsKeyDown(Keys.W)) dy = -1;
			else if(Input.IsKeyDown(Keys.S)) dy = 1;
			Move(dx, dy);
			// apply movement
			base.Update(gameTime);
			// clamp to bounds
			Position = Position.Clamp(bounds);

			// shoot
			if(Input.IsKeyDown(Keys.Space))
				Fire();
		}
	}
}
