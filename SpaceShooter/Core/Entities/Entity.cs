using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;

namespace SpaceShooter.Core.Entities {
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

		public bool Overlaps(Entity other) {
			if(Region == null || other.Region == null) return false;
			float dx = Position.X - other.Position.X;
			float dy = Position.Y - other.Position.Y;
			return Math.Abs(dx) < (Region.Width + other.Region.Width) / 2f
			       && Math.Abs(dy) < (Region.Height + other.Region.Height) / 2f;
		}

		public virtual void OnCollide(Entity other) { }

		public virtual void Destroy() {
			IsDestroyed = true;
		}
	}
}
