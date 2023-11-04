using RoguelikeWFC.Tiles;

namespace RoguelikeWFC.WFC;

public class BitMap
{
    public int width { get; }
    public int height { get; }
    private readonly TileSet _tileSet;
    private MapTile[] tiles => _tileSet.Tiles;
    public Wave wave { get; }
    private readonly MapTile _nullTile = new NullTile();
    
    public bool initialized { get; private set; }

    public BitMap(int width, int height, TileSet tileSet)
    {
        this.width = width;
        this.height = height;
        wave = new(width, height);
        _tileSet = tileSet;
    }
    
    public void Init()
    {
        foreach (WavePossition wavePossition in wave.wave)
            wavePossition.entropy = _tileSet.ValidInitialTiles();
        initialized = true;
    }
    
    public byte[] ValidInitialTiles() => 
        _tileSet.ValidInitialTiles();
    
    public WavePossition GetSmallerEntropyPossition()
    {
        WavePossition smallerEntropy = wave.wave[0, 0];
        int smallerEntropyLength = int.MaxValue;
        foreach (WavePossition wavePossition in wave.wave)
        {
            if (wavePossition.collapsed ||
                wavePossition.entropy.Length >= smallerEntropyLength) 
                continue;
            
            smallerEntropy = wavePossition;
            smallerEntropyLength = wavePossition.entropy.Length;
        }

        return smallerEntropy;
    }

    public byte GetRandomTileFromPossition(WavePossition possition)
    {
        Random rng = new();
        int tileIndex = rng.Next(0, possition.entropy.Length);
        return possition.entropy[tileIndex];
    }
    
    public MapTile? GetTileById(int id)
    {
        foreach (MapTile tile in tiles)
        {
            if(tile.id == id) 
                return tile;
        }
        
        return null;
    }
    
    public MapTile GetTileAtPossition(int rowIndex, int columnIndex)
    {
        WavePossition possition = wave.wave[rowIndex, columnIndex];
        if(!possition.collapsed)
            return new TextTile(possition.entropy.Length.ToString()[0]);
        
        int tileId = possition.entropy[0];
        return GetTileById(tileId) ?? _nullTile;
    }
}