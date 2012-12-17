using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using System;
using ludumdare_25.Managers;

namespace ludumdare_25.Classes
{
	class Civilian : Actor
	{
		// For "sporadic" movement
		int movementPoints;
		int movementPoints_achieved;
		bool movingToPoint;
		Vector2 nextPoint;

		int speed = 3;

		public Civilian(Sprite sprite, Vector2 position, Level currentLevel)
			: base(sprite, position, currentLevel, 10)
		{
			movementPoints = Game.random.Next(3, 6);
			movementPoints_achieved = 0;
			movingToPoint = false;

			movementState = MovementState.Walking;
		}

		public override void Update(GameTime gameTime)
		{
			// Handle animations
			if (movementState == MovementState.Walking)
			{
				sprite.AdvanceFrame(gameTime);
			}
			else if (movementState == MovementState.Idle)
			{
				sprite.AnimationFrame_Current = 0;
			}

			AI(gameTime);

			//base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw(
				this.sprite.texture,
				Camera.getScreenPosition(position),
				this.sprite.getDrawArea(FacingDirection),
				Color.White,
				0f,
				Vector2.Zero,
				1f,
				SpriteEffects.None,
				/*LayerDepth*/ (0.5f * (this.position.Y / 600f)) + 0.25f
			);

			base.Draw(spritebatch);
		}

		public void AI(GameTime gameTime)
		{
			if (
				Math.Abs(position.X - currentLevel.player1.position.X) < 450 &&	// Position is (close to being) within one screen of player
				!movingToPoint													// And not currently moving towards a point
			)
			{
				if (movementPoints_achieved < movementPoints)					// If civilian isn't "spent"
				{
					// Pick next point
					nextPoint = new Vector2(
						Game.random.Next(600) + currentLevel.player1.position.X - 300,
						Game.random.Next(175) + 425 - this.sprite.height
					);

					movingToPoint = true;
				}
				else{
					// Run towards beginning of level
					nextPoint = new Vector2(-200, 500);
					speed = 5;
					movingToPoint = true;
				}
			}

			if (movingToPoint)
			{
				// Move towards the next point - Horiztonal direction
				if (position.X > nextPoint.X)
				{
					if (Math.Abs(position.X - nextPoint.X) <= speed)
						position.X = nextPoint.X;
					else
						position.X -= speed;
					FacingDirection = Enums.Direction.Left;
				}
				else if (position.X < nextPoint.X)
				{
					if (Math.Abs(position.X - nextPoint.X) <= speed)
						position.X = nextPoint.X;
					else
						position.X += speed;
					FacingDirection = Enums.Direction.Right;
				}

				// Move towards the next point - Horizontal direction
				if (position.Y > nextPoint.Y)
				{
					if (Math.Abs(position.Y - nextPoint.Y) <= speed)
						position.Y = nextPoint.Y;
					else
						position.Y -= speed;
				}
				else if (position.Y < nextPoint.Y)
				{
					if (Math.Abs(position.Y - nextPoint.Y) <= speed)
						position.Y = nextPoint.Y;
					else
						position.Y += speed;
				}
			}

			// If you've reached the next point, set movingToPoint to false
			if(
				Math.Abs(position.X - nextPoint.X) < speed + 1 &&
				Math.Abs(position.Y - nextPoint.Y) < speed + 1
			)
			{
				movementPoints_achieved++;
				movingToPoint = false;
			}
		}
	}
}
