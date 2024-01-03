﻿using Dysnomia.Domain.World;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dysnomia.Domain.bin
{
    internal class connect : Command
    {
        public static String Name = "cmd_Connect";
        public static String Description = "Initiate A New Connection connect [Host] [Port]";

        protected override void Phi()
        {
            byte[] From = Encoding.Default.GetBytes(Name);
            Theta.In.Enqueue(new Tare.MSG(From, new byte[] { 0x05 }, 6));
            if (Args.Length == 0)
                Theta.Out.Enqueue(new Tare.MSG(From, Encoding.Default.GetBytes("Connect Command Requires At Least 1 Argument"), 6));
            else
            {
                Theta.In.Enqueue(new Tare.MSG(From, Encoding.Default.GetBytes(Args[0]), 6));
                if(Args.Length < 2) Theta.In.Enqueue(new Tare.MSG(From, new byte[] { 0xB3, 0x15 }, 6)); // 0x15B3 = 5555
                else Theta.In.Enqueue(new Tare.MSG(From, Encoding.Default.GetBytes(Args[1]), 6));
            }
        }
    }
}
