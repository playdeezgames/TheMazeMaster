﻿Friend Module Terrains
    Friend ReadOnly AllTerrains As IReadOnlyDictionary(Of TerrainIdentifier, Terrain) =
        New List(Of Terrain) From
        {
            New Terrain(TerrainIdentifier.BLOOD, TILE_BLOOD, canWalk:=True),
            New Terrain(TerrainIdentifier.WALL_EAST, TILE_WALL_EAST),
            New Terrain(TerrainIdentifier.WALL_WEST, TILE_WALL_WEST),
            New Terrain(TerrainIdentifier.WALL_NORTH, TILE_WALL_NORTH),
            New Terrain(TerrainIdentifier.WALL_SOUTH, TILE_WALL_SOUTH),
            New Terrain(TerrainIdentifier.DOOR_EAST, TILE_DOOR_EAST, canWalk:=True),
            New Terrain(TerrainIdentifier.DOOR_WEST, TILE_DOOR_WEST, canWalk:=True),
            New Terrain(TerrainIdentifier.DOOR_NORTH, TILE_DOOR_NORTH, canWalk:=True),
            New Terrain(TerrainIdentifier.DOOR_SOUTH, TILE_DOOR_SOUTH, canWalk:=True),
            New Terrain(TerrainIdentifier.WALL_CORNER_INSIDE_NORTHEAST, TILE_CORNER_INSIDE_NORTHEAST),
            New Terrain(TerrainIdentifier.WALL_CORNER_INSIDE_NORTHWEST, TILE_CORNER_INSIDE_NORTHWEST),
            New Terrain(TerrainIdentifier.WALL_CORNER_INSIDE_SOUTHEAST, TILE_CORNER_INSIDE_SOUTHEAST),
            New Terrain(TerrainIdentifier.WALL_CORNER_INSIDE_SOUTHWEST, TILE_CORNER_INSIDE_SOUTHWEST),
            New Terrain(TerrainIdentifier.WALL_CORNER_OUTSIDE_NORTHEAST, TILE_CORNER_OUTSIDE_NORTHEAST),
            New Terrain(TerrainIdentifier.WALL_CORNER_OUTSIDE_NORTHWEST, TILE_CORNER_OUTSIDE_NORTHWEST),
            New Terrain(TerrainIdentifier.WALL_CORNER_OUTSIDE_SOUTHEAST, TILE_CORNER_OUTSIDE_SOUTHEAST),
            New Terrain(TerrainIdentifier.WALL_CORNER_OUTSIDE_SOUTHWEST, TILE_CORNER_OUTSIDE_SOUTHWEST),
            New Terrain(TerrainIdentifier.FLOOR, TILE_FLOOR, canWalk:=True),
            New Terrain(TerrainIdentifier.SOLID, TILE_SOLID),
            New Terrain(TerrainIdentifier.GRASS, TILE_GRASS, canWalk:=True),
            New Terrain(TerrainIdentifier.FENCE_NORTH, TILE_FENCE_NORTH),
            New Terrain(TerrainIdentifier.FENCE_EAST, TILE_FENCE_EAST),
            New Terrain(TerrainIdentifier.FENCE_SOUTH, TILE_FENCE_SOUTH),
            New Terrain(TerrainIdentifier.FENCE_WEST, TILE_FENCE_WEST),
            New Terrain(TerrainIdentifier.FENCE_CORNER_INSIDE_NORTHEAST, TILE_FENCE_CORNER_INSIDE_NORTHEAST),
            New Terrain(TerrainIdentifier.FENCE_CORNER_INSIDE_NORTHWEST, TILE_FENCE_CORNER_INSIDE_NORTHWEST),
            New Terrain(TerrainIdentifier.FENCE_CORNER_INSIDE_SOUTHEAST, TILE_FENCE_CORNER_INSIDE_SOUTHEAST),
            New Terrain(TerrainIdentifier.FENCE_CORNER_INSIDE_SOUTHWEST, TILE_FENCE_CORNER_INSIDE_SOUTHWEST)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module
