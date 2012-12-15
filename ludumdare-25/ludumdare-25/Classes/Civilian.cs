using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using ludumdare_25.Managers;

namespace ludumdare_25.Classes
{
	class Civilian : Actor
	{
		public Civilian(Sprite sprite, Vector2 position, Level currentLevel)
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
				/*LayerDepth*/ 1f
			);

			base.Draw(spritebatch);
		}

		public void AI(GameTime gameTime)
		{
			/*
			bool moved = false;

			#region X movement
			// Moving left
			if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.X < -MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Left)
			)
			{
				if(position.X - 1 > currentLevel.playBounds.X)
					position.X -= 1;
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
				if(position.X + 1 + (this.sprite.width) < currentLevel.playBounds.X + currentLevel.playBounds.Width)
					position.X += 1;
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
				if(position.Y + 1 + this.sprite.height < currentLevel.playBounds.Y + currentLevel.playBounds.Height)
					position.Y += 1;
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
				if ((position.Y - 1 + this.sprite.height * (2.0/3.0)) > currentLevel.playBounds.Y)
					position.Y -= 1;
				//FacingDirection = Enums.Direction.Up;
				movementState = MovementState.Walking;
				moved = true;
			}
			#endregion

			if (!moved)
			{
				movementState = MovementState.Idle;
			}
			*/
		}
	}
}
