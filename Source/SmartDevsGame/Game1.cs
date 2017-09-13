using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SmartDevsGame.Animation;
using SmartDevsGame.ParticleSystem;

namespace SmartDevsGame
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public List<IEntity> Entities = new List<IEntity>();

		private PlayerOne playerOne;

		public Scenery Scenery = new Scenery();

		private Enemy enemy;

		private Explosion explosion;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 800,
				PreferredBackBufferHeight = 600
			};

			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			var screenWidth = 800;
			var screenHeight = 600;

			this.playerOne = new PlayerOne(this.Content.Load<Texture2D>("playerOne"), new Vector2(screenWidth * 0.1f, screenHeight * 0.4f));
			this.Entities.Add(this.playerOne);

			this.Scenery.LoadScenery(this.Content.Load<Texture2D>("background"));

			this.enemy = new Enemy(this.Content.Load<Texture2D>("enemy"), new Vector2(screenWidth*1.2f, screenHeight*0.6f));
			this.Entities.Add(this.enemy);

			this.explosion = new Explosion(this);
			this.explosion.LoadTexture(this.Content.Load<Texture2D>("explosion"));

			this.playerOne.SetAnimation(new AnimatedSprite(new SpriteSheet(271, 224, this.Content.Load<Texture2D>("Eyes"))));
	
		}

		protected override void Update(GameTime gameTime)
		{
			this.Scenery.Update(gameTime);

			this.Entities.ForEach(x => x.Update(gameTime));


			if (this.Scenery.XPosition > 200 && !this.enemy.IsActive)
			{
				this.enemy.Activate();
			}

			if (CollisionDetection.EntityCollision(this.playerOne, this.enemy))
			{
				this.enemy.MakeDead();
				this.explosion.Explode(this.enemy);
			}

			this.explosion.Update(gameTime);

			HandleInput();
			base.Update(gameTime);
		}

		private void HandleInput()
		{
			var keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Space))
			{
				this.playerOne.Jump();
			}

			if (keyboard.IsKeyDown(Keys.Right))
			{
				this.Scenery.Move(SceneryMovement.Backward);
			}
			else if (keyboard.IsKeyDown(Keys.Left))
			{
				this.Scenery.Move(SceneryMovement.Forward);
			}
			else
			{
				this.Scenery.Move(SceneryMovement.None);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			this.spriteBatch.Begin();

			this.Scenery.Draw(spriteBatch);
			this.Entities.ForEach(x => x.Draw(this.spriteBatch));

			this.spriteBatch.End();


			this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			this.explosion.Draw(this.spriteBatch, gameTime);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}