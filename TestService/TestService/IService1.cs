using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        TestTable GetData();

        [OperationContract]
        string PutData(TestTable tb);

        //[CallbackBehavior]
        //byte[] CallbackHandler();

        //[JavascriptCallbackBehavior]
        //void JavascriptHandler();

        // TODO: Add your service operations here
    }

    //public interface IServiceEventCallBacks
    //{
    //    [OperationContract]
    //    void EventCallback(string message);
    //}

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class TestTable
    {
        // 16 bit Word Length by default
        int length = 16;
        int row = 0;
        float column = 0;
        int id;

        string unit;// = "HEX";
        string stringValue;// = "SYNC_1";


        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = this.Row + (int)this.Column; }
        }

        [DataMember]
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        [DataMember]
        public float Column
        {
            get { return column; }
            set { column = value; }
        }

        [DataMember]
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        [DataMember]
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        [DataMember]
        public string Name
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
