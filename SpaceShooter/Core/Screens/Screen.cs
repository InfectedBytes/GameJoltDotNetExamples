using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SpaceShooter.Utils;
using SpaceShooter.Utils.External;

namespace SpaceShooter.Core.Screens {
	internal abstract class Screen {
		protected SpriteBatch SpriteBatch { get; }
		protected Starfield Starfield { get; }
		protected Camera Camera { get; } = new Camera(Assets.GraphicsDevice);
		private readonly float starfieldSpeed;
		private float movement;

		protected Screen(float starfieldSpeed = 100) {
			this.starfieldSpeed = starfieldSpeed;
			SpriteBatch = new SpriteBatch(Assets.GraphicsDevice);
			Starfield = new Starfield(Vector2.Zero, Assets.GraphicsDevice, Assets.Content);
			Starfield.LoadContent();
		}

		public virtual void Load() { }

		public virtual void Unload() {
			SpriteBatch.Dispose();
			Starfield.Dispose();
		}

		public virtual void Update(GameTime gameTime) {
			movement += gameTime.GetElapsedSeconds();
		}

		public virtual void Draw(GameTime gameTime) {
			Starfield.Draw(new Vector2(0, -starfieldSpeed * movement));
		}
	}
}
