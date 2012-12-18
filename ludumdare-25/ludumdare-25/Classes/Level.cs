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
		public List<Entity> Entities;
		public List<Entity> Entities_ToAdd;
		public Player player1;
		public Rectangle playBounds;

		public Level()
		{
			backgrounds = new List<BackgroundItem>();
			Entities = new List<Entity>();
			Entities_ToAdd = new List<Entity>();
		}

		public void Update(GameTime gameTime)
		{
			// update actors
			foreach (Entity entity in Entities)
			{
				entity.Update(gameTime);
			}

			// update player
			player1.Update(gameTime);

			// update camera position
			UpdateCameraPosition();

			// Remove actors with negative positions
			for (int entityCount = 0; entityCount < Entities.Count; entityCount++)
			{
				if (Entities[entityCount].position.X < -10)
				{
					Entities.RemoveAt(entityCount);
					entityCount = 0;
				}
			}

			// Add any new actors
			for (int entityCount = 0; entityCount < Entities_ToAdd.Count; entityCount++)
			{
				Entities.Add(Entities_ToAdd[entityCount]);
				Entities_ToAdd.RemoveAt(entityCount);
				entityCount = 0;
			}
			
		}

		public void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
		{
			//draw backgrounds
			foreach (BackgroundItem background in backgrounds)
			{
				background.Draw(spriteBatch);
			}

			//draw actors
			foreach (Entity entity in Entities)
			{
				entity.Draw(spriteBatch);
			}

			// draw player
			player1.Draw(spriteBatch);

			// Draw HUD
			spriteBatchHUD.DrawString(Game.ArialSmall, "health: " + player1.Health_current, Vector2.Zero, Color.DarkRed);
			spriteBatchHUD.DrawString(Game.ArialSmall, "$" + String.Format("{0:0.00}", player1.coins), Vector2.Zero + new Vector2(Game.SCREEN_WIDTH - 100, 0), Color.Yellow);
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

		public void addEntity(Entity entity)
		{
			Entities_ToAdd.Add(entity);
		}

		public void addBackgroundItem(Texture2D texture, Vector2 position, float speed, float layerDepth)
		{
			backgrounds.Add(new BackgroundItem(texture, position, speed, layerDepth));
		}
	}
}
