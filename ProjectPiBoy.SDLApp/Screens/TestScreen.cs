using ProjectPiBoy.Common.Utilities;
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
        public TestScreen(Assets assets)
        {
            var clickMeButton = new UiButton(this, new UiObjectPlacement(0.12F, 0.05F, 0.24F, 0.1F, 0))
            {
                new UiText(this)
                {
                    Text = "Click Me!"
                }
            };

            clickMeButton.Click += () => Console.WriteLine("Click Me button clicked!");

            this.UiObjects.Add(clickMeButton);

            this.UiObjects.Add(new UiButton(this, new UiObjectPlacement(0.88F, 0.05F, 0.24F, 0.1F, 0))
            {
                new UiText(this)
                {
                    Text = "Button 2"
                }
            });


            this.UiObjects.Add(new UiText(this)
            {
                Placement = new UiObjectPlacement(0.4F, 0.1F, 0F, 0F, 0),
                Text = "Text"
            });

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

            button3.Click += () => Console.WriteLine("Button 3 clicked!");

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

                    button.Click += () => Console.WriteLine("Inner button clicked!");

                    return button;
                }))(),

                new UiText(this)
                {
                    Placement = new UiObjectPlacement(0.2F, 0F, 0F, 0F, 0),
                    Text = "Inner Text",
                    Color = new Color(0xFF0000FF)
                }
            };

            outerButton.Click += () => Console.WriteLine("Outer button clicked!");

            this.UiObjects.Add(outerButton);
        }
    }
}
