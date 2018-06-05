﻿namespace GL.Servers.CoC.Packets.Messages.Server
{
    using GL.Servers.CoC.Logic;

    internal class Keep_Alive_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Keep_Alive_OK(Device Device) : base(Device)
        {
            this.Identifier = 20108;
        }
    }
}