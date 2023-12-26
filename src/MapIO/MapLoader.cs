using System.Xml.Serialization;
using RoguelikeWFC.MapIO.Models;

namespace RoguelikeWFC.MapIO;

public class MapLoader(string filePath)
{
    private XmlSerializer _serializer = new(typeof(SerializebleWorldMap));
    private FileStream _writer = File.OpenRead(filePath);

    public SerializebleWorldMap? Load()
    {
        SerializebleWorldMap? map = _serializer.Deserialize(_writer) as SerializebleWorldMap;
        return map;
    }
}