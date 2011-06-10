using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;


namespace ParallelDocs.Logic
{
    class ProfileManager
    {
        private static Profile current_profile = null;

        public static void init() {

            current_profile = new Profile("", "", "");
            Stream stream = File.Open("profile.dat", FileMode.Create, FileAccess.Write);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, current_profile);
            stream.Close();
        }
        public static void setCurrentProfile(Profile p) { current_profile = p; }
        public static Profile getCurrentProfile() { return current_profile; }

        public static void saveProfile(Profile p) { 
        
            //save the profile to profile.dat

            Stream stream = File.Open("profile.dat", FileMode.Create, FileAccess.Write);
            BinaryFormatter bformatter = new BinaryFormatter();

            Console.WriteLine("Writing Employee Information");
            bformatter.Serialize(stream, p);
            stream.Close();

            current_profile = p;

            // Write the serialized current profile into the "profile.dat" file
        }


        public static void loadProfile() { 

            Stream stream = File.Open("profile.dat", FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
        
            Console.WriteLine("Reading Employee Information");
            current_profile = (Profile)bformatter.Deserialize(stream);
            stream.Close();
                                       
        // Read the "profile.dat" file , convert it to the Profile object. set the current profile
        
        }

        public static bool createProfile(string fullname, string email) {

            if (!((Regex.IsMatch(fullname, "^[A-Za-z]+$")) && (Regex.IsMatch(email, "^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$"))))
            {
                return false;
            }

            else
            {
                Profile p = new Profile(fullname, email, null);
                ProfileManager.saveProfile(p);
                return true;
            }
        }

        
        

    }
}
