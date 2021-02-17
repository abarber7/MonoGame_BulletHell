using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    class Bullet : Projectile
    {

        public Bullet(Dictionary<string, object> bulletProperties) : base(bulletProperties)
        {
            
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
        }
    }
}
