using System;
using System.IO;
using System.Xml.Serialization;
using Lab1;

namespace Lab2
{
    public class Serializer
    {
        public void Serialize(BlockState[] blockStates)
        {
            bool isSerialized = false;
            while (!isSerialized)
            {
                try
                {
                    Console.WriteLine("Provide path: ");
                    string path = Console.ReadLine();
                    using (StreamWriter stream = new StreamWriter(path))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockState[]));
                        xmlSerializer.Serialize(stream, blockStates);
                        isSerialized = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public BlockState[] Deserialize()
        {
            bool isDeserialized = false;
            BlockState[] blockStates = null;
            while (!isDeserialized)
            {
                try
                {
                    Console.WriteLine("Provide path: ");
                    string path = Console.ReadLine();
                    using (StreamReader stream = new StreamReader(path))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BlockState[]));
                        isDeserialized = true;
                        blockStates = (BlockState[])xmlSerializer.Deserialize(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return blockStates;
        }
    }
}