using Microsoft.Xna.Framework;

namespace SpaceShooter.Core.Screens {
	internal abstract class Screen {
		public virtual void Load() { }
		public virtual void Unload() { }
		public virtual void Update(GameTime gameTime) { }
		public virtual void Draw() { }
	}
}
