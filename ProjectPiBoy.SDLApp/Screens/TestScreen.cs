using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Data.Binding;
using ProjectPiBoy.SDLApp.UiObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.Screens
{
    /// <summary>
    /// A screen meant to be a playground for testing the UI framework.
    /// </summary>
    public class TestScreen : Screen
    {
        private class TestScreenViewModel : CustomBindableBase
        {
            private string _Text1;
            public string Text1
            {
                get => this._Text1;
                set => this.SetProperty(ref this._Text1, value);
            }

            private string _TimeText;
            public string TimeText
            {
                get => this._TimeText;
                set => this.SetProperty(ref this._TimeText, value);
            }

            private double _FPS;
            public double FPS
            {
                get => this._FPS;
                set => this.SetPropertyWithDependencies(ref this._FPS, value,
                    nameof(this.FPS), nameof(this.FpsStr));
            }

            public string FpsStr => $"FPS: {this.FPS:0.00}";
        }

        private TestScreenViewModel ViewModel { get; set; }

        public override void OnFpsUpdate(double frameRate, double frameTime)
        {
            this.ViewModel.FPS = frameRate;
        }

        public TestScreen(Assets assets)
        {
            this.ViewModel = new TestScreenViewModel();

            var clickMeButton = new UiButton(this, new UiObjectPlacement(0.12F, 0.05F, 0.24F, 0.1F, 0))
            {
                new UiText(this)
                {
                    Text = "Click Me!"
                }
            };

            clickMeButton.Click += () => this.ViewModel.Text1 = "Click Me button clicked!";

            this.UiObjects.Add(clickMeButton);

            this.UiObjects.Add(new UiButton(this, new UiObjectPlacement(0.88F, 0.05F, 0.24F, 0.1F, 0))
            {
                new UiText(this)
                {
                    Text = "Button 2"
                }
            });

            UiText text = new UiText(this)
            {
                Placement = new UiObjectPlacement(0.5F, 0.1F, 0F, 0F, 0)
            };

            this.UiObjects.Add(text);

            //Create a binding between Text1 and the text
            PropertyBinding<string, string>.NewBinding(text, t => t.Text, this.ViewModel, vm => vm.Text1);

            this.ViewModel.Text1 = "Custom Text";

            UiText fpsText = new UiText(this)
            {
                Placement = new UiObjectPlacement(0.85F, 0.15F, 0F, 0F, 0)
            };

            this.UiObjects.Add(fpsText);

            //Create a binding between FPS and the fpsText
            PropertyBinding<string, string>.NewBinding(fpsText, t => t.Text, this.ViewModel, vm => vm.FpsStr);

            var panel = new UiTitledPanel(this, new UiObjectPlacement(0.5F, 0.4F, 0.95F, 0.4F, 0))
            {
                BackgroundColor = assets.Theme.BackgroundColor,
                Title = "TITLE"
            };

            var button3 = new UiButton(this, new UiObjectPlacement(
                (-panel.Placement.Width / 2F) + 0.15F, (-panel.Placement.Height / 2F) + 0.15F,
                0.24F, 0.1F, 0))
            {
                new UiText(this)
                {
                    Text = "Button 3"
                }
            };

            button3.Click += () => this.ViewModel.Text1 = "Button 3 clicked!";

            var timeText = new UiText(this)
            {
                Placement = new UiObjectPlacement(0.15F, 0.15F, 0F, 0F, 0)
            };

            //Create a binding between timeText and TimeText
            PropertyBinding<string, string>.NewBinding(timeText, t => t.Text, this.ViewModel, vm => vm.TimeText);

            panel.Add(timeText);

            panel.Add(button3);

            this.UiObjects.Add(panel);

            var outerButton = new UiButton(this, new UiObjectPlacement(0.5F, 0.8F, 0.9F, 0.2F, 0))
            {
                ((Func<UiButton>)(() =>
                {
                    UiButton button = new UiButton(this, new UiObjectPlacement(-0.2F, 0F, 0.3F, 0.1F, 0))
                    {
                        new UiText(this)
                        {
                            Text = "Inner Button",
                            Color = new Color(0xFFFF0000)
                        }
                    };

                    button.Click += () => this.ViewModel.Text1 = "Inner button clicked!";

                    return button;
                }))(),

                new UiText(this)
                {
                    Placement = new UiObjectPlacement(0.2F, 0F, 0F, 0F, 0),
                    Text = "Inner Text",
                    Color = new Color(0xFF0000FF)
                }
            };

            outerButton.Click += () => this.ViewModel.Text1 = "Outer button clicked!";

            this.UiObjects.Add(outerButton);
        }

        public override void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            base.Render(renderer, screenDimensions, assets, showDebugBorders);

            //Update the time property (this could be WAY more efficient)
            this.ViewModel.TimeText = $"Time: {DateTime.Now}";
        }
    }
}
