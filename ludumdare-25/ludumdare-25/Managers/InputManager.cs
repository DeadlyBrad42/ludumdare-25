using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ludumdare_25.Managers
{
	static class InputManager
	{
		private const int NUMBER_OF_PLAYERS = 4;

		public static GamePadState[] gamePadState_old = new GamePadState[NUMBER_OF_PLAYERS];
		public static GamePadState[] gamePadState_new = new GamePadState[NUMBER_OF_PLAYERS];

		public static KeyboardState keyboardState_old;
		public static KeyboardState keyboardState_new;

		public static void UpdateStates()
		{
			// Update gamepad state for each player
			for (int countPlayers = 0; countPlayers < NUMBER_OF_PLAYERS; countPlayers++)
			{
				gamePadState_old[countPlayers] = gamePadState_new[countPlayers];
				gamePadState_new[countPlayers] = GamePad.GetState((PlayerIndex)countPlayers);
			}

			// Update keyboard
			keyboardState_old = keyboardState_new;
			keyboardState_new = Keyboard.GetState();
		}

		public static bool WasButtonPressed(PlayerIndex playerIndex, Buttons button)
		{
			int playerIndex_int = (int)playerIndex;

			return
				gamePadState_new[playerIndex_int].IsButtonDown(button) &&
				gamePadState_old[playerIndex_int].IsButtonUp(button)
			;
		}

		public static bool WasKeyPressed(Keys key)
		{
			return
				keyboardState_new.IsKeyDown(key) &&
				keyboardState_old.IsKeyUp(key)
			;
		}

		public static bool isKeyHeld(Keys key)
		{
			return keyboardState_new.IsKeyDown(key);
		}
	}
}
