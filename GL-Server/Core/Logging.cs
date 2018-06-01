﻿namespace GL.Servers.CoC.Core
{
    using System;
    using System.Diagnostics;

    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Messages.Server;

    internal static class Logging
    {
        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Message">The message.</param>
        internal static void Error(Type Type, string Message)
        {
            Resources.Logger.Error(Type.Name + " : " + Message);
            Debug.WriteLine("[*] " + Type.Name + " : " + Message);
        }

        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Device">The device.</param>
        /// <param name="Message">The message.</param>
        /// <param name="ServerError">if set to <c>true</c> [server error].</param>
        internal static void Error(Type Type, Device Device, string Message, bool ServerError = true)
        {
            Logging.Error(Type, Message);
            Resources.Sentry.Catch(new Exception(Type.Name + " : " + Message), Device.Model, Device.OS, Device.OSVersion);

            if (ServerError)
            {
                new Server_Error(Device, Type.Name + " : " + Message).Send();
            }
        }
    }
}