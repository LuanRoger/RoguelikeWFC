# RoguelikeWFC

A demo of a variant of the [Wave Function Collapse](https://github.com/mxgmn/WaveFunctionCollapse) algorithm based on rules. As the name suggests, it is inspired by the WFC but instead of using a bitmap as input, it uses a set of rules, which are applied to the tiles on an atlas. That way, the user can have more control over the output.

![](https://github.com/LuanRoger/RoguelikeWFC/blob/main/images/demo.gif)

## The rules

The tile's rules are simple, they define which tile can be placed on side of another tile. For example, the following tile define a `TileSocket` that contains the tiles it can be placed next to:

```csharp
public record GrassTile() : RenderableMapTile(TileIDs.GRASS, [249, 250],
    new([TileIDs.GRASS, TileIDs.TREE, TileIDs.MOUNTAIN, TileIDs.SAND]))
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Green);
}
```

*See the implementation of the `RenderableMapTile` class for more details.*

The rules:
`[TileIDs.GRASS, TileIDs.TREE, TileIDs.MOUNTAIN, TileIDs.SAND]` define that the `GrassTile` can be generate next to a `TreeTile`, a `MountainTile` or a `SandTile`.

Beside this rule, the `IsolationGroup` can also be defined, it is used to define if this tile can be isolated or can be next to another tile of the same type. For example, the `MountainTile` is defined as follow:

```csharp
public record MountainTile() : RenderableMapTile(TileIDs.MOUNTAIN, 30,
    new([ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK, TileIDs.GRASS, TileIDs.SAND ]),
    isolationGroup: [ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK ])
{
    public override TileSpriteMetadata SpriteMetadata { get; } = new(Color.Gray);
}
```

By the rules, the `MountainTile` can be generate from a `GrassTile` and it can generate a `GrassTile` as well, this create a chance to have a `MountainTile` isolated from other `MountainTile`, if you don't want that this happen, you can define the `IsolationGroup`, so at the end, the `MountainTile` can only be next to another `MountainTile` or a `MountainPeekTile` as defined in `isolationGroup: [ TileIDs.MOUNTAIN, TileIDs.MOUNTAIN_PEEK ]`.

This rule will be applied during the post-generation step, so the `MountainTile` will be generated from a `GrassTile` and then, the `IsolationGroup` will be applied to the generated `MountainTile`.

The other parameter is domain specific (e.g. `RenderableMapTile` that inherits from `MapTile`), it is used to define the sprite of the tile.

## The algorithm

### Implementation details

The algorithm it's implemented in the `WorldGenerator` class, which receives a widht and a height (defining the Wave dimensions) and a `TileAtlas` containing all the tiles that can be used to generate the world, after that, you can call `Wfc(WfcCallKind.Complete)` to start a complete generation (it suport execute a single interation).

When the `WorldGenerator` is created, it will create a `Wave` with the given dimensions and it will initialize it with the tiles from the `TileAtlas`, but at the start, each tile will be a `WavePossition` with the `Entropy` == `TileAtlas.ValidInitialTiles()`, so every possition can be any initial tile.

### Steps

The algorithm is divided in 3 steps:

1. Get the tile with the smaller entropy, pick a random valid tile and collapse into the possition.
2. Propagate the state through the entire wave:
    - For each non collapsed `WavePossition` `wp` on wave:
        1. Get the adjacents `WavePossition` (top, right, bottom, left) and update the `Entropy` of them by the intersection of the `MapTile`'s `TileSocket.fit<side>` at the `wp` possition and the `Entropy` of the adjacent possition.
        2. If the new `Entropy` is empty so it consider on conflict.
3. Run the post-generation step only if the wave contains only non collapsed conflict tiles:
    - While have conflicts and non solved isolations:
        1. For each `WavePossition` `wp` on wave:
            1. If `wp` is on conflict, get the adjacents `WavePossition` (top, right, bottom, left) and update the `Entropy` of it by the `ValidInitialTiles()` from atlas.
            2. If `wp` is on isolation (`isolationGroup` is not empty and it is not next to any of the tiles on the `isolationGroup`), get the adjacents `WavePossition` (top, right, bottom, left) and update the `Entropy` of it by the `ValidInitialTiles()` from atlas.
        2. Repeat the step 2.

## World map

At the end of the generation (when it's all clean), the `WorldGenerator` can generate a `WorldMap` from the `Wave` by calling getting the `worldMap` property, it will create a `WorldMap` with the same dimensions of the `Wave` and it will fill it with the tiles from the `Wave` (the tiles are picked randomly from the `WavePossition`'s `Entropy`).
