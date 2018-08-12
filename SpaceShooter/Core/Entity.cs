using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	internal abstract class Entity {
		public World World { get; internal set; }

		public Team Team { get; }
		public TextureRegion2D Region { get; set; }
		public SpriteEffects Effects { get; set; }

		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public float Rotation { get; set; }

		public bool IsDestroyed { get; private set; }

		protected Entity(Team team = Team.None) {
			Team = team;
		}

		public virtual void Update(GameTime gameTime) {
			Position += Velocity * gameTime.GetElapsedSeconds();
		}

		public virtual void Draw(SpriteBatch spriteBatch) {
			if(Region == null) return;
			spriteBatch.Draw(Region, Position, Rotation, Region.Size / 2f, Effects);
		}

		public virtual void OnCollide(Entity other) { }

		public void Destroy() {
			IsDestroyed = true;
		}
	}
}
