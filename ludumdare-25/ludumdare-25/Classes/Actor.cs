using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Utils;
using System;

namespace ludumdare_25.Classes
{
	enum MovementState
	{
		Idle,
		Walking,
	}

	abstract class Actor : Entity
	{
		public Enums.Direction FacingDirection;
		protected MovementState movementState;

		public int Health_current;
		public int Health_max;

		public Actor(Sprite sprite, Vector2 position, Level currentLevel, int health_max)
			: base(sprite, position, currentLevel)
		{
			this.currentLevel = currentLevel;

			this.Health_max = health_max;
			this.Health_current = health_max;

			this.FacingDirection = Enums.Direction.Down;
		}
	}
}
