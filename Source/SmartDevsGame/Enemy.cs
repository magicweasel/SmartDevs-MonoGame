using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame
{
    public class Enemy : Entity
    {
	    public Vector2 initialPosition;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
	        this.initialPosition = position;
        }

		private bool isActive = false;

	    public bool IsActive
	    {
		    get { return isActive; }
	    }

	    public void Activate()
	    {
		    this.isActive = true;
		    this.Position = initialPosition;
	    }

	    public override void Update(GameTime gameTime)
	    {
		    if (isActive)
		    {
				if (this.Position.X + this.Texture.Width > 0)
				{
					this.Position -= new Vector2(gameTime.ElapsedGameTime.Milliseconds / 2f, 0);
				}
				else
				{
					isActive = false;
					this.Position = this.initialPosition;
				}
		    }
	    }

	    public override void Draw(SpriteBatch spriteBatch)
	    {
		    if (isActive)
		    {
				base.Draw(spriteBatch);
		    }
		    
	    }

	    public void MakeDead()
	    {
		    isActive = false;
	    }
    }
}
