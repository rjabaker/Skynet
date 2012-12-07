using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using KinectUtilities.Gestures;

namespace KinectUtilities
{
    public static class KinectSerializer
    {
        public static void SerializeToXml<T>(T obj, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fileStream, obj);
            fileStream.Close();
        }
        public static T DeserializeFromXml<T>(string fileName)
        {
            StreamReader streamReader = new StreamReader(fileName);
            string xml = streamReader.ReadToEnd();
            T obj = DeserializeFromXmlString<T>(xml);
            streamReader.Close();

            return obj;
        }
        public static T DeserializeFromXmlString<T>(string xml)
        {
            T result;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)serializer.Deserialize(tr);
            }

            return result;
        }

        public static void SerializeObject<T>(string filename, T item)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, item);
            stream.Close(); 
        }
        public static T DeserializeObject<T>(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            T item = (T)bFormatter.Deserialize(stream);
            stream.Close();

            return item;
        }
    }
}
