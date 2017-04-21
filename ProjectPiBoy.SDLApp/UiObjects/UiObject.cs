using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Input;
using ProjectPiBoy.SDLApp.Screens;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static SDL2.SDL;
using System.Collections;
using ProjectPiBoy.SDLApp.Events;

namespace ProjectPiBoy.SDLApp.UiObjects
{
    /// <summary>
    /// Represents a UI Object that can be displayed on a screen.
    /// </summary>
    public abstract class UiObject : TouchListener, IDisposable, IRenderable, IEnumerable<UiObject>, IRoutedEventListener
    {
        public UiObject(Screen screen) : this(screen, default(UiObjectPlacement)) { }

        public UiObject(Screen screen, UiObjectPlacement placement, Vector2 placementOffset = new Vector2())
        {
            this.ChildObjects = new ObservableCollection<UiObject>();
            this.ChildObjects.CollectionChanged += (s, e) => this.OnChildrenChanged(e);

            this.Screen = screen;
            this.Placement = placement;
            this.PlacementOffset = placementOffset;
        }

        /// <summary>The placement of this <see cref="UiObject"/></summary>
        public virtual UiObjectPlacement Placement
        {
            get => this._Placement;
            set
            {
                this._Placement = value;
                this.OnPlacementUpdated();
            }
        }
        protected UiObjectPlacement _Placement;

        /// <summary>The offset of the placement</summary>
        public virtual Vector2 PlacementOffset
        {
            get => this._PlacementOffset;
            set
            {
                this._PlacementOffset = value;
                this.OnPlacementUpdated();
            }
        }
        protected Vector2 _PlacementOffset;

        /// <summary>The global placement of this <see cref="UiObject"/>, recalculated whenever the placement or placement offset changes</summary>
        public UiObjectPlacement GlobalPlacement { get; protected set; }

        /// <summary>
        /// Called when either the <see cref="Placement"/> or <see cref="PlacementOffset"/> is updated
        /// </summary>
        protected void OnPlacementUpdated()
        {
            //Create a copy of this UiObject's placement
            UiObjectPlacement globalPlacement = new UiObjectPlacement(this.Placement);

            //Add the placement offset to the placement
            globalPlacement.PosVec += this.PlacementOffset;

            //Update the global placement
            this.GlobalPlacement = globalPlacement;

            //Update each child's placement offset
            foreach (UiObject child in this.ChildObjects)
                child.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
        }

        /// <summary>
        /// Determines whether this <see cref="UiObject"/> contains the specified point.
        /// </summary>
        /// <remarks>
        /// Override this if your <see cref="UiObject"/> uses its placement differently
        /// </remarks>
        /// <param name="point">The point to check</param>
        /// <returns>Whether this <see cref="UiObject"/> contains the specified point</returns>
        public virtual bool ContainsGlobalPoint(Vector2 point)
        {
            return this.GlobalPlacement.ContainsPoint(point);
        }

        /// <summary>The screen that this <see cref="UiObject"/> is on</summary>
        public Screen Screen { get; }

        /// <summary>Whether the <see cref="UiObject"/> is being hovered over</summary>
        public bool Hovered { get; protected set; }

        /// <summary>The parent <see cref="UiObject"/>. If it is null, then this is at the top.</summary>
        public UiObject Parent { get; }

        /// <summary>A collection of child <see cref="UiObject"/>s</summary>
        public virtual ObservableCollection<UiObject> ChildObjects { get; }

        /// <summary>
        /// Adds a child <see cref="UiObject"/>. For use with collection initializers.
        /// </summary>
        /// <param name="child">The child to add</param>
        public void Add(UiObject child)
        {
            this.ChildObjects.Add(child);
        }

        /// <summary>
        /// Called when the children list changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected void OnChildrenChanged(NotifyCollectionChangedEventArgs e)
        {
            //Call this.OnChildAdded() for each new item
            foreach (object obj in e.NewItems)
                if (obj is UiObject)
                    this.OnChildAdded((UiObject)obj);
        }

        /// <summary>
        /// Called whenever a child <see cref="UiObject"/> is added
        /// </summary>
        /// <param name="child">The child <see cref="UiObject"/></param>
        protected void OnChildAdded(UiObject child)
        {
            //Update the new child's placement offset
            child.PlacementOffset = this.PlacementOffset + this.Placement.PosVec;
        }

        public virtual void Render(IntPtr renderer, Vector2 screenDimensions, Assets assets, bool showDebugBorders)
        {
            bool hovered = false;

            //If there are any touches over this object, then it is being hovered over
            foreach (var touch in this.Screen.Touches)
                if (this.ContainsGlobalPoint(touch.Value))
                {
                    hovered = true;
                    break;
                }

            //Update hovered state
            this.Hovered = hovered;

            if (showDebugBorders)
            {
                //Get a SDL_Rect representing the border
                SDL_Rect border = ScreenSpaceUtil.GetGlobalBorderRectangle(this.GlobalPlacement, screenDimensions);

                //Determine the border color
                Color borderColor = this.Hovered ? assets.Theme.DebugOutlineHighlightColor : assets.Theme.DebugOutlineColor;

                //Set the color to green, and render the border rectangle
                SDLUtil.SetSDLRenderDrawColor(renderer, borderColor);
                SDL_RenderDrawRect(renderer, ref border);
            }

            //Render children
            foreach (UiObject child in this.ChildObjects)
                child?.Render(renderer, screenDimensions, assets, showDebugBorders);
        }

        public virtual void Dispose()
        {
            //Nothing to dispose

            //Dispose children
            foreach (UiObject child in this.ChildObjects)
                child?.Dispose();
        }

        public IEnumerator<UiObject> GetEnumerator() => this.ChildObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.ChildObjects.GetEnumerator();

        public override void OnEvent(RoutedEventArgs e)
        {
            base.OnEvent(e);

            //This can be overriden to handle events, but make sure to call base!
        }
    }
}
