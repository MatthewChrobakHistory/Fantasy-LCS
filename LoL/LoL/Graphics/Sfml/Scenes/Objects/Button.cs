using SFML.Graphics;

namespace LoL.Graphics.Sfml.Scenes.Objects
{
    public class Button : SceneObject
    {
        public string Caption;
        public Color TextColor;
        public uint FontSize;

        public override void Draw() {
            // Draw the surface if we have one.
            base.Draw();

            // Draw the button's caption.
            base.RenderCaption(this.Caption, this.FontSize, this.TextColor);
        }

        public override void MouseDown(int x, int y) {
            var system = ((Sfml)GraphicsManager.Graphics).SceneSystem;

            base.MouseDown(x, y);

            switch (this.Name) {
                case "cmdLogin":
                    string name = system.GetUIObject("txtAccount").GetStringValue("text");

                    if (name != string.Empty) {
                        Data.DataManager.LoadAccount(name);
                        LoL.SetGameState(GameState.MainMenu);
                    }
                    break;
            }

            switch (this.Caption) {
                case "RELOAD":
                    system.Reload();
                    break;
            }
        }
    }
}
