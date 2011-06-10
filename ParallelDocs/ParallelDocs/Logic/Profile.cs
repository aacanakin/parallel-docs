using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ParallelDocs
{
    [Serializable()]

    class Profile : ISerializable
    {
        private string ip;
        private string full_name;
        private string email;
        //private string hostName; // refers to Pc name

        public Profile(string full_name, string email, string ip)
        {
            this.full_name = full_name;
            this.email = email;
            this.ip = ip;
			//this.hostName = hostName;
        }

        public string getIp() { return ip; }
        public string getFullName() { return full_name; }
        public string getEmail() { return email; }
        //public string getHostName() { return hostName; }

        public void setIp(string ip) { this.ip = ip; }
        public void setFullName(string full_name) { this.full_name = full_name; }
        public void setEmail(string email) { this.email = email; }
        //public void setHostName(string hostName) { this.hostName = hostName; }

        public Profile(SerializationInfo info, StreamingContext ctxt)
        {
            this.full_name = (string)info.GetValue("FullName", typeof(string));
            this.ip = (string)info.GetValue("Ip", typeof(string));
            this.email = (string)info.GetValue("Email", typeof(string));
			//this.hostName = (string)info.GetValue("HostName", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("FullName", this.full_name);
            info.AddValue("Ip", this.ip);
            info.AddValue("Email", this.email);
			//info.AddValue("HostName", this.hostName);
        }


    }
}
