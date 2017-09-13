using System;
using Microsoft.Xna.Framework;

namespace SmartDevsGame
{
	public class CollisionDetection
	{

		public static bool EntityCollision(IEntity a, IEntity b)
		{
			var rectangleA = new Rectangle((int) a.Position.X, (int) a.Position.Y, a.Texture.Width, a.Texture.Height);
			var rectangleB = new Rectangle((int) b.Position.X, (int) b.Position.Y, b.Texture.Width, b.Texture.Height);

			var colorDataA = new Color[a.Texture.Width * a.Texture.Height];
			a.Texture.GetData(colorDataA);

			var colorDataB = new Color[b.Texture.Width * b.Texture.Height];
			b.Texture.GetData(colorDataB);

			return IntersectPixels(rectangleA, colorDataA, rectangleB, colorDataB);
		}

		private static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
										   Rectangle rectangleB, Color[] dataB)
		{
			// Find the bounds of the rectangle intersection
			var top = Math.Max(rectangleA.Top, rectangleB.Top);
			var bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
			var left = Math.Max(rectangleA.Left, rectangleB.Left);
			var right = Math.Min(rectangleA.Right, rectangleB.Right);

			// Check every point within the intersection bounds
			for (var y = top; y < bottom; y++)
			{
				for (var x = left; x < right; x++)
				{
					// Get the color of both pixels at this point
					var colorA = dataA[(x - rectangleA.Left) +
										 (y - rectangleA.Top) * rectangleA.Width];
					var colorB = dataB[(x - rectangleB.Left) +
										 (y - rectangleB.Top) * rectangleB.Width];

					// If both pixels are not completely transparent,
					if (colorA.A != 0 && colorB.A != 0)
					{
						// then an intersection has been found
						return true;
					}
				}
			}

			// No intersection found
			return false;
		}
	}
}
