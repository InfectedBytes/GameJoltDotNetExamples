using Microsoft.Xna.Framework;
using MonoGame.Extended.Animations.SpriteSheets;
using SpaceShooter.Utils;

namespace SpaceShooter.Core {
	internal sealed class Explosion : Entity {
		private readonly SpriteSheetAnimation animation;

		public Explosion() {
			animation = Assets.ExplosionAnimation.Create("explode");
			animation.IsLooping = false;
			animation.Play();
		}

		public override void Update(GameTime gameTime) {
			animation.Update(gameTime);
			Region = animation.CurrentFrame;
			if(animation.IsComplete) Destroy();
		}
	}
}
