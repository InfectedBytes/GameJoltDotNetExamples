using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Utils {
	internal class Camera {
		public float Zoom { get; set; } = 1f;
		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public Matrix Transform => Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
		                           Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom)
		                           * Matrix.CreateTranslation(GraphicsDevice.Viewport.Width / 2f,
			                           GraphicsDevice.Viewport.Height / 2f, 0);
		public GraphicsDevice GraphicsDevice { get; }

		public Camera(GraphicsDevice graphicsDevice) {
			GraphicsDevice = graphicsDevice;
		}
	}
}
