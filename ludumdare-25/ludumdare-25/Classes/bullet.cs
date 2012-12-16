using Microsoft.Xna.Framework;
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
		}

		public override void Update(GameTime gameTime)
		{
			if (FacingDirection == Enums.Direction.Left)
			{
				this.position += new Vector2(0, (-1) * speed);
			}
			else if (FacingDirection == Enums.Direction.Right)
			{
				this.position += new Vector2(0, speed);
			}

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
	}
}
