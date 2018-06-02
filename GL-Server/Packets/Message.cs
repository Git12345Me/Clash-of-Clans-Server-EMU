﻿namespace GL.Servers.CoC.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    #endregion Usings

    internal class Message
    {
        internal ushort Identifier;
        internal uint Length;
        internal ushort Version;

        internal int Offset;

        /// <summary>
        /// The device, technically called as 'client'.
        /// </summary>
        internal Device Device;

        /// <summary>
        /// The message reader, used to.. read the message.
        /// </summary>
        internal Reader Reader;

        /// <summary>
        /// The message writer, used to.. write the message.
        /// </summary>
        internal List<byte> Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Message(Device Device)
        {
            this.Device = Device;
            this.Data   = new List<byte>(Constants.SendBuffer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        internal Message(Device Device, Reader Reader)
        {
            this.Device = Device;
            this.Reader = Reader;
        }

        /// <summary>
        /// Gets or sets the packet data, in/from a byte array.
        /// </summary>
        /// <returns>The packet data, in a byte array, header included.</returns>
        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddUShort(this.Identifier);
                Packet.AddUInt24(this.Length);
                Packet.AddUShort(this.Version);
                Packet.AddRange(this.Data);

                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Decodes the <see cref="Message"/>, using the <see cref="Reader"/> instance.
        /// </summary>
        internal virtual void Decode()
        {
            // Decode.
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer"/> instance.
        /// </summary>
        internal virtual void Encode()
        {
            // Encode.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Process.
        }

        /// <summary>
        /// Decrypts the message, using the Sodium cryptography.
        /// </summary>
        /// <exception cref="CryptographicException">We tried to decrypt an incomplete message.</exception>
        internal virtual void Decrypt()
        {
            if (this.Device.State >= State.LOGIN)
            {
                byte[] Decrypted    = this.Reader.ReadBytes((int) this.Length);

                this.Device.Crypto.Decrypt(ref Decrypted);

                this.Reader         = new Reader(Decrypted);
                this.Length         = (ushort) this.Reader.BaseStream.Length;
            }
        }

        /// <summary>
        /// Encrypts the message, using the Sodium cryptography.
        /// </summary>
        internal virtual void Encrypt()
        {
            if (this.Device.State >= State.LOGIN)
            {
                byte[] Encrypted = this.Data.ToArray();

                this.Device.Crypto.Encrypt(ref Encrypted);

                this.Data.Clear();
                this.Data.AddRange(Encrypted);
            }

            this.Length = (ushort) this.Data.Count;
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void Debug()
        {
            Console.WriteLine(this.GetType().Name + " : " + BitConverter.ToString(this.Reader.ReadBytes((int) (this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))));
        }

        /// <summary>
        /// Shows the values.
        /// </summary>
        internal void ShowValues()
        {
            Console.WriteLine(Environment.NewLine);

            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Console.WriteLine(ConsolePad.Padding(this.GetType().Name) + " - " + ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }

        /// <summary>
        /// Logs this instance.
        /// </summary>
        internal void Log()
        {
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\Logs\\" + this.GetType().Name + ".log", BitConverter.ToString(this.Reader.ReadBytes((int)(this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))) + Environment.NewLine);
        }
    }
}
