﻿/// <summary>
/// A Ship has all the details about itself. For example the shipname,
/// size, number of hits taken and the location. Its able to add tiles,
/// remove, hits taken and if its deployed and destroyed.
/// <remarks>
/// Deployment information is supplied to allow ships to be drawn.
/// </remarks>
/// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace MyGame
{
    public class Ship
    {
        private ShipName _shipName;
        private int _sizeOfShip;
        private int _hitsTaken = 0;
        private List<Tile> _tiles;
        private int _row;
        private int _col;
        private Direction _direction;

        /// <summary>
        /// The type of ship.
        /// <value>The type of ship</value>
        /// <returns>The type of ship</returns>
        /// </summary>
        public string Name
        {
            get
            {
                if (_shipName == ShipName.AircraftCarrier)
                    return "Aircraft Carrier";

                return _shipName.ToString();
            }
        }

        /// <summary>
        /// The number of cells that this ship occupies.
        /// <value>The number of hits the ship can take</value>
        /// <returns>The number of hits the ship can take</returns>
        /// </summary>
        public int Size
        {
            get
            {
                return _sizeOfShip;
            }
        }

        /// <summary>
        /// The number of hits that the ship has taken.
        /// <value>The number of hits the ship has taken.</value>
        /// <returns>The number of hits the ship has taken</returns>
        /// <remarks>When this equals Size the ship is sunk</remarks>
        /// </summary>
        public int Hits
        {
            get
            {
                return _hitsTaken;
            }
        }

        /// <summary>
        /// The row location of the ship.
        /// <value>The topmost location of the ship</value>
        /// <returns>the row of the ship</returns>
        /// </summary>
        public int Row
        {
            get
            {
                return _row;
            }
        }

        public int Column
        {
            get
            {
                return _col;
            }
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }
        }

        public Ship(ShipName ship)
        {
            _shipName = ship;
            _tiles = new List<Tile>();

            // Gets the ship size from the enumerator.
            _sizeOfShip = (int)_shipName;
        }

        /// <summary>
        /// Adds the ship tile.
        /// <param name="tile">one of the tiles the ship is on</param>
        /// </summary>
        public void AddTile(Tile tile)
        {
            _tiles.Add(tile);
        }

        /// <summary>
        /// Clears the tile back to a sea tile.
        /// </summary>
        public void Remove()
        {
            foreach (Tile tile in _tiles)
                tile.ClearShip();
            _tiles.Clear();
        }

        public void Hit()
        {
            _hitsTaken = _hitsTaken + 1;
        }

        /// <summary>
        /// Returns if the ships is deployed, if its deployed it has more than 0 tiles.
        /// </summary>
        public bool IsDeployed
        {
            get
            {
                return _tiles.Count > 0;
            }
        }

        /// <summary>
        /// Returns if the ships is destroyed, if its destroyed it has an equal size and
        /// number of hits taken.
        /// </summary>
        public bool IsDestroyed
        {
            get
            {
                return Hits == Size;
            }
        }

        /// <summary>
        /// Record that the ship is now deployed.
        /// <param name="direction"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// </summary>
        internal void Deployed(Direction direction, int row, int col)
        {
            _row = row;
            _col = col;
            _direction = direction;
        }
    }
}