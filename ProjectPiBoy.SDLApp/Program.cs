using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Events.RoutedExtensions;
using ProjectPiBoy.SDLApp.Screens;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
namespace ProjectPiBoy.SDLApp
{
    public class Program : IDisposable
    {
        /// <summary>Whether the program is running its update loop or not. Setting this to false stops the program.</summary>
        public bool Running { get; private set; }
        
        /// <summary>The <see cref="Stopwatch"/> used for tracking frame render time.</summary>
        private Stopwatch FrameStopwatch { get; set; }

        /// <summary>The target framerate, in frames per second (fps)</summary>
        public const double TargetFrameRate = 30;
        /// <summary>The target frame time, in milliseconds</summary>
        public const double TargetFrameTime = 1000D / TargetFrameRate;

        ///// <summary>The actual measured frame rate, in frames per second (fps)</summary>
        //public double FrameRate { get; private set; }

        /// <summary>An event that gets fired when a frame is rendered, useful for setting VM properties</summary>
        public event Action<(double frameRate, double frameTime)> FrameRendered;

        /// <summary>An <see cref="SDLEventListener"/> for the program.</summary>
        private SDLEventListener EventListener;

        /// <summary>The assets for the program.</summary>
        private Assets Assets { get; set; }

        /// <summary>A stack of screens. The screen on top of the stack is the one that will be displayed.</summary>
        private Stack<Screen> Screens { get; }

        //Handles
        private IntPtr Window;
        private IntPtr Renderer;

        /// <summary>
        /// Constructs a new instance of the program.
        /// </summary>
        public Program()
        {
            this.Screens = new Stack<Screen>();

            //Handle fps updates
            this.FrameRendered += e => this.Screens.Peek()?.OnFpsUpdate(e.frameRate, e.frameTime);
        }

        public void SetupScreen()
        {
            this.Screens.Push(new TestScreen(this.Assets));
        }

        /// <summary>
        /// Initializes the program's resources. Returns whether it was successful or not.
        /// </summary>
        /// <returns>Whether initialization was successful or not</returns>
        public bool Init()
        {
            //Initialize SDL
            if (SDL_Init(SDL_INIT_AUDIO | SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"SDL_Init() error: \"{SDL_GetError()}\"");
                return false;
            }

            //Initialize SDL_TTF
            if (TTF_Init() < 0)
            {
                Console.WriteLine($"TTF_Init() error: \"{SDL_GetError()}\"");
                return false;
            }

            //Create the window
            this.Window = SDL_CreateWindow("PiBoy", 100, 100, 640, 480, SDL_WindowFlags.SDL_WINDOW_SHOWN);

            //If the window pointer is null
            if (this.Window == IntPtr.Zero)
            {
                Console.WriteLine($"SDL_CreateWindow() error: \"{SDL_GetError()}\"");
                SDL_Quit();
                return false;
            }

            //Create a renderer for the window
            this.Renderer = SDL_CreateRenderer(this.Window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            //If the renderer pointer is null
            if (this.Renderer == IntPtr.Zero)
            {
                SDL_DestroyWindow(this.Window);
                Console.WriteLine($"SDL_CreateRenderer() error: \"{SDL_GetError()}\"");
                SDL_Quit();
                return false;
            }

            this.Assets = new Assets();

            if (!this.Assets.Init())
            {
                SDL_DestroyWindow(this.Window);
                Console.WriteLine("An error occurred while initializing the assets.");
                SDL_Quit();
                return false;
            }

            //Create a new event listener
            this.EventListener = new SDLEventListener();

            //Quit the program when the quit event is fired
            this.EventListener.QuitRequested += (s, e) => this.Running = false;

            //DEBUG
            //this.EventListener.TouchEvent += (s, e) => Console.WriteLine(e);

            //Delegate touch events to the top screen
            this.EventListener.TouchEvent += (s, e) =>
            {
                if (this.Screens.Count > 0)
                {
                    SDL_GetWindowSize(this.Window, out int width, out int height);

                    //Convert to percent coordinates
                    e.Pos /= new Vector2(width, height);

                    //Route the touch event to the screen
                    this.Screens.Peek()?.Route(e);
                }
            };

            //Set up the screen
            this.SetupScreen();

            return true;
        }

        /// <summary>
        /// Executes the program. This will both initialize and dispose of the program's resources when it needs to.
        /// </summary>
        /// <returns>An error code if the program encounters an error it can't handle.</returns>
        public int Execute()
        {
            //Initialize the program. If it fails return -1.
            if (!this.Init())
                return -1;

            //Start the program running
            this.Running = true;
            this.FrameStopwatch = Stopwatch.StartNew();
            //ulong frame = 0;

            //Update loop
            while (this.Running)
            {
                this.FrameStopwatch.Reset();
                this.FrameStopwatch.Start();

                //Handle all SDL events
                while (SDL_PollEvent(out SDL_Event sdlEvent) == 1)
                    this.EventListener.OnSDLEvent(sdlEvent);

                this.Update();
                this.Render();


                //Measure the time it took to render
                this.FrameStopwatch.Stop();
                long frameTime = this.FrameStopwatch.ElapsedMilliseconds;
                this.FrameStopwatch.Start();

                //Calculate the time to wait until the next frame
                int timeToSleep = (int) Math.Max(0, TargetFrameTime - frameTime);

                //this.FrameStopwatch.Stop();
                //Console.WriteLine($"Frame {frame} done rendering: Elapsed time: {frameTime} ms, sleeping for {timeToSleep} ms");

                //Wait until the next frame
                Thread.Sleep(timeToSleep);

                //Measure the time it took to render and sleep
                this.FrameStopwatch.Stop();
                frameTime = this.FrameStopwatch.ElapsedMilliseconds;

                //Calculate the frame rate
                double fps = 1000D / frameTime;

                //Console.WriteLine($"FPS: {this.FrameRate}");
                //Console.WriteLine($"Frame {frame} finished: Elapsed time: {frameTime} ms, fps: {this.FrameRate}");

                //frame++;

                //Fire the FrameRendered event
                this.FrameRendered?.Invoke((fps, frameTime));
            }

            //Once the update loop has been exited, dispose of the program's resources
            this.Dispose();

            //Program shut down successfully
            return 0;
        }

        /// <summary>
        /// Called every tick to update the state of the program.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Called every frame to render the program.
        /// </summary>
        public void Render()
        {
            SDL_GetWindowSize(this.Window, out int scrWidth, out int scrHeight);
            Vector2 screenDimensions = new Vector2(scrWidth, scrHeight);

            //Clear the frame buffer
            SDLUtil.SetSDLRenderDrawColor(this.Renderer, this.Assets.Theme.BackgroundColor);
            SDL_RenderClear(this.Renderer);

            if (this.Screens.Count > 0)
            {
                Screen screen = this.Screens.Peek();
                screen.Render(this.Renderer, screenDimensions, this.Assets, showDebugBorders: false);
            }

            //Update the screen
            SDL_RenderPresent(this.Renderer);
        }

        /// <summary>
        /// Disposes the program's resources.
        /// </summary>
        public void Dispose()
        {
            SDL_DestroyRenderer(this.Renderer);
            SDL_DestroyWindow(this.Window);

            //Dispose of all remaining screens
            while (this.Screens.Count > 0)
                this.Screens.Pop().Dispose();

            this.Assets.Dispose();
        }

        /// <summary>
        /// The entry point for the program
        /// </summary>
        /// <param name="args">An array of arguments passed to the program</param>
        public static int Main(string[] args)
        {
            int exitCode = new Program().Execute();
            Environment.ExitCode = exitCode;

            //Keep the console open if it wasn't a clean exit
            if (exitCode != 0)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            return exitCode;
        }
    }
}
