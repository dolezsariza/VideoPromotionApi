﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VideoPromotionApi.Services
{
    public class IPGetter
    {
        public string GetIpOfHost()
        {
            try
            {
                string hostName = Dns.GetHostName();
                string hostIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
                return hostIP;
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
