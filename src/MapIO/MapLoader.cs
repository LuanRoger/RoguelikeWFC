using System.Xml.Serialization;
using RoguelikeWFC.MapIO.Models;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC.MapIO;

public class MapLoader
{
    private XmlSerializer _serializer;
    private FileStream _writer;

    public MapLoader(string filePath)
    {
        _serializer = new(typeof(SerializebleWorldMap));
        _writer = File.OpenRead(filePath);
    }
    
    public SerializebleWorldMap? Load()
    {
        SerializebleWorldMap? map = _serializer.Deserialize(_writer) as SerializebleWorldMap;
        return map;
    }
}