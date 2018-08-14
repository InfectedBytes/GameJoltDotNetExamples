using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Utils {
	/// <summary>
	/// Basic 2D camera.
	/// </summary>
	internal class Camera {
		public float Zoom { get; set; } = 1f;
		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public Matrix Transform => Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
		                           Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom)
		                           * Matrix.CreateTranslation(Consts.ScreenWidth / 2f,
			                           Consts.ScreenHeight / 2f, 0);
		public GraphicsDevice GraphicsDevice { get; }

		public Camera(GraphicsDevice graphicsDevice) {
			GraphicsDevice = graphicsDevice;
		}
	}
}
