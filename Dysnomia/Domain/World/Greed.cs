﻿#pragma warning disable CS0168

using Dysnomia.Domain.bin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Dysnomia.Domain.Tare;

namespace Dysnomia.Domain.World
{
    public class Greed
    {
        public readonly String Host;
        public readonly int Port;
        public BigInteger ClientId;

        public TcpClient Mu;
        public Fa Rho;
        public Faung? Psi;
        public Living Theta;
        public bool Cone = false;
        public bool TimedOut = false;
        public short HandshakeState = 0x00;

        BigInteger PeerFoundation = 0;
        BigInteger PeerChannel = 0;
        BigInteger PeerDynamo = 0;

        public Greed(String _Host, int _Port)
        {
            Host = _Host;
            Port = _Port;
            Mu = new TcpClient();
            Rho = new Fa();
            Theta = new Living(Phi);
        }

        public Greed(TcpClient Iota)
        {
            if (Iota.Client.RemoteEndPoint as IPEndPoint == null) throw new Exception("Null Client");
            Host = ((IPEndPoint)Iota.Client.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)Iota.Client.RemoteEndPoint).Port;
            Mu = Iota;
            Rho = new Fa();
            Theta = new Living(Phi);
            Cone = true;
        }

        public Buffer Encode(String Beta)
        {
            Logging.Log("Greed", "Encoding: " + Beta, 1);
            if (Psi == null) throw new Exception("Null Psi");
            Buffer A = new Buffer(Psi, Encoding.Default.GetBytes(Beta));
            Logging.Log("Greed", "Encoded Base64: " + Convert.ToBase64String(A.Bytes), 2);
            return A;
        }

        public Buffer Decode(Buffer Beta)
        {
            Logging.Log("Greed", "Decoding Base64: " + Convert.ToBase64String(Beta.Bytes), 1);
            if (Psi == null) throw new Exception("Null Psi");
            Buffer B = new Buffer(Psi, Beta.Bytes);
            Logging.Log("Greed", "Decoded: " + Encoding.Default.GetString(B.Bytes), 2);
            return B;
        }

        private void Handshake(String Step, BigInteger Iota)
        {
            Logging.Log("Greed", String.Format("{0} {1} Handshake: {2}", Cone?"Cone":"Rod", Step, Iota, 1));
            Theta.Out.Enqueue(new Tare.MSG(Encoding.Default.GetBytes("Fi"), Encoding.Default.GetBytes(Step), Iota.ToByteArray(), 1));
        }

        private void NextHandshake(ref BigInteger Beta)
        {
            if (Cone)
            {
                if(Rho.Tau.IsZero)
                {
                    Rho.Tau = Rho.Avail(Beta);
                    Handshake("Tau", Rho.Tau);
                    HandshakeState = 0x01;
                }
                else if (Rho.Pole.IsZero && PeerChannel.IsZero)
                {
                    Rho.Form(Beta);
                    Rho.Polarize();
                    Handshake("Pole", Rho.Pole);
                    HandshakeState = 0x02;
                }
                else if (Rho.Coordinate.IsZero)
                {
                    Rho.Conjugate(ref Beta);
                    Rho.Conify();
                    Handshake("Foundation", Rho.Foundation);
                    Handshake("Channel", Rho.Channel);
                    HandshakeState = 0x03;
                }
                else if (Rho.Element.IsZero && PeerFoundation.IsZero)
                {
                    PeerFoundation = Beta;
                    HandshakeState = 0x04;
                }
                else if (Rho.Element.IsZero && PeerChannel.IsZero)
                {
                    PeerChannel = Beta;
                    Rho.Saturate(PeerFoundation, PeerChannel);
                    Rho.Bond();
                    Handshake("Dynamo", Rho.Dynamo);
                    HandshakeState = 0x05;
                }
                else if (Rho.Barn.IsZero)
                {
                    PeerDynamo = Beta;
                    Rho.Adduct(PeerDynamo);
                    Rho.Open();
                    Logging.Log("Greed", "Cone Handshake Complete: " + Rho.Barn, 2);
                    Psi = new Faung(Rho.Ring, Rho.Coordinate, Rho.Manifold, Rho.Barn, Rho.Element);
                    HandshakeState = 0x06;
                }
                else
                    throw new Exception("Not Implemented");
            }
            else
            {
                if (Rho.Alpha.IsZero)
                {
                    Rho.Alpha = Rho.Avail(Beta);
                    Handshake("Alpha", Rho.Alpha);
                    HandshakeState = 0x01;
                }
                else if (Rho.Pole.IsZero && PeerChannel.IsZero)
                {
                    Rho.Form(Beta);
                    Rho.Polarize();
                    Handshake("Pole", Rho.Pole);
                    HandshakeState = 0x02;
                }
                else if (Rho.Coordinate.IsZero)
                {
                    Rho.Conjugate(ref Beta);
                    HandshakeState = 0x03;
                }
                else if (Rho.Element.IsZero && PeerFoundation.IsZero)
                {
                    PeerFoundation = Beta;
                    HandshakeState = 0x04;
                }
                else if (Rho.Element.IsZero && PeerChannel.IsZero)
                {
                    PeerChannel = Beta;
                    Rho.Saturate(PeerFoundation, PeerChannel);
                    Rho.Bond();
                    Handshake("Foundation", Rho.Foundation);
                    Handshake("Channel", Rho.Channel);
                    Handshake("Dynamo", Rho.Dynamo);
                    HandshakeState = 0x05;
                }
                else if (Rho.Barn.IsZero)
                {
                    PeerDynamo = Beta;
                    Rho.Adduct(PeerDynamo);
                    Rho.Open();
                    Logging.Log("Greed", "Rod Handshake Complete: " + Rho.Barn, 2);
                    Psi = new Faung(Rho.Ring, Rho.Coordinate, Rho.Manifold, Rho.Barn, Rho.Element);
                    HandshakeState = 0x06;
                }
                else
                    throw new Exception("Not Implemented");
            }
        }

        void Disconnect()
        {
            try {
                Greed? Beta;
                Fi.Psi.TryRemove(ClientId, out Beta);
                NetworkStream Iota = Mu.GetStream();
                try {
                    Iota.Close();
                } catch (Exception E) { }
            } catch (Exception E) { }
            try {
                Mu.Close();
            } catch (Exception E) { }
            Logging.Log("Greed", "Disconnected " + Host, 6);
        }

        void Phi()
        {
            Thread.Sleep(10);
            if(!Mu.Connected && Theta.In.Count == 0 && Cone == false)
                Mu.Connect(new IPEndPoint(Dns.GetHostAddresses(Host)[0], Port));

            byte[] bytes = new byte[64];
            NetworkStream Iota = Mu.GetStream();
            Stopwatch stopwatch = new Stopwatch();
            short Resets = 0;

            Span<Byte> Omicron = new Span<Byte>(bytes);
            MSG? Lambda;

            while (Mu.Connected)
            {
                try
                {
                    stopwatch.Start();
                    while (Theta.In.Count > 0)
                    {
                        if (!Theta.In.TryDequeue(out Lambda)) throw new Exception("Cannot Dequeue");
                        String Subject = (Lambda.Subject == null)?"":Encoding.Default.GetString(Lambda.Subject);
                        if (Cone && Subject == "Xi")
                        {
                            BigInteger Delta = new BigInteger(Lambda.Data);
                            ClientId = Delta;
                            NextHandshake(ref Delta);
                            stopwatch.Reset();
                        }
                        else throw new Exception("Unknown Handshake Subject");
                    }

                    while (Theta.Out.Count > 0)
                    {
                        if (!Theta.Out.TryDequeue(out Lambda)) throw new Exception("Cannot Dequeue");
                        Iota.Write(Lambda.Data);
                        Iota.Write(Encoding.Default.GetBytes(Fi.DLE));
                    }

                    if (Iota.DataAvailable)
                    {
                        Thread.Sleep(200);
                        int size = Iota.Read(Omicron);

                        int A, B;
                        for (int i = A = B = 0; i < size; i++)
                        {
                            if (i == A && Omicron[A] != 0x10) B = -1;
                            if (i >= A && Omicron.Slice(i, 4).SequenceEqual<Byte>(Encoding.Default.GetBytes(Fi.DLE)))
                            {
                                if (B == 0) A = i + 4;
                                B = i - A;
                            }
                            if (B <= 0) continue;

                            BigInteger Alpha = new BigInteger(Omicron.Slice(A, B));
                            NextHandshake(ref Alpha);
                            stopwatch.Reset();

                            A = i + 4;
                            B = 0;
                            i += 3;
                        }
                        Omicron.Clear();
                    }

                    if (Theta.In.Count == 0 && Theta.Out.Count == 0 && !Rho.Barn.IsZero) return;
                    stopwatch.Stop();
                    if (stopwatch.Elapsed.TotalSeconds > 2)
                        if (++Resets > 2) throw new Exception("Handshake Timeout");
                        else
                        {
                            Logging.Log("Greed", "Handshake Timeout, Sending Reset", 6);
                            Theta.Out.Enqueue(new Tare.MSG(Encoding.Default.GetBytes("Fi"), Encoding.Default.GetBytes("Reset"), new byte[] { 0x06 }, 1));
                            stopwatch.Reset();
                        }
                    stopwatch.Start();
                } catch (Exception E) { Disconnect(); return; }
            }
        }
    }
}
