using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Input
{
    /// <summary>
    /// A default implementation of <see cref="ITouchListener"/>
    /// </summary>
    public abstract class TouchListener : ITouchListener
    {
        public virtual bool HandleTouch(TouchInputEventArgs e)
        {
            //Delegate the touch event to the other handlers
            switch (e.Type)
            {
                case EnumTouchType.Motion:
                    return this.OnTouchMove(e);
                case EnumTouchType.Hover:
                    return this.OnTouchHover(e);
                case EnumTouchType.Down:
                    return this.OnTouchDown(e);
                case EnumTouchType.Up:
                    return this.OnTouchUp(e);
                default:
                    return false;
            }
        }

        public virtual bool OnTouchHover(TouchInputEventArgs e) => false;

        public virtual bool OnTouchDown(TouchInputEventArgs e) => false;

        public virtual bool OnTouchUp(TouchInputEventArgs e) => false;

        public virtual bool OnTouchMove(TouchInputEventArgs e) => false;

        /// <summary>
        /// A helper method for delegating touch input to child objects
        /// </summary>
        /// <typeparam name="TListener">The type of the child object</typeparam>
        /// <param name="e">The touch event</param>
        /// <param name="children">The child objects</param>
        /// <param name="callBase">A function to call the base method</param>
        /// <param name="childValidator">A function that determines whether a child is valid for this touch event</param>
        /// <returns>Whether the touch event was handled or not</returns>
        protected static bool HandleChildrenTouchHelper<TListener>(TouchInputEventArgs e, IEnumerable<TListener> children, Func<TouchInputEventArgs, bool> callBase, Func<TouchInputEventArgs, TListener, bool> childValidator = null)
            where TListener : ITouchListener
        {
            if (childValidator == null)
                childValidator = (e1, c1) => true;

            bool handled = false;

            //Attempt to have each child handle the touch
            foreach (TListener child in children)
            {
                //If the touch is handled, don't try any more children
                if (handled)
                    break;

                //If the child is valid, try to have it handle the touch
                if (childValidator(e, child))
                    handled = child.HandleTouch(e);
            }

            //If the touch is still not handled, handle it ourselves
            if (!handled)
                callBase?.Invoke(e);

            return handled;
        }
    }
}
