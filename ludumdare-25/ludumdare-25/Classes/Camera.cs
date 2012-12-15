using Microsoft.Xna.Framework;

namespace ludumdare_25
{
	static class Camera
	{
		public static Vector2 Position;

		//TODO: Stub, I don't really know what this shit does
		// (Something about taking level coordinates (Which could be much larger than the viewport size), and converting them to draw coordinates)
		public static Vector2 getScreenPosition(Vector2 worldPosition)
		{
			return worldPosition - Camera.Position + new Vector2((Game.SCREEN_WIDTH / 2), (Game.SCREEN_HEIGHT / 2));
		}
	}
}
