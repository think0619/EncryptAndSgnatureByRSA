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
        /// ID of Sender
        /// </summary>
        public int SenderID { get; set; }

        public string SenderName { get; set; }

        public string SendeUid { get; set; }
        /// <summary>
        ///ID of Receiver
        /// </summary>
        public int ReceiverID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SentTime { get; set; }

        /// <summary>
        /// Cleartext
        /// </summary>
        public string MessagePlaintext { get; set; }

        /// <summary>
        /// Ciphertext
        /// </summary>
        public string MessageCiphertext { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        public string MessageSignature { get; set; }
     
        
        /// <summary>
        /// 1:未读 2：已读
        /// </summary>
        public int Status { get; set; }
    }
}
