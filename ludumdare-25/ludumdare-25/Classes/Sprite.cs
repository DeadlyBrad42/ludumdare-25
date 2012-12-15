using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ludumdare_25.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ludumdare_25.Classes
{
	class Sprite
	{
		public Texture2D texture;
		public int width;
		public int height;

		public int AnimationFrame_Current;
		public float currentFrameTime;

		public void AdvanceFrame(GameTime gameTime)
		{
			currentFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (currentFrameTime >= Game.framerate)
			{
				currentFrameTime = 0f;
				AnimationFrame_Current = (AnimationFrame_Current + 1) % AnimationFrame_Max;
			}
		}

		public int AnimationFrame_Max;

		public Sprite(Texture2D texture, int width, int height, int frames)
		{
			this.texture = texture;
			this.height = height;
			this.width = width;

			currentFrameTime = 0f;
			this.AnimationFrame_Current = 0;
			this.AnimationFrame_Max = frames;
		}

		public Rectangle getDrawArea()
		{
			return new Rectangle(texture.Width * AnimationFrame_Current, 0, texture.Width, texture.Height);
		}

		public Rectangle getDrawArea(Enums.Direction facingDirection)
		{
			Rectangle sourceRect = new Rectangle(0, 0, 0, 0);
			if (facingDirection == Enums.Direction.Right)
			{
				sourceRect = new Rectangle(
					width * AnimationFrame_Current,
					height * 0,
					width,
					height
				);
			}
			else if (facingDirection == Enums.Direction.Left)
			{
				sourceRect = new Rectangle(
					width * AnimationFrame_Current,
					height * 1,
					width,
					height
				);
			}

			return sourceRect;
		}
	}
}
