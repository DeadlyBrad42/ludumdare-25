﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using System;
using ludumdare_25.Managers;


namespace ludumdare_25.Classes
{
	class Bullet : Entity
	{
		int speed = 8;
		Enums.Direction FacingDirection;

		public Bullet(Sprite sprite, Vector2 position, Level currentLevel, Enums.Direction facingDirection)
			: base(sprite, position, currentLevel)
		{
			this.FacingDirection = facingDirection;
		}

		public override void Update(GameTime gameTime)
		{
			if (FacingDirection == Enums.Direction.Left)
			{
				this.position.X += (-1) * speed;
			}
			else if (FacingDirection == Enums.Direction.Right)
			{
				this.position.X += speed;
			}

			// If the middle X of this hits the middle X of the player, deal damage
			if (
				Math.Abs(this.position.X - currentLevel.player1.position.X) < 6 &&
				Math.Abs(this.position.Y - currentLevel.player1.position.Y) < 5
			)
			{
				currentLevel.player1.takeDamage((5 - (int)Math.Abs(this.position.Y - currentLevel.player1.position.Y)) * 2);
				this.position.X = -100;
			}

			//base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw(
				this.sprite.texture,
				Camera.getScreenPosition(position),
				this.sprite.texture.Bounds,
				Color.White,
				0f,
				Vector2.Zero,
				1f,
				SpriteEffects.None,
				/*LayerDepth*/ (0.5f * (this.position.Y / 600f)) + 0.25f
			);

			base.Draw(spritebatch);
		}
	}
}
