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
            this.UiObjects.Add(new UiButton(new UiObjectPlacement(0.2F, 0.1F, 0.4F, 0.2F, 0))
            {
                Content = new UiText()
                {
                    Text = "Click Me!"
                }
            });


            this.UiObjects.Add(new UiButton(new UiObjectPlacement(0.8F, 0.1F, 0.4F, 0.2F, 0))
            {
                Content = new UiText()
                {
                    Text = "Button 2"
                }
            });

            this.UiObjects.Add(new UiText()
            {
                Placement = new UiObjectPlacement(0.4F, 0.4F, 0F, 0F, 0),
                Text = "TITLE"
            });

            this.UiObjects.Add(new UiButton(new UiObjectPlacement(0.5F, 0.8F, 0.9F, 0.2F, 0))
            {
                Content = new UiAbsoluteLayoutContainer()
                {
                    ContentList = new ObservableCollection<UiObject>()
                    {
                        new UiButton(new UiObjectPlacement(-0.2F, 0F, 0.3F, 0.1F, 0))
                        {
                            Content = new UiText()
                            {
                                Text = "Inner Button",
                                Color = new Color(0xFFFF0000)
                            }
                        },

                        new UiText()
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
