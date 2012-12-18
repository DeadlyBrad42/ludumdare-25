using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ludumdare_25.Managers;
using ludumdare_25.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ludumdare_25.Classes;

namespace ludumdare_25
{
	enum GameState
	{
		MainMenu,
		HowToPlay,
		About,
		Playing,
		GameOver_Death,
		GameOver_Win,
		Story
	}

	static class GameManager
	{
		public static GameState GameState = GameState.MainMenu;
		public static List<Level> levels = new List<Level>();
		public static int CurrentLevel = 0;

		public static void Update(GameTime gameTime)
		{
			switch(GameState)
			{
				case GameState.MainMenu:
					MenuManager.Update(gameTime);
					break;
				case GameState.HowToPlay:
					// Await input to go back to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						GameState = GameState.MainMenu;
					}
					break;
				case GameState.GameOver_Death:
					// Await input to go back to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						GameState = GameState.MainMenu;
					}
					break;
				case GameState.GameOver_Win:
					// Await input to go back to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						GameState = GameState.MainMenu;
					}
					break;
				case GameState.Story:
					// Await input to go back to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						CreateLevels();
						GameState = GameState.Playing;
					}
					break;
				case GameState.About:
					// Await input to go back to main menu
					if (
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.A) ||
						InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Start) ||
						InputManager.WasKeyPressed(Keys.Space)
					)
					{
						GameState = GameState.MainMenu;
					}
					break;
				case GameState.Playing:
					// Update the level
					levels[CurrentLevel].Update(gameTime);
					break;
			}
		}

		public static void Draw(SpriteBatch spriteBatch, SpriteBatch spriteBatchHUD)
		{
			switch(GameState)
			{
				case GameState.MainMenu:
					MenuManager.Draw(spriteBatchHUD);
					break;
				case GameState.HowToPlay:
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "Use the arrow keys to move, and space to punch!", new Vector2(10, 10), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "Also (barely) supports an Xbox 360 controller!", new Vector2(10, 60), Color.White);
					break;
				case GameState.About:
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "Game by @DeadlyBrad42", new Vector2(10, 10), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "Environment sprites from OpenGameArt.org", new Vector2(10, 60), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "Character sprites stolen from River City Ransom :(", new Vector2(10, 110), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "Source available at", new Vector2(10, 160), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "https://github.com/DeadlyBrad42/ludumdare-25", new Vector2(10, 210), Color.White);
				
					
					break;
				case GameState.GameOver_Death:
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "Carl has died before ever getting a true taste of freedom", new Vector2(10, 10), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, ":(", new Vector2(10, 60), Color.White);
					break;
				case GameState.GameOver_Win:
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "Victory! To celebrate, Carl buys himself as", new Vector2(10, 10), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "much beer as he can get for $" + String.Format("{0:0.00}", levels[CurrentLevel].player1.coins), new Vector2(10, 60), Color.White);
					break;
				case GameState.Story:
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "Carl is in prison for murder, but has", new Vector2(10, 10), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "decided he wants out. To enjoy his break", new Vector2(10, 60), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "in the free world, he is going to head to ", new Vector2(10, 110), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "the bar for a drink.", new Vector2(10, 160), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "Help Carl get a drink by fending off", new Vector2(10, 260), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "attacks from the police, and beating up", new Vector2(10, 310), Color.White);
					spriteBatchHUD.DrawString(Game.ArialSmall, "civilians for money!", new Vector2(10, 360), Color.White);
					break;
				case GameState.Playing:
					levels[CurrentLevel].Draw(spriteBatch, spriteBatchHUD);
					break;
			}
		}

		public static void CreateLevels()
		{
			Level level = new Level();

			//level 1 backgrounds
			level.addBackgroundItem(Game.Spr_Level_LevelBG_1, Vector2.Zero, 0.0f, 0.0f);
			level.addBackgroundItem(Game.Spr_Level_LevelBG_2, new Vector2(3215, 0), 0.0f, 0.0f);
			//level.addBackgroundItem(Game.SprStage1BGMain, Vector2.Zero, 1.0f, 0.90f);
			//level.addBackgroundItem(Game.SprStage1FGEntrance, new Vector2(739, 0), 1.0f, 0.1f);

			// Define playbounds
			level.playBounds = new Rectangle(400, Game.SCREEN_HEIGHT - 200, 6114 - 800, 200);

			// level 1 actors
			// Player
			level.player1 = new Player(
				new Sprite(Game.Spr_Actors_Player, 42, 68, 3),
				new Vector2(450, 500),
				level
			);
			// Enemies

			// 2 civ
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_1, 42, 68, 3),
					new Vector2(900, 500),
					level
				)
			);
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_2, 42, 68, 3),
					new Vector2(1000, 500),
					level
				)
			);

			// 1 cop
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(1800, 500),
					level
				)
			);

			// 1 cop, 1 civ
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_2, 42, 68, 3),
					new Vector2(2450, 500),
					level
				)
			);
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(2600, 500),
					level
				)
			);

			// 1 cop 2 civ
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_1, 42, 68, 3),
					new Vector2(3100, 500),
					level
				)
			);
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_2, 42, 68, 3),
					new Vector2(3200, 500),
					level
				)
			);
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(3300, 500),
					level
				)
			);

			// 2 civ
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_2, 42, 68, 3),
					new Vector2(4100, 500),
					level
				)
			);
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(4200, 500),
					level
				)
			);

			// 1 cop
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(5000, 500),
					level
				)
			);

			// Add levl
			levels.Clear();
			levels.Add(level);

			// Init camera
			Camera.Position = new Vector2(Game.SCREEN_WIDTH / 2, Game.SCREEN_HEIGHT / 2);
		}
	}
}
