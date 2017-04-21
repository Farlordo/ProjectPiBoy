using ProjectPiBoy.SDLApp.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Input
{
    /// <summary>
    /// A default implementation of <see cref="ITouchListener"/> that listens for routed <see cref="TouchInputEventArgs"/>
    /// </summary>
    public abstract class TouchListener : ITouchListener, IRoutedEventListener
    {
        public virtual void OnEvent(RoutedEventArgs e)
        {
            if (e is TouchInputEventArgs te)
            {
                //Delegate the touch event to the other handlers
                switch (te.Type)
                {
                    case EnumTouchType.Motion:
                        this.OnTouchMove(te);
                        break;
                    case EnumTouchType.Hover:
                        this.OnTouchHover(te);
                        break;
                    case EnumTouchType.Down:
                        this.OnTouchDown(te);
                        break;
                    case EnumTouchType.Up:
                        this.OnTouchUp(te);
                        break;
                }
            }
        }

        public virtual void OnTouchHover(TouchInputEventArgs e) { }

        public virtual void OnTouchDown(TouchInputEventArgs e) { }

        public virtual void OnTouchUp(TouchInputEventArgs e) { }

        public virtual void OnTouchMove(TouchInputEventArgs e) { }

        public virtual void OnPreRoute(RoutedEventArgs e) { }
    }
}
