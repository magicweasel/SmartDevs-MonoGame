using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartDevsGame.Animation;

namespace SmartDevsGame
{
	public class PlayerOne : Entity
	{
		public PlayerOne(Texture2D texture, Vector2 vector) : base(texture, vector)
		{

		}

		private bool isJumping = false;

		private Vector2 initialPosition;
		private Vector2 jumpPosition;

		private bool goingUp = true;

		private float jumpDivision = 2f;

		public void Jump()
		{
			if (isJumping) return;

			initialPosition = this.Position;
			jumpPosition = this.Position + new Vector2(0, -200);
			goingUp = true;
			jumpDivision = 2;
			isJumping = true;
		}


		public override void Update(GameTime gameTime)
		{
			if (isJumping)
			{
				if (this.goingUp)
				{
					if (this.Position.Y > this.jumpPosition.Y)
					{
						this.Position -= new Vector2(0, gameTime.ElapsedGameTime.Milliseconds / jumpDivision);
						
						this.jumpDivision += gameTime.ElapsedGameTime.Milliseconds / 200f;
						
					}
					else
					{
						this.goingUp = false;
					}
				}
				else
				{
					if (this.Position.Y < this.initialPosition.Y)
					{
						this.Position += new Vector2(0, gameTime.ElapsedGameTime.Milliseconds / jumpDivision);
					
						this.jumpDivision -= gameTime.ElapsedGameTime.Milliseconds / 200f;
					
					}
					else
					{
						this.isJumping = false;
					}
				}
			}

			base.Update(gameTime);

			this.animatedSprite?.Update(gameTime);

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.animatedSprite?.Draw(spriteBatch, this.Position + new Vector2(90, -10));
		}

		private AnimatedSprite animatedSprite;

		public void SetAnimation(AnimatedSprite animatedSprite)
		{
			this.animatedSprite = animatedSprite;
		}
	}
}