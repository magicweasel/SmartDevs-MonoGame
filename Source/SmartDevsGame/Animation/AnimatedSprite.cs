using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame.Animation
{
	public class AnimatedSprite
	{
		public Texture2D[] Texture { get; set; }

		public bool Forward { get; set; } = true;

		public Action AnimationFinished { get; set; }

		public Vector2 Offset { get; set; }

		public string CurrentKey { get; private set; }

		private bool cycle = true;

		public AnimatedSprite()
		{
			this.currentFramePointer = 0;
		}

		public AnimatedSprite(SpriteSheet spriteSheet)
		{
			this.currentFramePointer = 0;
			this.SetAnimation("Animation", true);
		    this.spriteSheet = spriteSheet;
		}

		public void SetAnimation(string key, bool cycleBackAndForth, bool forward = true)
		{
			if (key != this.CurrentKey || forward != this.Forward)
			{
				this.Forward = forward;

				if (this.Forward)
				{
					this.currentFramePointer = 0;
				}
				else
				{
					this.currentFramePointer = this.spriteSheet.TotalFrames - 1;
				}
				this.cycle = cycleBackAndForth;
			}
		}

		private float currentFramePointer;

	    private SpriteSheet spriteSheet;

	    private int CurrentFrame
		{
			get
			{
				var frame = (int)this.currentFramePointer;
				if (frame > this.spriteSheet.TotalFrames - 1)
				{
					return this.spriteSheet.TotalFrames - 1;
				}
				if (frame < 0)
				{
					return 0;
				}

				return frame;
			}
		}

		public void Update(GameTime gameTime)
		{
			if (this.Forward)
			{
				this.currentFramePointer += gameTime.ElapsedGameTime.Milliseconds / 16f;
			}
			else
			{
				this.currentFramePointer -= gameTime.ElapsedGameTime.Milliseconds / 16f;
			}

			if (this.Forward && (this.currentFramePointer > this.spriteSheet.TotalFrames - 1))
			{
				this.Forward = false;
			}

			if (!this.Forward && this.currentFramePointer < 0)
			{
				this.Forward = true;
			}
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location)
		{
			var destinationRectangle = new Rectangle((int)location.X, (int)location.Y, this.Width, this.Height);
			this.spriteSheet.DrawFrame(spriteBatch, this.CurrentFrame, destinationRectangle);
		}

		public int Width => this.spriteSheet.Width;
		public int Height => this.spriteSheet.Height;
	}
}