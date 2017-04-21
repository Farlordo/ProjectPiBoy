using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPiBoy.SDLApp.Screens;
using ProjectPiBoy.Common.Utilities;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    public class UiTitledPanel : UiPanel
    {
        public UiTitledPanel(Screen screen, UiObjectPlacement placement) : base(screen, placement)
        {

        }

        private string _Title;
        /// <summary>The panel title</summary>
        public string Title
        {
            get => this._Title;
            set
            {
                this._Title = value;

                if (this.LabelPanel != null)
                {
                    //Remove the old label panel
                    this.ChildObjects.Remove(this.LabelPanel);

                    //Dispose of the old label panel
                    this.LabelPanel?.Dispose();
                }

                if (value != null)
                {
                    //TODO: Find a more reliable way of calculating this
                    float titleStrWidth = 0.0233F * value.Length;

                    const float titleHeight = 0.075F;
                    const float titleLeftOffset = titleHeight / 2F;

                    //Calculate the title position
                    Vector2 titlePos = new Vector2(
                        (-this.Placement.Width / 2F) + (titleStrWidth / 2F) + titleLeftOffset,
                        (-this.Placement.Height / 2F) + 0F);

                    //Create a new label panel
                    this.LabelPanel = new UiPanel(this.Screen, new UiObjectPlacement(
                        (float)titlePos.X, (float)titlePos.Y,
                        titleStrWidth + 0.01F, titleHeight, this.Placement.Depth - 1))
                    {
                        new UiText(this.Screen)
                        {
                            Text = value
                        }
                    };

                    //Set the label's background color
                    this.LabelPanel.BackgroundColor = this.BackgroundColor;

                    //Add the text
                    this.Add(this.LabelPanel);
                }
            }
        }

        /// <summary>A reference to the label panel</summary>
        public UiPanel LabelPanel { get; protected set; }
    }
}
