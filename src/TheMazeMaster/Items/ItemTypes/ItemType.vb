Friend Class ItemType
    Friend Sub New(
                  identifier As ItemTypeIdentifier,
                  name As String,
                  Optional tileIndex As Integer = TILE_EMPTY,
                  Optional stacks As Boolean = False,
                  Optional attackValue As Integer = 0,
                  Optional attackMaximum As Integer = 0,
                  Optional spawnCount As Integer = 0)
        Me.Identifier = identifier
        Me.Stacks = stacks
        Me.Name = name
        Me.TileIndex = tileIndex
        Me.AttackValue = attackValue
        Me.AttackMaximum = attackMaximum
        Me.SpawnCount = spawnCount
    End Sub
    Friend ReadOnly Property Identifier As ItemTypeIdentifier
    Friend ReadOnly Property Stacks As Boolean
    Public ReadOnly Property Name As String
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property AttackValue As Integer
    Public ReadOnly Property AttackMaximum As Integer
    Public ReadOnly Property SpawnCount As Integer

    Public Function RollAttack() As Integer
        Return ROLL_DICE(AttackValue, AttackMaximum)
    End Function
    Friend Function Create() As Integer
        'TODO: FIRST LOOK FOR EMPTY ITEM
        Dim I = AllItems.Count
        AllItems.Add(New Item(I, Identifier))
        Return I
    End Function
    Friend Function CreateInRoom(mazeColumn As Integer, mazeRow As Integer, roomColumn As Integer, roomRow As Integer) As Integer
        Dim i = Create()
        AllItems(i).MazeColumn = mazeColumn
        AllItems(i).MazeRow = mazeRow
        AllItems(i).RoomColumn = roomColumn
        AllItems(i).RoomRow = roomRow
        Return i
    End Function

    Friend Sub Generate()
        Dim mazeColumn As Integer
        Dim mazeRow As Integer
        Dim roomColumn As Integer
        Dim roomRow As Integer
        Do
            mazeColumn = Rnd(0, MAZE_COLUMNS - 1)
            mazeRow = Rnd(0, MAZE_ROWS - 1)
            roomColumn = Rnd(0, ROOM_COLUMNS - 1)
            roomRow = Rnd(0, ROOM_ROWS - 1)
        Loop Until Worlds.world.GetRoom(mazeColumn, mazeRow).Map.GetCell(roomColumn, roomRow).CanSpawn
        Dim itemIndex = CreateInRoom(mazeColumn, mazeRow, roomColumn, roomRow)
        AllItems(itemIndex).Place()
    End Sub
End Class
