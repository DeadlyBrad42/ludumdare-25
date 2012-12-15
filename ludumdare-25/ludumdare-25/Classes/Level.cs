using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Classes;
using ludumdare_25.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace ludumdare_25
{
	class Level
	{
		struct BackgroundItem
		{
			Texture2D texture;
			Vector2 position;
			float speed;
			float layerDepth;

			public BackgroundItem(Texture2D texture, Vector2 position, float speed, float layerDepth)
			{
				this.texture = texture;
				this.position = position;
				this.speed = speed;
				this.layerDepth = layerDepth;
			}

			public void Draw(SpriteBatch spriteBatch)
			{
				spriteBatch.Draw(this.texture, Camera.getScreenPosition(this.position), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, this.layerDepth);
			}
		}

		List<BackgroundItem> backgrounds;
		public List<Actor> Actors;
		public Player player1;
		public Rectangle playBounds;

		public Level()
		{
			backgrounds = new List<BackgroundItem>();
			Actors = new List<Actor>();
		}

		public void Update(GameTime gameTime)
		{
			// update actors
			foreach (Actor actor in Actors)
			{
				actor.Update(gameTime);
			}

			// update player
			player1.Update(gameTime);

			// update camera position
			UpdateCameraPosition();

			// extend play area
			/*if (InputManager.WasKeyPressed(Keys.Q))
			{
				AddToPlayArea(150);
			}*/
		}

		public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
		{
			//draw backgrounds
			foreach (BackgroundItem background in backgrounds)
			{
				background.Draw(spriteBatch);
			}

			//draw actors
			foreach (Actor actor in Actors)
			{
				actor.Draw(spriteBatch);
			}

			// draw player
			player1.Draw(spriteBatch);
		}

		private void UpdateCameraPosition()
		{
			if (player1.position.X > Camera.Position.X)
			{
				Camera.Position.X += 3;
				if (Camera.Position.X + (Game.SCREEN_WIDTH / 2) > this.playBounds.Right)
					Camera.Position.X = this.playBounds.Right - (Game.SCREEN_WIDTH / 2);
			}
		}

		public void addBackgroundItem(Texture2D texture, Vector2 position, float speed, float layerDepth)
		{
			backgrounds.Add(new BackgroundItem(texture, position, speed, layerDepth));
		}

		public void AddToPlayArea(int howMuch)
		{
			this.playBounds.Width += howMuch;
		}
	}
}
