using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

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
    }
}
