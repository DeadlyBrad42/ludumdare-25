using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ludumdare_25.Utils;
using ludumdare_25.Classes;

namespace ludumdare_25.Managers
{
	enum MenuState
	{
		TitleScreen,
		MainMenu
	}

	struct MenuItem
	{
		String text;
		Vector2 position;

		public MenuItem(String text, Vector2 position)
		{
			this.text = text;
			this.position = position;
		}

		public void Draw(SpriteBatch spritebatchHUD, Color drawColor)
		{
			Utils.Draw.TextShadow(spritebatchHUD, Game.ArialSmall, text, position, new Vector2(position.X + 2, position.Y + 2), drawColor, null);
		}
	}

	static class MenuManager
	{
		static Color colorStandard = Color.BlanchedAlmond;
		static Color colorSelected = Color.White;
		//static Color colorFlash = Color.White;

		static List<MenuItem> menuItems = new List<MenuItem>();
		static int currentMenuItem = 0;
		public static MenuState MenuState = MenuState.TitleScreen;

		public static void Update(GameTime gameTime)
		{
			handleInput();
		}

		private static void handleInput()
		{
			switch (MenuState)
			{
				case MenuState.TitleScreen:
					// Await input to move on to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						MenuState = MenuState.MainMenu;
					}
					break;
				case MenuState.MainMenu:
					// Navigate the menu

					// Up
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.DPadUp) ||
						(
							InputManager.gamePadState_new[(int)PlayerIndex.One].ThumbSticks.Left.Y < 0.3 &&
							InputManager.gamePadState_old[(int)PlayerIndex.One].ThumbSticks.Left.Y > 0.3
						) ||
						InputManager.WasKeyPressed(Keys.Up)
					)
					{
						currentMenuItem--;
						if (currentMenuItem < 0) currentMenuItem = menuItems.Count - 1;
					}

					// Down
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.DPadDown) ||
						(
							InputManager.gamePadState_new[(int)PlayerIndex.One].ThumbSticks.Left.Y < -0.3 &&
							InputManager.gamePadState_old[(int)PlayerIndex.One].ThumbSticks.Left.Y > -0.3
						) ||
						InputManager.WasKeyPressed(Keys.Down)
					)
					{
						currentMenuItem++;
						if (currentMenuItem >= menuItems.Count) currentMenuItem = 0;
					}

					// Activate menu item actions
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						switch (currentMenuItem)
						{
							case 0:
								// Begin game
								GameManager.GameState = GameState.Story;
								break;
							case 1:
								// How to play
								GameManager.GameState = GameState.HowToPlay;
								break;
							case 2:
								// About screen
								GameManager.GameState = GameState.About;
								break;
							case 3:
								// Exit game
								Game.ExitGame();
								break;
						}
					}
					break;
			}
		}

		public static void Draw(SpriteBatch spriteBatchHUD)
		{
			// Draw the menu background
			if (MenuState == Managers.MenuState.TitleScreen)
			{
				spriteBatchHUD.Draw(
					Game.Spr_UI_TitleBG,
					Vector2.Zero,
					new Rectangle(0, 0, Game.SCREEN_WIDTH, Game.SCREEN_HEIGHT),
					Color.White,
					0f,
					Vector2.Zero,
					1f,
					SpriteEffects.None,
					1f
				);
			}
			else
			{
				spriteBatchHUD.Draw(
					Game.Spr_UI_TitleBG_dark,
					Vector2.Zero,
					new Rectangle(0, 0, Game.SCREEN_WIDTH, Game.SCREEN_HEIGHT),
					Color.White,
					0f,
					Vector2.Zero,
					1f,
					SpriteEffects.None,
					1f
				);
			}

			switch (MenuState)
			{
				case MenuState.TitleScreen:
					Utils.Draw.TextShadow(spriteBatchHUD, Game.ArialLarge, "PRISON BREAK", new Vector2(35, 25), null, Color.BlanchedAlmond, null);
					Utils.Draw.TextShadow(spriteBatchHUD, Game.ArialSmall, "Press space to start", new Vector2(255, 150), new Vector2(257, 152), Color.BlanchedAlmond, null);
					break;
				case MenuState.MainMenu:
					for (int i = 0; i < menuItems.Count; i++)
					{
						if (i == currentMenuItem)
							menuItems[i].Draw(spriteBatchHUD, colorSelected);
						else
							menuItems[i].Draw(spriteBatchHUD, colorStandard);
					}
					break;
			}
		}

		public static void CreateMenuItems()
		{
			menuItems.Add(new MenuItem("Begin Game", new Vector2(600, 20)));
			menuItems.Add(new MenuItem("How to Play", new Vector2(600, 60)));
			menuItems.Add(new MenuItem("About", new Vector2(600, 100)));
			menuItems.Add(new MenuItem("Exit Game", new Vector2(600, 140)));
		}
	}
}
