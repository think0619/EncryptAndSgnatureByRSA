using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptSign.Entity
{
    public class SendMessage
    {
        public int ID { get; set; }

        /// <summary>
        /// Uid of Sender
        /// </summary>
        public string SenderUid { get; set; }

        public string SenderName { get; set; }

        /// <summary>
        /// Uid of Receiver
        /// </summary>
        public string ReceicerUid { get; set; }

        public string ReceicerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Cleartext
        /// </summary>
        public string MessageCleartext { get; set; }

        /// <summary>
        /// Ciphertext
        /// </summary>
        public string MessageCiphertext { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        public string MessageSignature { get; set; }
        
    }
}
