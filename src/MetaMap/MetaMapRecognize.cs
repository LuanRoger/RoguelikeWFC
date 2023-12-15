using RoguelikeWFC.MapIO;
using RoguelikeWFC.MapIO.Models;
using RoguelikeWFC.MetaMap.Exceptions;
using RoguelikeWFC.Tiles;
using RoguelikeWFC.WFC;

namespace RoguelikeWFC.MetaMap;

public class MetaMapRecognize(SerializebleWorldMap serializebleWorldMap)
{
    public const string META_MAP_FILE_EXTENSION = ".wmap";
    public const int META_MAP_VERSION = 1;

    public WorldMap Recognize()
    {
        if(serializebleWorldMap.version != META_MAP_VERSION)
            throw new IncompatibleMapVersionException();
        
        TileAtlas tileAtlas = GetTileAtlas();
        var mapTile = GetMapTiles(tileAtlas);
        WorldMap map = new(serializebleWorldMap.width, serializebleWorldMap.height, mapTile, tileAtlas.AtlasId);
        return map;
    }
    
    private TileAtlas GetTileAtlas() =>
        serializebleWorldMap.atlasId switch
        {
            1 => PlainsTiles.Instance,
            2 => DesertTiles.Instance,
            _ => throw new IncompatibleMapVersionException()
        };
    private MapTile[,] GetMapTiles(TileAtlas tileAtlas)
    {
        var tiles = new MapTile[serializebleWorldMap.height, serializebleWorldMap.width];
        for (int row = 0; row < serializebleWorldMap.height; row++)
        {
            for (int col = 0; col < serializebleWorldMap.width; col++)
            {
                byte tileId = serializebleWorldMap.tiles[row * serializebleWorldMap.width + col];
                tiles[row, col] = tileAtlas.GetAtlasTileById(tileId);
            }
        }
        return tiles;
    }
}