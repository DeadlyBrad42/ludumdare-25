using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ludumdare_25.Classes;
using ludumdare_25.Managers;
using ludumdare_25.Utils;

namespace ludumdare_25
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		// Drawing variables
		public const int SCREEN_WIDTH = 800;
		public const int SCREEN_HEIGHT = 600;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteBatch spriteBatchHUD;

		// Textures
		public static Texture2D Spr_SinglePixel;
		public static Texture2D Spr_UI_TitleBG;

		// Fonts
		public static SpriteFont ArialLarge;
		public static SpriteFont ArialSmall;

		// Misc.
		public static float framerate = 1f / 12f;
		static bool exitGame;

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
			graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
			//graphics.IsFullScreen = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			spriteBatchHUD = new SpriteBatch(GraphicsDevice);

			// Initialize game menu
			MenuManager.CreateMenuItems();

			// initialize textures
			#region Textures
			Spr_SinglePixel = Content.Load<Texture2D>(@"Textures\SinglePixel");
			Spr_UI_TitleBG = Content.Load<Texture2D>(@"Textures\UI\TitleBG");
			#endregion

			// Initialize fonts
			#region Fonts
			ArialLarge = Content.Load<SpriteFont>(@"Fonts\ArialLarge");
			ArialSmall = Content.Load<SpriteFont>(@"Fonts\ArialSmall");
			#endregion
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			//if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || exitGame)
			if (
				InputManager.WasButtonPressed(PlayerIndex.One, Buttons.Back) ||
				InputManager.WasKeyPressed(Keys.Escape) ||
				exitGame
			)
			{
				this.Exit();
			}

			// Get user's input
			InputManager.UpdateStates();

			// Update the game
			GameManager.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Draw the screen
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			spriteBatchHUD.Begin();
			GameManager.Draw(spriteBatch, spriteBatchHUD);
			spriteBatch.End();
			spriteBatchHUD.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// Exits the game.
		/// </summary>
		public static void ExitGame()
		{
			exitGame = true;
		}
	}
}
