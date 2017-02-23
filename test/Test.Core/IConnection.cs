using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core
{
    public interface IConnection
    {
        void Open();
    }


    public class SampleConnection : IConnection
    {
        public void Open()
        {
            
        }
    }

}
