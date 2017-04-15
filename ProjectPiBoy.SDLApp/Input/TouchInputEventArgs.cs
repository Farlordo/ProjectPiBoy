using ProjectPiBoy.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ProjectPiBoy.SDLApp.Input
{
    public class TouchInputEventArgs : EventArgs
    {
        /// <summary>
        /// The type of the touch input
        /// </summary>
        public EnumTouchType Type { get; }

        /// <summary>
        /// The time that the input occurred, in milliseconds since application startup
        /// </summary>
        public uint Timestamp { get; }

        /// <summary>
        /// The ID of the input device that caused the input
        /// </summary>
        public long InputID { get; }

        /// <summary>
        /// The ID of the finger that caused the input, for touch screens
        /// </summary>
        public long FingerID { get; }

        /// <summary>
        /// The position of the touch
        /// </summary>
        public Vector2 Pos { get; set; }

        /// <summary>
        /// The movement of the touch
        /// </summary>
        public Vector2 Delta { get; }

        /// <summary>
        /// The pressure level of the touch
        /// </summary>
        public float Pressure { get; }

        /// <summary>
        /// Creates a new <see cref="TouchInputEventArgs"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="timestamp"></param>
        /// <param name="inputID"></param>
        /// <param name="fingerID"></param>
        /// <param name="pos"></param>
        /// <param name="delta"></param>
        /// <param name="pressure"></param>
        public TouchInputEventArgs(EnumTouchType type, uint timestamp, long inputID, long fingerID, Vector2 pos, Vector2 delta, float pressure)
        {
            this.Type = type;
            this.Timestamp = timestamp;
            this.InputID = inputID;
            this.FingerID = fingerID;
            this.Pos = pos;
            this.Delta = delta;
            this.Pressure = pressure;
        }

        public override string ToString() => $"TouchInputEventArgs(Type: {this.Type}, Timestamp: {this.Timestamp}, InputID: {this.InputID}, FingerID: {this.FingerID}, Pos: {this.Pos}, Delta: {this.Delta}, Pressure: {this.Pressure:P})";

        /// <summary>
        /// Creates a new <see cref="TouchInputEventArgs"/> object from an <see cref="SDL_TouchFingerEvent"/>
        /// </summary>
        /// <param name="e"> The <see cref="SDL_TouchFingerEvent"/>{</param>
        /// <returns>A <see cref="TouchInputEventArgs"/> constructed from the <see cref="SDL_TouchFingerEvent"/></returns>
        public static TouchInputEventArgs FromTouch(SDL_TouchFingerEvent e)
        {
            EnumTouchType type;

            switch (e.type)
            {
                case (uint)SDL_EventType.SDL_FINGERMOTION:
                    type = EnumTouchType.Motion;
                    break;
                case (uint)SDL_EventType.SDL_FINGERDOWN:
                    type = EnumTouchType.Down;
                    break;
                case (uint)SDL_EventType.SDL_FINGERUP:
                    type = EnumTouchType.Up;
                    break;
                default:
                    throw new ArgumentException($"Event wasn't a touch event! Event type: {e.type}", nameof(e));
            }

            return new TouchInputEventArgs(
                type,
                e.timestamp,
                e.touchId,
                e.fingerId,
                new Vector2(e.x, e.y),
                new Vector2(e.dx, e.dy),
                e.pressure);
        }

        /// <summary>
        /// Creates a new <see cref="TouchInputEventArgs"/> object from an <see cref="SDL_MouseButtonEvent"/>
        /// </summary>
        /// <param name="e"> The <see cref="SDL_MouseButtonEvent"/>{</param>
        /// <returns>A <see cref="TouchInputEventArgs"/> constructed from the <see cref="SDL_MouseButtonEvent"/></returns>
        public static TouchInputEventArgs FromMouseInput(SDL_MouseButtonEvent e)
        {
            EnumTouchType type;

            switch (e.type)
            {
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    type = EnumTouchType.Down;
                    break;
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    type = EnumTouchType.Up;
                    break;
                default:
                    throw new ArgumentException($"Event wasn't a mouse event! Event type: {e.type}", nameof(e));
            }

            return new TouchInputEventArgs(
                type,
                e.timestamp,
                0,
                0,
                new Vector2(e.x, e.y),
                new Vector2(0, 0),
                1); //Max pressure
        }

        /// <summary>
        /// Creates a new <see cref="TouchInputEventArgs"/> object from an <see cref="SDL_MouseMotionEvent"/>
        /// </summary>
        /// <param name="e"> The <see cref="SDL_MouseMotionEvent"/>{</param>
        /// <returns>A <see cref="TouchInputEventArgs"/> constructed from the <see cref="SDL_MouseMotionEvent"/></returns>
        public static TouchInputEventArgs FromMouseMotion(SDL_MouseMotionEvent e)
        {
            EnumTouchType type;

            //If any button is pressed, this is a mouse drag (ignore which button it was)
            if (e.state > 0)
                type = EnumTouchType.Motion;
            else
                type = EnumTouchType.Hover;

            return new TouchInputEventArgs(
                type,
                e.timestamp,
                0,
                0,
                new Vector2(e.x, e.y),
                new Vector2(e.xrel, e.yrel),
                type == EnumTouchType.Hover ? 0 : 1); //No pressure if hovering
        }
    }
}
