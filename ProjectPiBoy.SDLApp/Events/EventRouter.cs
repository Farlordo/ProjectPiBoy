using ProjectPiBoy.SDLApp.UiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Events.RoutedExtensions
{
    public static class EventRouter
    {
        /// <summary>
        /// Routes the event to the <see cref="UiObject"/> container until it is handled.
        /// </summary>
        /// <param name="uiObjects">The <see cref="UiObject"/> container</param>
        /// <param name="e">The event to route</param>
        public static void Route(this IEnumerable<UiObject> uiObjects, RoutedEventArgs e)
        {
            //Notify the object that the event is about to be routed
            if (uiObjects is IRoutedEventListener listener)
                listener.OnPreRoute(e);

            //Try to route to each UiObject
            foreach (UiObject uiObject in uiObjects)
            {
                //If the UiObject is targeted
                if (uiObject.GlobalPlacement.ContainsPoint(e.Pos))
                {
                    //Route the event to child objects first
                    Route(uiObject, e);

                    //If nothing else has been the lowest hit at this point, then this is the lowest hit
                    if (e.LowestHit == null)
                        e.LowestHit = uiObject;

                    if (!e.Handled)
                        uiObject.OnEvent(e); //Try to handle the event
                    else
                        break; //The event was handled, stop routing it
                }
            }
        }
    }
}
