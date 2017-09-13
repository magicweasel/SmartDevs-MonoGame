using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmartDevsGame
{
    public interface IEntity
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }
       
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
