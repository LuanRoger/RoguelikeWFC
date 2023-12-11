using RoguelikeWFC.MapIO;
using RoguelikeWFC.MapIO.Models;
using RoguelikeWFC.MetaMap.Exceptions;
using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC.MetaMap;

public class MetaMapRecoginizer
{
    public const string META_MAP_FILE_EXTENSION = ".wmap";
    public const int META_MAP_VERSION = 1;
    private readonly SerializebleWorldMap _serializebleWorldMap;

    public MetaMapRecoginizer(SerializebleWorldMap serializebleWorldMap)
    {
        _serializebleWorldMap = serializebleWorldMap;
    }
    
    public WorldMap Recognize()
    {
        if(_serializebleWorldMap.version != META_MAP_VERSION)
            throw new IncompatibleMapVersionException();
        
        TileAtlas tileAtlas = GetTileAtlas();
        var mapTile = GetMapTiles(tileAtlas);
        WorldMap map = new(_serializebleWorldMap.width, _serializebleWorldMap.height, mapTile, tileAtlas.AtlasId);
        return map;
    }
    
    private TileAtlas GetTileAtlas() =>
        _serializebleWorldMap.atlasId switch
        {
            1 => PlainsTiles.Instance,
            _ => throw new IncompatibleMapVersionException()
        };
    private MapTile[,] GetMapTiles(TileAtlas tileAtlas)
    {
        var tiles = new MapTile[_serializebleWorldMap.height, _serializebleWorldMap.width];
        for (int row = 0; row < _serializebleWorldMap.height; row++)
        {
            for (int col = 0; col < _serializebleWorldMap.width; col++)
            {
                byte tileId = _serializebleWorldMap.tiles[row * _serializebleWorldMap.width + col];
                tiles[row, col] = tileAtlas.GetAtlasTileById(tileId);
            }
        }
        return tiles;
    }
}