using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ludumdare_25.Utils
{
	static class Draw
	{
		public static void TextShadow(SpriteBatch spritebatch, SpriteFont font, String text, Vector2 textPosition, Vector2? shadowPosition, Color textColor, Color? shadowColor)
		{
			if (shadowPosition == null)
			{
				shadowPosition = new Vector2(textPosition.X + 5, textPosition.Y + 5);
			}

			if (shadowColor == null)
			{
				shadowColor = Color.Black;
			}

			spritebatch.DrawString(font, text, shadowPosition.GetValueOrDefault(), shadowColor.GetValueOrDefault());
			spritebatch.DrawString(font, text, textPosition, textColor);
		}
	}
}
