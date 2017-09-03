using LoL.Graphics.Sfml.Scenes.Objects;
using System.Collections.Generic;
using System.IO;

namespace LoL.Graphics.Sfml.Scenes
{
    public class SceneSystem : IScenes
    {
        // The collection of all the scene related surfaces, and objects.
        private List<GraphicalSurface> _surfaces;
        private List<SceneObject>[] _UIObject;

        // The scene object that has the current focus.
        private SceneObject _curFocus;

        // General bool variable stating whether or not the mouse buttons are down.
        public bool MouseLeftDown { private set; get; }
        public bool MouseMiddleDown { private set; get; }
        public bool MouseRightDown { private set; get; }

        #region Core Scene System Logic
        // All these methods pertain to event handling.
        // You shouldn't have to change this.

        public SceneSystem() {
            // Create an array of collections containing scene objects for 
            // every game state.
            this._UIObject = new List<SceneObject>[(int)GameState.Length];
            for (int i = 0; i < this._UIObject.Length; i++) {
                this._UIObject[i] = new List<SceneObject>();
            }

            // Load all the graphical surfaces.
            this.LoadSurfaces();

            // Load all the hard-coded scene objects.
            this.LoadSceneObjects();
        }

        public void Reload() {
            this.Destroy();

            // Load all the graphical surfaces.
            this.LoadSurfaces();

            // Load all the hard-coded scene objects.
            this.LoadSceneObjects();
        }

        public void Destroy() {
            foreach (var scene in _UIObject) {
                scene.Clear();
            }

            this._surfaces.Clear();
        }

        private void LoadSurfaces() {
            // Initialize the collection.
            this._surfaces = new List<GraphicalSurface>();

            // Load every png file we find in the directory specified.
            foreach (string file in Directory.GetFiles(GraphicsManager.GuiPath, "*.png")) {
                this._surfaces.Add(new GraphicalSurface(file));
            }

            // Load every png file we find in the directory specified.
            foreach (string file in Directory.GetFiles(GraphicsManager.GuiPath, "*.jpg")) {
                this._surfaces.Add(new GraphicalSurface(file));
            }
        }

        public void MouseMove(int x, int y) {
            // If our left mouse button is down, we can apply dragging
            // processing on our focused scene object.
            if (this.MouseLeftDown && this._curFocus != null) {
                this._curFocus.Drag(x, y);
            }

            // Make sure that we actually initialized the scene system.
            if (this._UIObject != null) {
                // Make sure that we actually have scene objects in our current state.
                if (this._UIObject[(int)LoL.State] != null) {
                    // Loop through all possible values for the ZOrder.
                    for (int z = ZOrder.GetHighZ(); z >= 0; z--) {
                        // Loop through every scene object we have in our current state.
                        foreach (var obj in this._UIObject[(int)LoL.State]) {
                            // Does the object's ZIndex match the ZOrder?
                            if (obj.Z == z) {
                                // Is the object visible?
                                if (obj.Visible) {
                                    // Did we move our mouse within the area of the scene object?
                                    if (x >= obj.Left && x <= obj.Left + obj.Width) {
                                        if (y >= obj.Top && y <= obj.Top + obj.Height) {

                                            // We did. Invoke appropriate event-handling methods.
                                            obj.MouseMove(x - obj.Left, y - obj.Top);

                                            // We assume we have the object we moused over.
                                            // Return so that we don't apply similar logic on scene objects that 
                                            // should not receive this processing.
                                            return;
                                        }
                                    }
                                }
                                // This break will break out of the current loop through all the scene objects for a respective Z value.
                                // It ensures we don't waste time looking for another object that can't exist.
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void MouseUp(string button, int x, int y) {
            // Set the states of the appropriate mouse button to not pressed.
            switch (button) {
                case "left":
                    this.MouseLeftDown = false;
                    break;
                case "middle":
                    this.MouseMiddleDown = false;
                    break;
                case "right":
                    this.MouseRightDown = false;
                    break;
            }

            // Since we're lifting a mouse button, do we have a 
            // currently focused scene object?
            if (this._curFocus != null) {
                // Invoke the EndDrag method for that object.
                this._curFocus.EndDrag();
            }
        }

        public void MouseDown(string button, int x, int y) {
            // Set the states of the appropriate mouse button to pressed.
            switch (button) {
                case "left":
                    this.MouseLeftDown = true;
                    break;
                case "middle":
                    this.MouseMiddleDown = true;
                    break;
                case "right":
                    this.MouseRightDown = true;
                    break;
            }

            // Make sure that the scene system has actually been initialized.
            if (this._UIObject != null) {
                // Make sure that we actually have scene objects in our current state.
                if (this._UIObject[(int)LoL.State] != null) {
                    // Loop through every possible ZOrder value.
                    for (int z = ZOrder.GetHighZ(); z >= 0; z--) {
                        // Loop through all the scene objects in our current state.
                        foreach (var obj in this._UIObject[(int)LoL.State]) {
                            // Does the ZIndex match the ZOrder?
                            if (obj.Z == z) {
                                // Make sure that the object is visible.
                                if (obj.Visible) {
                                    // Did we click within the area of the scene object?
                                    if (x >= obj.Left && x <= obj.Left + obj.Width) {
                                        if (y >= obj.Top && y <= obj.Top + obj.Height) {

                                            //If we had a previous scene object, let that object
                                            // know that it no longer has the focus.
                                            if (this._curFocus != null) {
                                                this._curFocus.HasFocus = false;
                                            }

                                            // Assign this scene object as our currently focused scene object and
                                            // let it know that it has our focus.
                                            this._curFocus = obj;
                                            this._curFocus.HasFocus = true;

                                            // Invoke the appropriate event handling methods.
                                            this._curFocus.MouseDown(x - obj.Left, y - obj.Top);
                                            this._curFocus.BeginDrag(x - obj.Left, y - obj.Top);

                                            // We assume that we have the object we clicked on.
                                            // Return so that we don't apply similar logic on scene objects that
                                            // should not receive this processing.
                                            return;
                                        }
                                    }
                                }
                                // This break will break out of the current loop through all the scene objects for a respective Z value.
                                // It ensures we don't waste time looking for another object that can't exist.
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void KeyDown(string key) {
            // Keypress event handling regarding the scene system requires an
            // object being focused.
            if (this._curFocus != null) {
                this._curFocus.KeyDown(key);
            }
        }

        public void KeyUp(string key) {
            // Keyup event handling regarding the scene system requires an
            // object being focused.
            if (this._curFocus != null) {
                this._curFocus.KeyUp(key);
            }
        }

        public void Draw() {
            // Make sure that we've actually loaded the scene system.
            if (this._UIObject != null) {
                // Make sure we actually have scene objects in our current state.
                if (this._UIObject[(int)LoL.State] != null) {
                    // Draw every object in this scene if it's visible.
                    foreach (var obj in this._UIObject[(int)LoL.State]) {
                        if (obj.Visible) {
                            obj.Draw();
                        }
                    }
                }
            }
        }

        private GraphicalSurface GetSurface(string tagName) {
            // Loop through our collection of graphical surfaces.
            foreach (var surface in this._surfaces) {
                // If the surface's tag matches our specific tag, return the surface.
                if (surface.Tag == tagName.ToLower()) {
                    return surface;
                }
            }
            // If the surface does not exist, return null.
            return null;
        }

        public SceneObject GetUIObject(string name) {
            // Make sure that we actually initialized the scene system.
            if (this._UIObject != null) {
                // Make sure we actually have scene objects in our current state.
                if (this._UIObject[(int)LoL.State] != null) {
                    // Loop through all the scene objects in our current state.
                    foreach (var obj in this._UIObject[(int)LoL.State]) {
                        // If the object has the same name as the one specified, return it.
                        if (obj.Name?.ToLower() == name.ToLower()) {
                            return obj;
                        }
                    }
                }
            }
            // If the scene object could not be found, return null.
            return null;
        }

        #endregion

        private void LoadSceneObjects() {
            this.LoadLoginMenu();
            this.LoadMainMenu();
        }

        private void LoadLoginMenu() {
            var array = new SceneObject[] {
                new Image() {
                    Height = 650,
                    Width = 1070,
                    Surface = GetSurface("menu-wallpaper"),
                    Left = 5,
                    Top = 10
                },
                new Image() {
                    Width = 325,
                    Height = 125,
                    Top = 25,
                    Left = 65,
                    Surface = GetSurface("logo")
                },
                new Rectangle() {
                    Height = 250,
                    Width = 350,
                    Left = 50,
                    Top = 175,
                    BackgroundColor = new SFML.Graphics.Color(0, 22, 27, 225),
                    OutlineColor = SFML.Graphics.Color.White,
                    OutlineThickness = 1f
                },
                new Label() {
                    Width = 100,
                    Height = 50,
                    Left = 50,
                    Top = 175,
                    FontSize = 24,
                    Caption = "Login",
                    TextColor = SFML.Graphics.Color.White
                },
                new Label() {
                    Width = 100,
                    Height = 50,
                    Left = 70,
                    Top = 225,
                    FontSize = 16,
                    Caption = "Username",
                    TextColor = SFML.Graphics.Color.White
                },
                new Textbox() {
                    Name = "txtAccount",
                    Width = 275,
                    Height = 25,
                    Top = 275,
                    Left = 80,
                    Surface = GetSurface("whitebox"),
                    MaxLength = 24,
                    TextColor = SFML.Graphics.Color.Black,
                    FontSize = 16
                },
                new Button() {
                    Name = "cmdLogin",
                    Left = 250,
                    Top = 325,
                    Width = 100,
                    Height = 35,
                    Caption = "Log In",
                    TextColor = SFML.Graphics.Color.Black,
                    FontSize = 16,
                    Surface = GetSurface("orangefade")
                },
                new CheckBox() {
                    Left = 80,
                    Top = 325,
                    Width = 100,
                    Height = 25,
                    Caption = "Remember Username?",
                    TextColor = SFML.Graphics.Color.White,
                    FontSize = 12,
                    SurfaceUnchecked = GetSurface("whitebox"),
                    Surface = GetSurface("whitebox-checked")
                }
            };

            foreach (var obj in array) {
                this._UIObject[(int)GameState.Login].Add(obj);
            }
        }

        private void LoadMainMenu() {
            var array = new SceneObject[] {
                new Button() {
                    Name = "cmdPlay",
                    Caption = "Play",
                    FontSize = 24,
                    TextColor = SFML.Graphics.Color.White,
                    Width = 150,
                    Height = 50,
                    Surface = GetSurface("orangefade"),
                    Top = 10,
                    Left = 440
                },
                new Image() {
                    Surface = GetSurface("logo"),
                    Width = 175,
                    Height = 75,
                    Left = 5,
                    Top = 5
                }
            };

            foreach (var obj in array) {
                this._UIObject[(int)GameState.MainMenu].Add(obj);
            }

            var button = new Button() {
                Width = 100,
                Height = 100,
                FontSize = 12,
                Caption = "RELOAD",
                TextColor = SFML.Graphics.Color.Red,
                Left = 600
            };

            this._UIObject[(int)GameState.MainMenu].Add(button);
        }
    }
}
