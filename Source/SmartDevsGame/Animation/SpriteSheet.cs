using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame.Animation
{
	public class SpriteSheet
	{
		private Color[] colorData;
		public int Width { get; }
		public int Height { get; }

		public FrameInfo[] FrameInfo { get; }

		public SpriteSheet(int width, int height, Texture2D texture)
		{
			this.Width = width;
			this.Height = height;
			this.Texture = texture;
			this.Columns = texture.Width / width;
			this.Rows = texture.Height / height;
			this.TotalFrames = this.Rows * this.Columns;

			this.FrameInfo = this.GetFrameInfo();
			this.TotalFrames -= this.FrameInfo.Count(x => x.IsEmpty);

			this.colorData = new Color[texture.Width * texture.Height];


		}

		public FrameInfo CurrentFrameInfo(int currentFrame)
		{
			return this.FrameInfo[currentFrame];
		}

		public int Columns { get; }

		public int Rows { get; }

		public int TotalFrames { get; }

		public Texture2D Texture { get; }

		public void DrawFrame(SpriteBatch spriteBatch, int currentFrame, Rectangle destinationRectangle)
		{
			var sourceRectangle = this.GetSourceRectangle(currentFrame);
			spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White);
		}

		private Rectangle GetSourceRectangle(int currentFrame)
		{
			var row = (int)(currentFrame / (float)this.Columns);
			var column = currentFrame % this.Columns;

			var sourceRectangle = new Rectangle(this.Width * column, this.Height * row, this.Width, this.Height);
			return sourceRectangle;
		}

		public Color[] GetColourData(int currentFrame)
		{
			this.Texture.GetData<Color>(0, this.GetSourceRectangle(currentFrame), this.colorData, 0, this.Width * this.Height);
			return this.colorData;
		}

		public FrameInfo[] GetFrameInfo()
		{
			var textureData = this.TextureTo2DArray(this.Texture);
			var list = new List<FrameInfo>();
			for (var i = 0; i < this.TotalFrames; i++)
			{
				list.Add(this.GetFrameInfo(i, textureData));
			}
			return list.ToArray();
		}

		private FrameInfo GetFrameInfo(int frameNumber, Color[,] textureData)
		{
			var sourceRectangle = this.GetSourceRectangle(frameNumber);
			var rowCount = 0;

			var colCount = 0;
			var emptyRows = 0;

			var isEmpty = true;

			for (var y = sourceRectangle.Y; y < sourceRectangle.Bottom; y++)
			{
				for (var x = sourceRectangle.X; x < sourceRectangle.Right; x++)
				{
					if (emptyRows == 0)
					{
						if (textureData[x, y].A != 0)
						{
							emptyRows = rowCount;
							isEmpty = false;
						}
					}
					colCount++;
				}
				colCount = 0;
				rowCount++;
			}
			return new FrameInfo(isEmpty, emptyRows);
		}

		private Color[,] TextureTo2DArray(Texture2D texture)
		{
			var colorsOne = new Color[texture.Width * texture.Height]; //The hard to read,1D array
			texture.GetData(colorsOne); //Get the colors and add them to the array

			var colorsTwo = new Color[texture.Width, texture.Height]; //The new, easy to read 2D array
			for (var x = 0; x < texture.Width; x++) //Convert!
				for (var y = 0; y < texture.Height; y++)
					colorsTwo[x, y] = colorsOne[x + y * texture.Width];

			return colorsTwo;
		}
	}
}