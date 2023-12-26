using System.Xml.Serialization;
using LawGen.Core.Tiling;
using LawGen.WFC;
using RoguelikeWFC.MapIO.Models;
using RoguelikeWFC.MetaMap;

namespace RoguelikeWFC.MapIO;

public class MapSaver : IDisposable, IAsyncDisposable
{
    private XmlSerializer _serializer;
    private StreamWriter _writer;
    private readonly SerializebleWorldMap _worldMap;

    public MapSaver(WorldMap map, string filePath)
    {
        _serializer = new(typeof(SerializebleWorldMap));
        _worldMap = GetSerializebleWorldMap(map);
        _writer = File.CreateText(filePath);
    }
    
    private SerializebleWorldMap GetSerializebleWorldMap(WorldMap map)
    {
        byte[] tiles = new byte[map.Width * map.Height];
        int tileIndex = 0;
        foreach (MapTile mapTile in map.tiles)
        {
            tiles[tileIndex] = mapTile.Id;
            tileIndex++;
        }
        return new()
        {
            version = MetaMapRecognize.META_MAP_VERSION,
            width = map.Width,
            height = map.Height,
            atlasId = map.AtlasId,
            tiles = tiles
        };
    }
    
    public void Save()
    {
        _serializer.Serialize(_writer, _worldMap);
    }

    public void Dispose()
    {
        _writer.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _writer.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}