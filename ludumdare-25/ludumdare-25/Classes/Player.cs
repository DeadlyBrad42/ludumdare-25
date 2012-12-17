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

		int speed = 2;

		public float attackspeed = 300;
		public float lastattack;
		public bool attacking;

		public Player(Sprite sprite, Vector2 position, Level currentLevel)
			: base(sprite, position, currentLevel, 100)
		{
			playerIndex = 0;
			FacingDirection = Enums.Direction.Right;
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
			if (attacking && (lastattack > attackspeed))
			{
				// We are done attacking
				attacking = false;
			}
			if (
				lastattack > attackspeed &&
				(
					InputManager.WasButtonPressed(playerIndex, Buttons.A) ||
					InputManager.WasKeyPressed(Keys.Space)
				)
			)
			{
				lastattack = 0;
				attacking = true;
			}

			Move(gameTime);

			// Center the camera on the player
			Camera.Position.X = position.X;

			//base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			if (!attacking)
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
			else if (lastattack < attackspeed)
			{
				int left = 0;
				int right = 0;

				if (lastattack <= 100) {left = 0;}
				else if (lastattack <= 200) {left = 1;}
				else {left = 2;}

				if(this.FacingDirection == Enums.Direction.Right) {right = 0;}
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
			{ }




			base.Draw(spritebatch);

			this.drawColor = Color.White;
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
