using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    internal class Wall : DrawableGameComponent
    {
        public static Texture2D tex;
        public static Rectangle wallColor;

        public SpriteData sprite;
        public float angle;

        public Wall(Game game) : base(game)
        {

        }

        public Wall(Game game, int x, int y, int w, int h, int r, int s) : base(game)
        {
            sprite = new SpriteData
            {
                rect = new Rectangle(x, y, w, h),
                rotation = r,
                scale = s,
            };
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/COMS_437-Project_1-ColorStrip");
            }
            if (wallColor == null)
            {
                wallColor = new Rectangle(2, 0, 1, 1);
            }

            sprite.tex = tex;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(tex, sprite.rect, wallColor, Color.White, sprite.rotation, Vector2.Zero, SpriteEffects.None, 0.0f);

            base.Draw(gameTime);
        }
    }
}
