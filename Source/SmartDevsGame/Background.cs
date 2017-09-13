using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame
{
	public class Background : Entity
	{
		public Background(Texture2D texture, Vector2 position) : base(texture, position)
		{
		}

		public bool Flip { get; set; }

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Flip)
			{
				spriteBatch.Draw(this.Texture, this.Position, effects: SpriteEffects.FlipHorizontally);
			}
			else
			{
				spriteBatch.Draw(this.Texture, this.Position);	
			}
			
		}
	}
}