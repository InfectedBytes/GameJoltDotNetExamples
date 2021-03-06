﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Core.Systems {
	/// <summary>
	/// Basic gameplay system.
	/// </summary>
	internal abstract class BaseSystem {
		public virtual void Load() { }
		public virtual void Unload() { }
		public virtual void Update(GameTime gameTime) { }
		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}
