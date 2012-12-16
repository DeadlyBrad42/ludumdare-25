using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using ludumdare_25.Managers;
using System;

namespace ludumdare_25.Classes
{
	class Enemy : Actor
	{
		int speed = 1;

		bool movingToPoint;
		Vector2 nextPoint;

		public Enemy(Sprite sprite, Vector2 position, Level currentLevel)
			: base(sprite, position, currentLevel, 10)
		{
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

			// Center the camera on the player
			Camera.Position.X = position.X;

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
			bool moved = false;

			if (
				Math.Abs(position.X - currentLevel.player1.position.X) < 450 &&	// Position is within one screen of player
				!movingToPoint													// And not currently moving towards a point
			)
			{
				if (
					Math.Abs(this.position.Y - currentLevel.player1.position.Y) > 7 || // If enemy is not on same Y as player
					Math.Abs(this.position.X - currentLevel.player1.position.X) > 350
				)
				{
					// Y direction
					if (Math.Abs(this.position.Y - currentLevel.player1.position.Y) > 7)
					{
						System.Diagnostics.Debug.WriteLine("cop moving in Y!");
						// Pick next point
						nextPoint = new Vector2(
							this.position.X,
							currentLevel.player1.position.Y
						);

						movingToPoint = true;
					}

					// X direction
					if (Math.Abs(this.position.X - currentLevel.player1.position.X) > 350)
					{
						System.Diagnostics.Debug.WriteLine("cop moving in X!");
						// Pick next point
						nextPoint = new Vector2(
							(
								(this.position.X > currentLevel.player1.position.X) ?
								(currentLevel.player1.position.X + 350) :
								(currentLevel.player1.position.X - 350)
							),
							this.position.Y
						);

						movingToPoint = true;
					}
				}
				else
				{
					// shoot
					System.Diagnostics.Debug.WriteLine("cop BANG!");
				}
			}

			// Move towards the next point - Horiztonal direction
			if (position.X > nextPoint.X)
			{
				if (Math.Abs(position.X - nextPoint.X) <= speed)
					position.X = nextPoint.X;
				else
					position.X -= speed;
				FacingDirection = Enums.Direction.Left;
				moved = true;
			}
			else if (position.X < nextPoint.X)
			{
				if (Math.Abs(position.X - nextPoint.X) <= speed)
					position.X = nextPoint.X;
				else
					position.X += speed;
				FacingDirection = Enums.Direction.Right;
				moved = true;
			}

			// Move towards the next point - Horizontal direction
			if (position.Y > nextPoint.Y)
			{
				if (Math.Abs(position.Y - nextPoint.Y) <= speed)
					position.Y = nextPoint.Y;
				else
					position.Y -= speed;
				moved = true;
			}
			else if (position.Y < nextPoint.Y)
			{
				if (Math.Abs(position.Y - nextPoint.Y) <= speed)
					position.Y = nextPoint.Y;
				else
					position.Y += speed;
				moved = true;
			}

			// If you've reached the next point, set movingToPoint to false
			if (
				Math.Abs(position.X - nextPoint.X) < speed + 1 &&
				Math.Abs(position.Y - nextPoint.Y) < speed + 1
			)
			{
				movingToPoint = false;
			}

			if (!moved)
			{
				movementState = MovementState.Idle;
			}
			else
			{
				movementState = MovementState.Walking;
			}
		}
	}
}
