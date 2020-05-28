using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptSign.Entity
{

    [Serializable]
    public class User
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int ID { get; set; }  

        /// <summary>
        /// Unique ID 
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Public Key,Generated from PKG
        /// </summary>
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        public string PwdHash { get; set; }
        /// <summary>
        /// Private Key,Generated from PKG
        /// </summary>
        public int Status { get; set; }
    }
}
