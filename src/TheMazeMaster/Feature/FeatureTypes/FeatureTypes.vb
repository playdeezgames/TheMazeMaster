Friend Module FeatureTypes
    Friend ReadOnly AllFeatureTypes As IReadOnlyDictionary(Of FeatureTypeIdentifier, FeatureType) =
        New List(Of FeatureType) From
        {
            New FeatureType(
                FeatureTypeIdentifier.StairsDown,
                RoomType.Town,
                TerrainIdentifier.GRASS,
                TILE_STAIRS_DOWN,
                spawnCount:=1),
            New FeatureType(
                FeatureTypeIdentifier.Knacker,
                RoomType.Town,
                TerrainIdentifier.GRASS,
                TILE_KNACKER,
                spawnCount:=1,
                shoppeType:=ShoppeTypeIdentifier.Knacker),
            New FeatureType(
                FeatureTypeIdentifier.NSDoor,
                RoomType.Chamber,
                TerrainIdentifier.FLOOR,
                TILE_NSDOOR),
            New FeatureType(
                FeatureTypeIdentifier.EWDoor,
                RoomType.Chamber,
                TerrainIdentifier.FLOOR,
                TILE_EWDOOR),
            New FeatureType(
                FeatureTypeIdentifier.Chef,
                RoomType.Town,
                TerrainIdentifier.GRASS,
                TILE_CHEF,
                spawnCount:=1,
                shoppeType:=ShoppeTypeIdentifier.Chef),
            New FeatureType(
                FeatureTypeIdentifier.StairsUp,
                RoomType.Passageway,
                TerrainIdentifier.FLOOR,
                TILE_STAIRS_UP,
                spawnCount:=1)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module
