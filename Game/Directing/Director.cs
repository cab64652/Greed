using System;
using System.Collections.Generic;
using Greed.Game.Casting;
using Greed.Game.Services;


namespace Greed.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService _keyboardService = null;
        private VideoService _videoService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this._keyboardService = keyboardService;
            this._videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            _videoService.OpenWindow();
            while (_videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            _videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor miner = cast.GetFirstActor("miner");
            Point velocity = _keyboardService.GetDirection();
            int x = velocity.GetX();
            int y = 0;
            velocity = new Point(x, y);
            miner.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(1, 60);
                int y = 0;
                Point position = new Point(x, y);
                position = position.Scale(15);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                string symbol = "*";
                int points = 20;
                int isRock = random.Next(0, 1);
                if (isRock == 1)
                {
                    symbol = "@";
                    points = -20;
                }
                else
                {
                    
                }

                Collectable collectable = new Collectable();
                collectable.SetText(text);
                collectable.SetFontSize(15);
                collectable.SetColor(color);
                collectable.SetPosition(position);
                collectaable.SetVelocity(new Point(0, 5));
                collectable.SetPoints(points);
                cast.AddActor("collectables", collectable);
            }

            Actor miner = cast.GetFirstActor("miner");
            Actor score = cast.GetFirstActor("score");
            List<Actor> collectables = cast.GetActors("collectables");

            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();

            miner.MoveNext(maxX, maxY);
            foreach (Actor collectable in collectables)
            {
                collectable.MoveNext(maxX, maxY);
            }


            banner.SetText("");
            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor collectable in collectables)
            {
                if (miner.GetPosition().Equals(collectable.GetPosition()))
                {
                    int points = ((Collectable) collectable).GetPoints();
                    ((Score)score).AddPoints(points);
                    cast.RemoveActor("collectables", collectable);
                }
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            _videoService.ClearBuffer();
            _videoService.DrawActors(actors);
            _videoService.FlushBuffer();
        }

    }
}