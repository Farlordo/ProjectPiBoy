using ProjectPiBoy.SDLApp.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
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
            this.Screens.Push(new TestScreen());
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
            this.Window = SDL_CreateWindow("PiBoy", 100, 100, 800, 480, SDL_WindowFlags.SDL_WINDOW_SHOWN);

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

            SDL_Event sdlEvent;

            //Update loop
            while (this.Running)
            {
                //Handle all SDL events
                while (SDL_PollEvent(out sdlEvent) == 1)
                    this.EventListener.OnSDLEvent(sdlEvent);

                this.Update();
                this.Render();
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
            int scrWidth, scrHeight;
            SDL_GetWindowSize(this.Window, out scrWidth, out scrHeight);
            Vector2 screenDimensions = new Vector2(scrWidth, scrHeight);

            //Clear the frame buffer
            SDL_SetRenderDrawColor(this.Renderer, 0, 0, 0, 255);
            SDL_RenderClear(this.Renderer);

            if (this.Screens.Count > 0)
            {
                Screen screen = this.Screens.Peek();
                screen.Render(this.Renderer, screenDimensions, this.Assets, showDebugBorders: true);
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
