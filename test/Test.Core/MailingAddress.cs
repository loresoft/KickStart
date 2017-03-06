using System;
using System.Text;

namespace Test.Core
{
    public class MailingAddress
    {
        public ContactType Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }


        public override string ToString()
        {
            var b = new StringBuilder();
            if (Address1 != null)
                b.Append(Address1)
                    .Append(", ");

            if (City != null)
                b.Append(City)
                    .Append(", ");


            if (State != null)
                b.Append(State)
                    .Append(" ");

            if (Zip != null)
                b.Append(Zip);

            return b.ToString();
        }
    }
}