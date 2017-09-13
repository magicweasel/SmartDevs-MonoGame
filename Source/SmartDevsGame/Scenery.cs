using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame
{
	public class Scenery
	{
		public List<IEntity> scenery = new List<IEntity>();

		private Texture2D texture;

		public void LoadScenery(Texture2D texture)
		{
			this.texture = texture;

			this.scenery.Add(new Background(texture, new Vector2(0, 0)));

			this.scenery.Add(new Background(texture, new Vector2(0 + texture.Width, 0)) { Flip = true });

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			this.scenery.ForEach(x => x.Draw(spriteBatch));
		}

		private SceneryMovement movement;

		public void Move(SceneryMovement sceneryMovement)
		{
			this.movement = sceneryMovement;
		}

		public float XPosition { get; set; }

		public virtual void Update(GameTime gameTime)
		{
			if (this.movement == SceneryMovement.None) return;

			var move = new Vector2(gameTime.ElapsedGameTime.Milliseconds / 3f, 0);

			if (this.movement == SceneryMovement.Forward)
			{
				this.scenery.ForEach(x =>
				{
					x.Position += move;
				});

				XPosition -= move.X;
			}
			else
			{
				this.scenery.ForEach(x =>
				{
					x.Position -= move;
				});

				XPosition += move.X;
			}

	
			this.scenery.ForEach(x =>
			{
				if (x.Position.X + this.texture.Width < 0)
				{
					x.Position = new Vector2(x.Position.X + (this.texture.Width * 2), 0);
				}
			});
	
		}
	}
}