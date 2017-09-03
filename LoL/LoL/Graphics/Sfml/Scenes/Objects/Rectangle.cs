using SFML.Graphics;
using SFML.System;

namespace LoL.Graphics.Sfml.Scenes.Objects
{
    public class Rectangle : SceneObject
    {
        public Color BackgroundColor;
        public Color OutlineColor;
        public float OutlineThickness;

        public override void Draw() {
            var rect = new RectangleShape(new Vector2f(this.Width, this.Height));
            rect.Position = new Vector2f(this.Left, this.Top);
            rect.FillColor = BackgroundColor;
            rect.OutlineColor = OutlineColor;
            rect.OutlineThickness = this.OutlineThickness;

            GraphicsManager.Graphics.DrawObject(rect);
        }
    }
}
