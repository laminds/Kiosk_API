using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Kiosk.Business.Model.Checkout
{
    public class MemberCheckOutResponseModel
    {
        public string MemberId { get; set; }
        public string AgreementNumber { get; set; }
        public string Message { get; set; }
        public string RecurringServiceId { get; set; }
        public string SGTRecurringServiceId { get; set; }
        public string PTMessage { get; set; }
        public string SGTMessage { get; set; }
        public string EXMessage { get; set; }
        public long NewMemberId { get; set; }
        public bool IsCardOnFile { get; set; }
        public bool CardOnFileStatus { get; set; }
        public string CardOnFileMessage { get; set; }
    }


    [XmlRoot(ElementName = "ABCMessages")]
    public class ABCMessages
    {
        [XmlElement(ElementName = "ABCMessage")]
        public List<ABCMessage> Message { get; set; }

    }

    [XmlRoot(ElementName = "ABCMessage")]
    public class ABCMessage
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
    }
}
