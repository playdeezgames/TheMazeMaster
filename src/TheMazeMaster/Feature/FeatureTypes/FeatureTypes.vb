Friend Module FeatureTypes
    Friend ReadOnly AllFeatureTypes As IReadOnlyDictionary(Of FeatureTypeIdentifier, FeatureType) =
        New List(Of FeatureType) From
        {
            New FeatureType(
                FeatureTypeIdentifier.StairsDown,
                RoomType.Town,
                TerrainIdentifier.GRASS,
                spawnCount:=1),
            New FeatureType(
                FeatureTypeIdentifier.StairsUp,
                RoomType.Passageway,
                TerrainIdentifier.FLOOR,
                spawnCount:=1)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module
