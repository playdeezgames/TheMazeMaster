Friend Class FeatureType
    Friend ReadOnly SpawnCount As Integer
    Friend ReadOnly SpawnRoomType As RoomType
    Friend ReadOnly SpawnTerrainIdentifier As TerrainIdentifier
    Friend ReadOnly TileIndex As Integer
    Friend ReadOnly ShoppeType As ShoppeTypeIdentifier?
    ReadOnly Property Identifier As FeatureTypeIdentifier
    Sub New(
           identifier As FeatureTypeIdentifier,
           spawnRoomType As RoomType,
           spawnTerrainIdentifier As TerrainIdentifier,
           tileIndex As Integer,
           Optional spawnCount As Integer = 0,
           Optional shoppeType As ShoppeTypeIdentifier = Nothing)
        Me.Identifier = identifier
        Me.SpawnCount = spawnCount
        Me.SpawnRoomType = spawnRoomType
        Me.SpawnTerrainIdentifier = spawnTerrainIdentifier
        Me.TileIndex = tileIndex
        Me.ShoppeType = shoppeType
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
        Loop Until room.Map.GetCell(roomColumn, roomRow).CanSpawn AndAlso room.Map.GetCell(roomColumn, roomRow).Terrain = SpawnTerrainIdentifier
        room.Map.GetCell(roomColumn, roomRow).FeatureIndex = world.AddFeature(Identifier, room.MazeColumn, room.MazeRow, roomColumn, roomRow)
    End Sub
End Class
