using System;
using System.IO;
using System.Xml.Serialization;
using Lab1;

namespace Lab2
{
    public class Serializer
    {
        public Exception Serialize(BlockState[] blockStates, string path)
        {
            try
            {
                using (StreamWriter stream = new StreamWriter(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockState[]));
                    xmlSerializer.Serialize(stream, blockStates);
                }
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }
        public Exception Deserialize(string path, out BlockState[] states)
        {
            bool isDeserialized = false;
            BlockState[] blockStates = null;
            while (!isDeserialized)
            {
                try
                {
                    using (StreamReader stream = new StreamReader(path))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockState[]));
                        xmlSerializer.UnknownElement += XmlSerializer_UnknownElement;
                        isDeserialized = true;
                        blockStates = (BlockState[])xmlSerializer.Deserialize(stream);
                    }
                }
                catch (Exception e)
                {
                    states = blockStates;
                    return e;
                }
            }
            states = blockStates;
            return null;
        }

        private void XmlSerializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            Console.WriteLine(e.Element.Name);
        }
    }
}