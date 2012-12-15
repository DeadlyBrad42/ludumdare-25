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

		public Sprite(Texture2D texture)
		{
			this.texture = texture;
		}

		private int animationframe_current;
		public int AnimationFrame_Current
		{
			get
			{
				return animationframe_current;
			}
			set
			{
				animationframe_current = (value < AnimationFrame_Max ? value : 0);
			}
		}
		public int AnimationFrame_Max { get; set; }

		/*public Sprite(ContentManager content, String texturePath, int height, int width, int frames)
		{
			LoadTexture(content, texturePath);
			Height = height;
			Width = width;

			AnimationFrame_Current = 0;
			AnimationFrame_Max = frames;
		}*/

		public Sprite(Texture2D texture, int frames)
		{
			this.texture = texture;

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
			if (facingDirection == Enums.Direction.Down)
			{
				sourceRect = new Rectangle(
					texture.Width * AnimationFrame_Current,
					texture.Height * 0,
					texture.Width,
					texture.Height
				);
			}
			else if (facingDirection == Enums.Direction.Left)
			{
				sourceRect = new Rectangle(
					texture.Width * AnimationFrame_Current,
					texture.Height * 1,
					texture.Width,
					texture.Height
				);
			}
			else if (facingDirection == Enums.Direction.Right)
			{
				sourceRect = new Rectangle(
					texture.Width * AnimationFrame_Current,
					texture.Height * 2,
					texture.Width,
					texture.Height
				);
			}
			else //if (facingDirection == DataTypes.Direction.Up)
			{
				sourceRect = new Rectangle(
					texture.Width * AnimationFrame_Current,
					texture.Height * 3,
					texture.Width,
					texture.Height
				);
			}

			return sourceRect;
		}
	}
}
