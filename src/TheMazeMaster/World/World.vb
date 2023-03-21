Friend Class World
    Private rooms As List(Of Room)
    Private items As List(Of Item)
    Private creatures As List(Of Creature)
    Private features As List(Of Feature)
    Friend character As Character
    Function GetCreature(i As Integer) As Creature
        Return creatures(i)
    End Function
    Function GetItem(i As Integer) As Item
        Return items(i)
    End Function
    Friend Sub Start()
        Dim maze As New Maze(MAZE_COLUMNS, MAZE_ROWS)
        maze.Generate()
        GenerateRooms(maze)
        GenerateFeatures()
        Dim doorCount = GenerateDoors(maze)
        GenerateItems(maze)
        GenerateKeys(doorCount, maze)
        GenerateCreatures(maze)
        GeneratePlayer(maze)
    End Sub

    Private Sub GenerateKeys(doorCount As Integer, maze As Maze)
        While doorCount > 0
            AllItemTypes(ItemTypeIdentifier.Key).Generate(Me, maze)
            doorCount -= 1
        End While
    End Sub

    Private doorFeatureTypeIdentifiers As IReadOnlyDictionary(Of DirectionIdentifier, FeatureTypeIdentifier) =
        New Dictionary(Of DirectionIdentifier, FeatureTypeIdentifier) From
        {
            {DirectionIdentifier.North, FeatureTypeIdentifier.NSDoor},
            {DirectionIdentifier.East, FeatureTypeIdentifier.EWDoor},
            {DirectionIdentifier.South, FeatureTypeIdentifier.NSDoor},
            {DirectionIdentifier.West, FeatureTypeIdentifier.EWDoor}
        }
    Dim doorRoomColumns As IReadOnlyDictionary(Of DirectionIdentifier, Integer) =
        New Dictionary(Of DirectionIdentifier, Integer) From
        {
            {DirectionIdentifier.North, ROOM_COLUMNS \ 2},
            {DirectionIdentifier.East, ROOM_COLUMNS - 1},
            {DirectionIdentifier.South, ROOM_COLUMNS \ 2},
            {DirectionIdentifier.West, 0}
        }
    Dim doorRoomRows As IReadOnlyDictionary(Of DirectionIdentifier, Integer) =
        New Dictionary(Of DirectionIdentifier, Integer) From
        {
            {DirectionIdentifier.North, 0},
            {DirectionIdentifier.East, ROOM_ROWS \ 2},
            {DirectionIdentifier.South, ROOM_ROWS - 1},
            {DirectionIdentifier.West, ROOM_ROWS \ 2}
        }

    Private Function GenerateDoors(maze As Maze) As Integer
        Dim result = 0
        For mazeColumn = 0 To maze.Columns - 1
            For mazeRow = 0 To maze.Rows - 1
                Dim cell = maze.GetCell(mazeColumn, mazeRow)
                If cell.ExitCount = 1 Then
                    Dim direction = cell.AllDoors.Single
                    Dim nextColumn = direction.StepX(mazeColumn)
                    Dim nextRow = direction.StepY(mazeRow)
                    Dim opposite = direction.Opposite
                    Dim index = features.Count
                    Dim roomColumn = doorRoomColumns(opposite)
                    Dim roomRow = doorRoomRows(opposite)
                    features.Add(New Feature(
                                 index,
                                 doorFeatureTypeIdentifiers(opposite),
                                 nextColumn,
                                 nextRow,
                                 roomColumn,
                                 roomRow))
                    GetRoom(nextColumn, nextRow).Map.GetCell(roomColumn, roomRow).FeatureIndex = index
                    result += 1
                End If
            Next
        Next
        Return result
    End Function

    Private Sub GenerateFeatures()
        features = New List(Of Feature)
        For Each entry In AllFeatureTypes
            Dim spawnCount = entry.Value.SpawnCount
            While spawnCount > 0
                entry.Value.Generate(Me)
                spawnCount -= 1
            End While
        Next
    End Sub
    Private Sub GenerateItems(maze As Maze)
        items = New List(Of Item)
        For Each entry In AllItemTypes
            Dim spawnCount = entry.Value.SpawnCount
            While spawnCount > 0
                entry.Value.Generate(Me, maze)
                spawnCount -= 1
            End While
        Next
    End Sub
    Friend Sub GenerateCreatures(maze As Maze)
        creatures = New List(Of Creature)
        For Each entry In AllCreatureTypes
            Dim SC = entry.Value.SpawnCount
            While SC > 0
                GenerateCreatureType(entry.Value, maze)
                SC -= 1
            End While
        Next
    End Sub
    Friend Function GetRoom(column As Integer?, row As Integer?) As Room
        If column.HasValue AndAlso row.HasValue Then
            Return rooms.Single(Function(x) If(x.MazeRow = row, False) AndAlso If(x.MazeColumn = column, False))
        End If
        Return rooms.Single(Function(x) Not x.MazeColumn.HasValue AndAlso Not x.MazeRow.HasValue)
    End Function
    Private Sub GenerateRooms(maze As Maze)
        rooms = New List(Of Room)
        rooms.Clear()
        Dim TEMP As Integer
        For ROW = 0 To MAZE_ROWS - 1
            For COLUMN = 0 To MAZE_COLUMNS - 1
                TEMP = 0
                For Each d In AllDirections
                    If maze.GetCell(COLUMN, ROW).HasDoor(d) Then
                        TEMP = TEMP Or (1 << d)
                    End If
                Next
                Dim EXIT_COUNT As Integer = maze.GetCell(COLUMN, ROW).ExitCount
                Dim IS_CHAMBER As Boolean = False
                If EXIT_COUNT = 1 Then
                    IS_CHAMBER = True
                ElseIf EXIT_COUNT = 2 Then
                    If Rnd(0, 99) < 50 Then
                        IS_CHAMBER = True
                    End If
                ElseIf EXIT_COUNT = 3 Then
                    If Rnd(0, 99) < 75 Then
                        IS_CHAMBER = True
                    End If
                Else
                    If Rnd(0, 99) < 25 Then
                        IS_CHAMBER = True
                    End If
                End If
                Dim ROOM_MAP As MapAssetData
                If IS_CHAMBER Then
                    ROOM_MAP = CLONE(CHAMBER_MAPS(TEMP))
                Else
                    ROOM_MAP = CLONE(PASSAGEWAY_MAPS(TEMP))
                End If
                rooms.Add(New Room(ROOM_MAP.ToMap, If(IS_CHAMBER, RoomType.Chamber, RoomType.Passageway), COLUMN, ROW))
            Next
        Next
        PlaceRoomDoors(maze)
        GenerateTown()
    End Sub
    Private Sub GenerateTown()
        Dim room = New Room(New Map(TOWN_COLUMNS, TOWN_ROWS), RoomType.Town, Nothing, Nothing)
        rooms.Add(room)
        For column = 0 To room.Map.Columns - 1
            For row = 0 To room.Map.Rows - 1
                room.Map.GetCell(column, row).Terrain = TerrainIdentifier.GRASS
            Next
        Next
        For column = 1 To room.Map.Columns - 2
            room.Map.GetCell(column, 0).Terrain = TerrainIdentifier.FENCE_NORTH
            room.Map.GetCell(column, room.Map.Rows - 1).Terrain = TerrainIdentifier.FENCE_SOUTH
        Next
        For row = 1 To room.Map.Rows - 2
            room.Map.GetCell(0, row).Terrain = TerrainIdentifier.FENCE_WEST
            room.Map.GetCell(room.Map.Columns - 1, row).Terrain = TerrainIdentifier.FENCE_EAST
        Next
        room.Map.GetCell(0, 0).Terrain = TerrainIdentifier.FENCE_CORNER_INSIDE_NORTHWEST
        room.Map.GetCell(room.Map.Columns - 1, 0).Terrain = TerrainIdentifier.FENCE_CORNER_INSIDE_NORTHEAST
        room.Map.GetCell(0, room.Map.Rows - 1).Terrain = TerrainIdentifier.FENCE_CORNER_INSIDE_SOUTHWEST
        room.Map.GetCell(room.Map.Columns - 1, room.Map.Rows - 1).Terrain = TerrainIdentifier.FENCE_CORNER_INSIDE_SOUTHEAST
    End Sub
    Private Sub PlaceRoomDoors(maze As Maze)
        For mazeColumn = 0 To MAZE_COLUMNS - 1
            For mazeRow = 0 To MAZE_ROWS - 1
                Dim exitCount = maze.GetCell(mazeColumn, mazeRow).ExitCount
                If exitCount = 1 Then
                    Dim direction As DirectionIdentifier = DirectionIdentifier.North
                    While Not maze.GetCell(mazeColumn, mazeRow).HasDoor(direction)
                        direction = direction.NextDirection
                    End While
                    Dim fromMap As MapAssetData

                    If GetRoom(mazeColumn, mazeRow).RoomType = RoomType.Chamber Then
                        fromMap = CHAMBERDOOR_MAPS(direction)
                    Else
                        fromMap = PASSAGEWAYDOOR_MAPS(direction)
                    End If
                    Dim toMap = GetRoom(mazeColumn, mazeRow).Map
                    BlitTerrain(fromMap.ToMap, toMap, TerrainIdentifier.EMPTY)

                    Dim nextX = direction.StepX(mazeColumn)
                    Dim nextY = direction.StepY(mazeRow)
                    direction = direction.Opposite
                    If GetRoom(nextX, nextY).RoomType = RoomType.Chamber Then
                        fromMap = CHAMBERDOOR_MAPS(direction)
                    Else
                        fromMap = PASSAGEWAYDOOR_MAPS(direction)
                    End If
                    toMap = GetRoom(nextX, nextY).Map
                    BlitTerrain(fromMap.ToMap, toMap, TerrainIdentifier.EMPTY)
                End If
            Next
        Next
    End Sub
    Private Shared Sub BlitTerrain(fromMap As Map, toMap As Map, transparent As TerrainIdentifier)
        For column = 0 To toMap.Columns - 1
            For row = 0 To toMap.Rows - 1
                Dim terrain = fromMap.GetCell(column, row).Terrain
                If terrain = transparent Then
                    Continue For
                End If
                toMap.GetCell(column, row).Terrain = terrain
            Next
        Next
    End Sub
    Friend Function AddItem(identifier As ItemTypeIdentifier) As Integer
        'TODO: FIRST LOOK FOR EMPTY ITEM
        Dim I = items.Count
        items.Add(New Item(I, identifier))
        Return I
    End Function
    Friend Function AddCreature(
                               identifier As CreatureTypeIdentifier,
                               mazeColumn As Integer,
                               mazeRow As Integer,
                               roomColumn As Integer,
                               roomRow As Integer) As Integer
        'TODO: REUSE DEAD CREATURES WHEN POSSIBLE
        Dim I = creatures.Count
        creatures.Add(New Creature(Me, I, identifier, mazeColumn, mazeRow, roomColumn, roomRow))
        Dim WT = AllCreatureTypes(identifier).DefaultWeaponType
        creatures(I).Place()
        Return I
    End Function
    Friend Sub GeneratePlayer(maze As Maze)
        character = New Character(Me, GenerateCreatureType(AllCreatureTypes(CreatureTypeIdentifier.Dude), maze))
        character.Creature.Remove()
        character.Creature.MoveToFeature(FeatureTypeIdentifier.StairsUp)
        character.Creature.Place()
    End Sub
    Function GenerateCreatureType(creatureType As CreatureType, maze As Maze) As Integer
        Dim exitCount As Integer
        Dim roomColumn As Integer
        Dim roomRow As Integer
        Dim mazeColumn As Integer
        Dim mazeRow As Integer
        Do
            mazeColumn = Rnd(0, MAZE_COLUMNS - 1)
            mazeRow = Rnd(0, MAZE_ROWS - 1)
            exitCount = maze.GetCell(mazeColumn, mazeRow).ExitCount
            Dim cell As MapCell
            Do
                roomColumn = Rnd(0, ROOM_COLUMNS - 1)
                roomRow = Rnd(0, ROOM_ROWS - 1)
                cell = GetRoom(mazeColumn, mazeRow).Map.GetCell(roomColumn, roomRow)
            Loop Until cell.CanSpawn AndAlso roomColumn >= creatureType.MinimumX AndAlso roomColumn <= creatureType.MaximumX AndAlso roomRow >= creatureType.MinimumY AndAlso roomRow <= creatureType.MaximumY
        Loop Until exitCount >= creatureType.MinimumExitCount AndAlso exitCount <= creatureType.MaximumExitCount
        Return AddCreature(creatureType.Identifier, mazeColumn, mazeRow, roomColumn, roomRow)
    End Function
    Friend Function GetRoomsOfType(roomType As RoomType) As IEnumerable(Of Room)
        Return rooms.Where(Function(x) x.RoomType = roomType)
    End Function
    Friend Function GetFeatureOfType(featureType As FeatureTypeIdentifier) As Feature
        Return features.SingleOrDefault(Function(x) x.FeatureTypeIdentifier = featureType)
    End Function
    Friend Function AddFeature(
                         identifier As FeatureTypeIdentifier,
                         mazeColumn As Integer?,
                         mazeRow As Integer?,
                         roomColumn As Integer,
                         roomRow As Integer) As Integer
        Dim I = features.Count
        features.Add(New Feature(I, identifier, mazeColumn, mazeRow, roomColumn, roomRow))
        Return I
    End Function
    Friend Function GetFeature(featureIndex As Integer) As Feature
        Return features(featureIndex)
    End Function
End Class
