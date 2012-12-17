using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ludumdare_25.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ludumdare_25.Classes
{
	abstract class Entity
	{
		public Sprite sprite;
		public Vector2 position;
		protected Level currentLevel;


		public float currentFrameTime;

		public Entity(Sprite sprite, Vector2 position, Level currentMap)
		{
			this.sprite = sprite;
			this.position = position;
			this.currentLevel = currentMap;
		}

		public abstract void Update(GameTime gameTime);

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			// Draw shadow here
		}
	}
}
