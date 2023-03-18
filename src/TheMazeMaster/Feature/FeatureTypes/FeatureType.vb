Friend Class FeatureType
    Friend ReadOnly SpawnCount As Integer
    Friend ReadOnly SpawnRoomType As RoomType
    Friend ReadOnly SpawnTerrainIdentifier As TerrainIdentifier
    ReadOnly Property Identifier As FeatureTypeIdentifier
    Sub New(
           identifier As FeatureTypeIdentifier,
           spawnRoomType As RoomType,
           spawnTerrainIdentifier As TerrainIdentifier,
           Optional spawnCount As Integer = 0)
        Me.Identifier = identifier
        Me.SpawnCount = spawnCount
        Me.SpawnRoomType = spawnRoomType
        Me.SpawnTerrainIdentifier = spawnTerrainIdentifier
    End Sub

    Friend Sub Generate()
        Dim world = Worlds.world
        Dim rooms = world.GetRoomsOfType(SpawnRoomType)
        Dim room = Rnd(rooms.ToList())
        Dim roomColumn As Integer
        Dim roomRow As Integer
        Do
            roomColumn = Rnd(0, room.Map.Columns - 1)
            roomRow = Rnd(0, room.Map.Rows - 1)
        Loop Until room.Map.GetCell(roomColumn, roomRow).Terrain = SpawnTerrainIdentifier
        room.Map.GetCell(roomColumn, roomRow).FeatureIndex = world.AddFeature(Identifier)
    End Sub
End Class
