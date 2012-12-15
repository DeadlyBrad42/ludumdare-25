﻿using System;
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
		Playing
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
					spriteBatchHUD.Draw(Game.Spr_SinglePixel, new Rectangle(0, 0, Game.SCREEN_WIDTH, Game.SCREEN_WIDTH), Color.DarkGreen);
					spriteBatchHUD.DrawString(Game.ArialSmall, "How to play screen", new Vector2(179, 540), Color.White);
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
			level.addBackgroundItem(Game.Spr_Level_LevelBG, Vector2.Zero, 0.4f, 0.95f);
			//level.addBackgroundItem(Game.SprStage1BGMain, Vector2.Zero, 1.0f, 0.90f);
			//level.addBackgroundItem(Game.SprStage1FGEntrance, new Vector2(739, 0), 1.0f, 0.1f);

			// Define playbounds
			level.playBounds = new Rectangle(0, Game.SCREEN_HEIGHT - 200, 1200, 200);

			// level 1 actors
			level.player1 = new Player(new Sprite(Game.Spr_Actor_Player, 42, 68, 3), new Vector2(100, 500), level);

			levels.Add(level);

			Camera.Position = new Vector2(Game.SCREEN_WIDTH / 2, Game.SCREEN_HEIGHT / 2);
		}
	}
}
