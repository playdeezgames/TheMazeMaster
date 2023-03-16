Friend Class ItemType
    Friend Sub New(
                  identifier As ItemTypeIdentifier,
                  name As String,
                  Optional tileIndex As Integer = TILE_EMPTY,
                  Optional stacks As Boolean = False,
                  Optional attackValue As Integer = 0,
                  Optional attackMaximum As Integer = 0,
                  Optional spawnCount As Integer = 0,
                  Optional minimumExitCount As Integer = 1,
                  Optional maximumExitCount As Integer = 4,
                  Optional minimumX As Integer = 1,
                  Optional maximumX As Integer = ROOM_COLUMNS - 2,
                  Optional minimumY As Integer = 1,
                  Optional maximumY As Integer = ROOM_ROWS - 2)
        Me.Identifier = identifier
        Me.Stacks = stacks
        Me.Name = name
        Me.TileIndex = tileIndex
        Me.AttackValue = attackValue
        Me.AttackMaximum = attackMaximum
        Me.SpawnCount = spawnCount
        Me.MinimumExitCount = minimumExitCount
        Me.MaximumExitCount = maximumExitCount
        Me.MaximumX = maximumX
        Me.MaximumY = maximumY
        Me.MinimumX = minimumX
        Me.MinimumY = minimumY
    End Sub
    Friend ReadOnly Property Identifier As ItemTypeIdentifier
    Friend ReadOnly Property Stacks As Boolean
    Public ReadOnly Property Name As String
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property AttackValue As Integer
    Public ReadOnly Property AttackMaximum As Integer
    Public ReadOnly Property SpawnCount As Integer
    Public ReadOnly Property MinimumExitCount As Integer
    Public ReadOnly Property MaximumExitCount As Integer
    Public ReadOnly Property MaximumX As Integer
    Public ReadOnly Property MaximumY As Integer
    Public ReadOnly Property MinimumX As Integer
    Public ReadOnly Property MinimumY As Integer

    Public Function RollAttack() As Integer
        Return ROLL_DICE(AttackValue, AttackMaximum)
    End Function
    Friend Function Create() As Integer
        Return Worlds.world.AddItem(Identifier)
    End Function
    Friend Function CreateInRoom(mazeColumn As Integer, mazeRow As Integer, roomColumn As Integer, roomRow As Integer) As Integer
        Dim i = Create()
        Worlds.world.GetItem(i).MazeColumn = mazeColumn
        Worlds.world.GetItem(i).MazeRow = mazeRow
        Worlds.world.GetItem(i).RoomColumn = roomColumn
        Worlds.world.GetItem(i).RoomRow = roomRow
        Return i
    End Function

    Friend Sub Generate(maze As Maze)
        Dim mazeColumn As Integer
        Dim mazeRow As Integer
        Dim roomColumn As Integer
        Dim roomRow As Integer
        Dim e As Integer
        Do
            mazeColumn = Rnd(0, MAZE_COLUMNS - 1)
            mazeRow = Rnd(0, MAZE_ROWS - 1)
            e = maze.GetCell(mazeColumn, mazeRow).ExitCount
            roomColumn = Rnd(MinimumX, MaximumX)
            roomRow = Rnd(MinimumY, MaximumY)
        Loop Until e >= MinimumExitCount AndAlso e <= MaximumExitCount AndAlso Worlds.world.GetRoom(mazeColumn, mazeRow).Map.GetCell(roomColumn, roomRow).CanSpawn
        Dim itemIndex = CreateInRoom(mazeColumn, mazeRow, roomColumn, roomRow)
        Worlds.world.GetItem(itemIndex).Place()
    End Sub
End Class
