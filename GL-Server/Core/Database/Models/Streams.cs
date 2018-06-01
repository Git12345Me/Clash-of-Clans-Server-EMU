namespace GL.Servers.CoC.Core.Database.Models
{
    public class Streams
    {
        /// <summary>
        /// Gets or sets the high identifier.
        /// </summary>
        internal int HighID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the low identifier.
        /// </summary>
        internal int LowID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        internal string Data
        {
            get;
            set;
        }
    }
}