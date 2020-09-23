using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LambdaBW_Conway_sGameOfLife
{
    struct Tile
    {
        public Vector2 Location { get; set; }
        public bool CurrentState { get; set; }

        public Tile(Vector2 location, bool currentState)
        {
            Location = location;
            CurrentState = currentState;
        }
    }
}
