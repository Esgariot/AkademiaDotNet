using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_Lab5 {
    public class DnsWorker {
        private string[] addresses;

        public string[] Addresses {
            get { return addresses; }

            set { addresses = value; }
        }

        public DnsWorker() {
            Addresses = new[] {
                "www.microsoft.com",
                "www.apple.com",
                "www.google.com",
                "www.ibm.com",
                "cisco.netacad.net",
                "www.oracle.com",
                "www.nokia.com",
                "www.hp.com",
                "www.dell.com",
                "www.samsung.com",
                "www.toshiba.com",
                "www.siemens.com",
                "www.amazon.com",
                "www.sony.com",
                "www.canon.com",
                "www.alcatel-lucent.com",
                "www.acer.com",
                "www.motorola.com"
            };
        }
    }
}