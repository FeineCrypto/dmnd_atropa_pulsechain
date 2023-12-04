﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dysnomia.Domain;

namespace Dysnomia
{
    public class 錨
    {
        public 锚 Mu;
        public 锚 Rho;
        public 锚 Psi;


        private static void Anchor(ref 锚 Beta)
        {
            Beta = new 锚();
        }

        public 錨()
        {
            Task t0 = new Task(() => { Anchor(ref Mu); Mu.Pi(); });
            t0.Start();
            Task t1 = new Task(() => { Anchor(ref Rho); Rho.Pi(); });
            t1.Start();
            Task t2 = new Task(() => { Anchor(ref Psi); Psi.Pi(); });
            t2.Start();

            while(Mu == null || Rho == null || Psi == null) {
                System.Threading.Thread.Sleep(2000);
            }
        }

        public OpCode Code(String Beta = null)
        {
            if(Beta == null)
                return new OpCode(Mu.Rho.Psi.Theta.Xi[0]);
            else
                return new OpCode(Mu.Rho.Psi.Theta.Xi[0], Beta);
        }
    }
}
