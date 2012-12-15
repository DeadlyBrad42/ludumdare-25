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

			public void Draw(SpriteBatch SB)
			{
				SB.Draw(this.texture, Camera.getScreenPosition(this.position), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, this.layerDepth);
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
			for (int i = 0; i < Actors.Count; i++)
			{
				Actors[i].Update(gameTime);
			}

			// update camera position
			UpdateCameraPosition();

			// extend play area
			if (InputManager.WasKeyPressed(Keys.Q))
			{
				AddToPlayArea(150);
			}
		}

		public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
		{
			//draw backgrounds
			for (int i = 0; i < backgrounds.Count; i++)
			{
				backgrounds[i].Draw(spriteBatch);
			}

			//draw actors
			for (int i = 0; i < Actors.Count; i++)
			{
				Actors[i].Draw(spriteBatch);
			}
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
