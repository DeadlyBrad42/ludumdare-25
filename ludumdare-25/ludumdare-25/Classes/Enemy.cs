using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using ludumdare_25.Managers;

namespace ludumdare_25.Classes
{
	class Enemy : Actor
	{
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

			// if position is within one screen of player

				// Move onto the same Y as player
				// Shoot

			if (!moved)
			{
				movementState = MovementState.Idle;
			}
		}
	}
}
