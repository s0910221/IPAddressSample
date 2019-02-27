using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace testIP
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.2.3.0 / 24
//1.32.232.0 / 21


            IPAddress ip = IPAddress.Parse("192.168.5.6");
            IPAddress ip2 = IPAddress.Parse("192.168.0.0");
            IPAddress sub = IPAddress.Parse("255.255.0.0");
            IPAddress sub2 = IPAddress.Parse("255.255.255.0");
            Console.WriteLine(ip.IsInSameSubnet(ip2, sub));
            Console.WriteLine(ip.IsInSameSubnet(ip2, sub2));
            Console.WriteLine(new IPAddress(0xFFFF0022));
            Console.WriteLine(new IPAddress((long)Math.Pow(2, 8)-1));
            Console.WriteLine(new IPAddress((long)Math.Pow(2, 30)-1));
            Console.WriteLine(new IPAddress((long)Math.Pow(2, 1)-1));
            Console.WriteLine(new IPAddress((long)Math.Pow(2, 32)-1));
            Console.WriteLine(Int32.Parse("20"));




        }
    }

    public static class IPAddressExtensions
    {
        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress network1 = address.GetNetworkAddress(subnetMask);
            IPAddress network2 = address2.GetNetworkAddress(subnetMask);

            return network1.Equals(network2);
        }
    }
}
