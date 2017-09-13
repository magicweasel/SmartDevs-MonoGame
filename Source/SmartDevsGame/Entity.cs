using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame
{
    public class Entity : IEntity
    {
        public Entity(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position);
        }

        
    }
}
