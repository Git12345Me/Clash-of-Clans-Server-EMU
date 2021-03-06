﻿namespace GL.Servers.CoC.Logic.Slots.Items
{
    using GL.Servers.CoC.Logic.Interfaces;

    using Newtonsoft.Json;

    internal class Obstacle : IGameObject
    {
        [JsonProperty("objects")]           internal Objects Objects;
        [JsonIgnore]                        internal Files.CSV_Logic.Obstacles Csv;

        [JsonProperty("id")]                public int ID { get; set; }
        [JsonProperty("data")]              public int Data { get; set; }

        [JsonProperty("x")]                 public int X { get; set; }
        [JsonProperty("y")]                 public int Y { get; set; }

        [JsonProperty("l1x")]               public int? L1X { get; set; }
        [JsonProperty("l1y")]               public int? L1Y { get; set; }
        [JsonProperty("l2x")]               public int? L2X { get; set; }
        [JsonProperty("l2y")]               public int? L2Y { get; set; }
        [JsonProperty("l3x")]               public int? L3X { get; set; }
        [JsonProperty("l3y")]               public int? L3Y { get; set; }
        [JsonProperty("l4x")]               public int? L4X { get; set; }
        [JsonProperty("l4y")]               public int? L4Y { get; set; }
        [JsonProperty("l5x")]               public int? L5X { get; set; }
        [JsonProperty("l5y")]               public int? L5Y { get; set; }
        [JsonProperty("l6x")]               public int? L6X { get; set; }
        [JsonProperty("l6y")]               public int? L6Y { get; set; }
        [JsonProperty("l7x")]               public int? L7X { get; set; }
        [JsonProperty("l7y")]               public int? L7Y { get; set; }

        [JsonProperty("clear_t")]           internal float? ClearTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/>
        /// </summary>
        internal Obstacle()
        {
            // Obstacle.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Obstacle"/>
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="Data">The data identifier.</param>
        internal Obstacle(int ID, int Data)
        {
            this.ID   = ID;
            this.Data = Data;
        }

        /// <summary>
        /// Refreshes the <see cref="Obstacle"/>
        /// </summary>
        /// <param name="Tick"></param>
        public void Refresh(float Tick)
        {
            if (this.ClearTime.HasValue)
            {
                this.ClearTime -= Tick;

                if (this.ClearTime <= 0f)
                {
                    this.ClearTime = null;

                    this.Objects.Obstacles.Remove(this.ID);
                }
            }
        }

        /// <summary>
        /// Move the <see cref="Obstacle"/>.
        /// </summary>
        /// <param name="X">X position</param>
        /// <param name="Y">Y position</param>
        /// <returns>True if sucess</returns>
        internal bool Move(int X, int Y)
        {
            if (this.Objects.Collider.Move(this.X, this.Y, X, Y, this.Csv.Width, this.Csv.Height, this.ID))
            {
                this.X = X;
                this.Y = Y;

                return true;
            }

            return false;
        }
    }
}
