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
        public TestScreen()
        {
            var clickMeButton = new UiButton(this, new UiObjectPlacement(0.2F, 0.1F, 0.4F, 0.2F, 0))
            {
                Content = new UiText(this)
                {
                    Text = "Click Me!"
                }
            };

            clickMeButton.Click += () => Console.WriteLine("Click Me button clicked!");

            this.UiObjects.Add(clickMeButton);


            this.UiObjects.Add(new UiButton(this, new UiObjectPlacement(0.8F, 0.1F, 0.4F, 0.2F, 0))
            {
                Content = new UiText(this)
                {
                    Text = "Button 2"
                }
            });

            this.UiObjects.Add(new UiText(this)
            {
                Placement = new UiObjectPlacement(0.4F, 0.4F, 0F, 0F, 0),
                Text = "TITLE"
            });

            this.UiObjects.Add(new UiButton(this, new UiObjectPlacement(0.5F, 0.8F, 0.9F, 0.2F, 0))
            {
                Content = new UiAbsoluteLayoutContainer(this)
                {
                    ContentList = new ObservableCollection<UiObject>
                    {
                        new UiButton(this, new UiObjectPlacement(-0.2F, 0F, 0.3F, 0.1F, 0))
                        {
                            Content = new UiText(this)
                            {
                                Text = "Inner Button",
                                Color = new Color(0xFFFF0000)
                            }
                        },

                        new UiText(this)
                        {
                            Placement = new UiObjectPlacement(0.2F, 0F, 0F, 0F, 0),
                            Text = "Inner Text",
                            Color = new Color(0xFF0000FF)
                        }
                    }
                }
            });
        }
    }
}
