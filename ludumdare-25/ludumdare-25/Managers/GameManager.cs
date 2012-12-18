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
		GameOver_Win
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "How to play screen", new Vector2(179, 540), Color.White);
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "About screen", new Vector2(179, 540), Color.White);
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "lol u ded", new Vector2(179, 540), Color.White);
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
					spriteBatchHUD.DrawString(Game.ArialSmall, "You win, and get to spend $" + String.Format("{0:0.00}", levels[CurrentLevel].player1.coins), new Vector2(179, 540), Color.White);
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
			level.addBackgroundItem(Game.Spr_Level_LevelBG, Vector2.Zero, 0.0f, 0.0f);
			//level.addBackgroundItem(Game.SprStage1BGMain, Vector2.Zero, 1.0f, 0.90f);
			//level.addBackgroundItem(Game.SprStage1FGEntrance, new Vector2(739, 0), 1.0f, 0.1f);

			// Define playbounds
			level.playBounds = new Rectangle(400, Game.SCREEN_HEIGHT - 200, 3600 - 800, 200);

			// level 1 actors
			// Player
			level.player1 = new Player(
				new Sprite(Game.Spr_Actors_Player, 42, 68, 3),
				new Vector2(450, 500),
				level
			);
			// Enemies
			level.Entities.Add(
				new Enemy(
					new Sprite(Game.Spr_Actors_Enemy_1, 42, 68, 3),
					new Vector2(600, 500),
					level
				)
			);
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_1, 42, 68, 3),
					new Vector2(1200, 500),
					level
				)
			);
			level.Entities.Add(
				new Civilian(
					new Sprite(Game.Spr_Actors_Civilian_2, 42, 68, 3),
					new Vector2(1300, 500),
					level
				)
			);

			// Add levl
			levels.Add(level);

			// Init camera
			Camera.Position = new Vector2(Game.SCREEN_WIDTH / 2, Game.SCREEN_HEIGHT / 2);
		}
	}
}
