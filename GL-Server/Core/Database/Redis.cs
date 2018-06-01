﻿namespace GL.Servers.CoC.Core.Database
{
    using System.Diagnostics;

    using GL.Servers.Core.Consoles;
    using GL.Servers.Logic.Enums;

    using StackExchange.Redis;

    internal class Redis
    {
        internal ConnectionMultiplexer Multiplexer;
        internal IServer Server;

        internal static IDatabase Players;
        internal static IDatabase Clans;
        internal static IDatabase Tournaments;
        internal static IDatabase Battles;

        /// <summary>
        /// Initializes a new instance of the <see cref="Redis"/> class.
        /// </summary>
        internal Redis()
        {
            if (Constants.Database == DBMS.Redis || Constants.Database == DBMS.Both)
            {
                ConfigurationOptions Configuration = new ConfigurationOptions();

                Configuration.EndPoints.Add("ns3.gobelinland.fr", 6379);

                Configuration.Password              = "Gobelinlal0i1gigaonl0la1llnbLand";
                Configuration.ClientName            = this.GetType().Assembly.FullName;
                Configuration.AbortOnConnectFail    = false;

                Configuration.SyncTimeout           = 7000;
                Configuration.ConnectTimeout        = 7000;
                Configuration.ResponseTimeout       = 7000;

                this.Multiplexer                    = ConnectionMultiplexer.Connect(Configuration, new RedisTextWriter());
                this.Server                         = this.Multiplexer.GetServer(Configuration.EndPoints[0]);

                Redis.Players                       = this.Multiplexer.GetDatabase((int) Logic.Enums.Database.Players);
                Redis.Clans                         = this.Multiplexer.GetDatabase((int) Logic.Enums.Database.Clans);
                // Redis.Tournaments                   = this.Multiplexer.GetDatabase((int) Logic.Enums.Database.Tournaments);
                // Redis.Battles                       = this.Multiplexer.GetDatabase((int) Logic.Enums.Database.Battles);

                this.Multiplexer.ErrorMessage      += this.OnError;
                this.Multiplexer.ConnectionFailed  += this.OnConnectionFailed;
            }
        }

        private void OnConnectionFailed(object Sender, ConnectionFailedEventArgs Error)
        {
            Debug.WriteLine("[*] " + Error.Exception.Message);
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Error">The <see cref="RedisErrorEventArgs"/> instance containing the event data.</param>
        private void OnError(object Sender, RedisErrorEventArgs Error)
        {
            Debug.WriteLine("[*] " + Error.Message);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            if (Constants.Database == DBMS.Redis || Constants.Database == DBMS.Both)
            {
                this.Multiplexer.Close();
            }
        }
    }
}
