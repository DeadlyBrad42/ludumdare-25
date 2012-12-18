using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using ludumdare_25.Managers;
using System;

namespace ludumdare_25.Classes
{
	class Player : Actor
	{
		#region other constants
		const float MOVEMENT_THRESHOLD = 0.3f;
		#endregion

		PlayerIndex playerIndex;

		int speed = 2;

		public double coins;

		public float attackspeed = 200;
		public float lastattack;
		public bool attacking;

		public Player(Sprite sprite, Vector2 position, Level currentLevel)
			: base(sprite, position, currentLevel, 100)
		{
			playerIndex = 0;
			FacingDirection = Enums.Direction.Right;

			lastattack = 300;
			attacking = false;

			coins = 0;
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

			lastattack += (float)gameTime.ElapsedGameTime.Milliseconds;

			// Handle punching
			if (
				!attacking &&
				(
					InputManager.WasButtonPressed(playerIndex, Buttons.A) ||
					InputManager.WasKeyPressed(Keys.Space)
				)
			)
			{
				lastattack = 0;
				attacking = true;
			}
			if (lastattack > attackspeed)
			{
				// We are done attacking
				attacking = false;
			}

			// If the middle X of this hits the middle X of the player, deal damage
			if (attacking)
			{
				foreach (Entity entity in currentLevel.Entities)
				{
					if (entity is Actor)
					{
						if (
							(
								FacingDirection == Enums.Direction.Left &&
								Math.Abs((this.position.X - 42) - entity.position.X) < 2 &&
								Math.Abs(this.position.Y - entity.position.Y) < 10
							) ||
							(
								FacingDirection == Enums.Direction.Right &&
								Math.Abs((this.position.X + 60) - entity.position.X) < 5 &&
								Math.Abs(this.position.Y - entity.position.Y) < 10
							)
						)
						{
							((Actor)entity).takeDamage((5 - (int)Math.Abs(this.position.Y - entity.position.Y)) * 2 + 10);
							attacking = false;
						}
					}
				}
			}

			Move(gameTime);

			// Center the camera on the player
			Camera.Position.X = position.X;

			//base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			if (lastattack < attackspeed)
			{
				int left = 0;
				int right = 0;

				if (lastattack <= 75) { left = 0; }
				else if (lastattack <= 150) { left = 1; }
				else { left = 2; }

				if (this.FacingDirection == Enums.Direction.Right) { right = 0; }
				else { right = 1; }

				spritebatch.Draw(
					Game.Spr_Actors_Player_Punch,
					Camera.getScreenPosition(position),
					new Rectangle(60 * left, 68 * right, 60, 68),
					drawColor,
					0f,
					Vector2.Zero,
					1f,
					SpriteEffects.None,
					/*LayerDepth*/ (0.5f * (this.position.Y / 600f)) + 0.25f
				);
			}
			else
			{
				spritebatch.Draw(
					this.sprite.texture,
					Camera.getScreenPosition(position),
					this.sprite.getDrawArea(FacingDirection),
					drawColor,
					0f,
					Vector2.Zero,
					1f,
					SpriteEffects.None,
					/*LayerDepth*/ (0.5f * (this.position.Y / 600f)) + 0.25f
				);
			}

			base.Draw(spritebatch);

			this.drawColor = Color.White;
		}

		public void CollectCoins(double amount)
		{
			coins += amount;
		}

		public void Move(GameTime gameTime)
		{
			bool moved = false;

			#region X movement
			// Moving left
			if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.X < -MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Left)
			)
			{
				if(position.X - speed > currentLevel.playBounds.X)
					position.X -= speed;
				FacingDirection = Enums.Direction.Left;
				movementState = MovementState.Walking;
				moved = true;
			}
			// Moving right
			else if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.X > MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Right)
			)
			{
				if(position.X + speed + (this.sprite.width) < currentLevel.playBounds.X + currentLevel.playBounds.Width)
					position.X += speed;
				FacingDirection = Enums.Direction.Right;
				movementState = MovementState.Walking;
				moved = true;
			}
			#endregion

			#region Y movement
			// Moving down
			if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.Y < -MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Down)
			)
			{
				if(position.Y + speed + this.sprite.height < currentLevel.playBounds.Y + currentLevel.playBounds.Height)
					position.Y += speed;
				//FacingDirection = Enums.Direction.Down;
				movementState = MovementState.Walking;
				moved = true;
			}
			// Moving up
			else if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.Y > MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Up)
				)
			{
				if ((position.Y - speed + this.sprite.height * (2.0/3.0)) > currentLevel.playBounds.Y)
					position.Y -= speed;
				//FacingDirection = Enums.Direction.Up;
				movementState = MovementState.Walking;
				moved = true;
			}
			#endregion

			if (!moved)
			{
				movementState = MovementState.Idle;
			}
		}
	}
}
