using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using ludumdare_25.Managers;

namespace ludumdare_25.Classes
{
	class Player : Actor
	{
		#region other constants
		const float MOVEMENT_THRESHOLD = 0.3f;
		#endregion

		PlayerIndex playerIndex;

		public Player(Sprite sprite, Vector2 position, Level currentLevel)
			: base(sprite, position, currentLevel, 10)
		{
			playerIndex = 0;
		}

		public override void Update(GameTime gameTime)
		{
			// Handle animations
			/*if (movementState == MovementState.Walking)
			{
				currentFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (currentFrameTime >= Game.framerate)
				{
					currentFrameTime = 0f;
					Sprite.AnimationFrame_Current++;
				}
			}
			else if (movementState == MovementState.Idle)
			{
				Sprite.AnimationFrame_Current = 0;
			}*/

			Move(gameTime);

			// Center the camera on the player
			Camera.Position = position;

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

		public void Move(GameTime gameTime)
		{
			bool moved = false;

			#region X movement
			if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.X < -MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Left)
			)
			{
				position.X -= 1;
				FacingDirection = Enums.Direction.Left;
				movementState = MovementState.Walking;
				moved = true;
			}
			else if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.X > MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Right)
			)
			{
				position.X += 1;
				FacingDirection = Enums.Direction.Right;
				movementState = MovementState.Walking;
				moved = true;
			}
			#endregion

			#region Y movement
			if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.Y < -MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Down)
			)
			{
				position.Y += 1;
				FacingDirection = Enums.Direction.Down;
				movementState = MovementState.Walking;
				moved = true;
			}
			else if (
				InputManager.gamePadState_new[(int)playerIndex].ThumbSticks.Left.Y > MOVEMENT_THRESHOLD ||
				InputManager.isKeyHeld(Keys.Up)
				)
			{
				position.Y -= 1;
				FacingDirection = Enums.Direction.Up;
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
