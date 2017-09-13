using System;

namespace SmartDevsGame.ParticleSystem
{
	public static class RandomHelper
	{
		// a random number generator that the whole sample can share.
		private static Random random = new Random();
		public static Random Random
		{
			get { return random; }
		}

		public static float RandomBetween(float min, float max)
		{
			return min + (float)random.NextDouble() * (max - min);
		}

	}
}