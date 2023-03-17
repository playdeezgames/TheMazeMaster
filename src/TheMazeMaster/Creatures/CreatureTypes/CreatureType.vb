Friend Class CreatureType
    Public Sub New(
                  identifier As CreatureTypeIdentifier,
                  name As String,
                  tileIndex As Integer,
                  hitPoints As Integer,
                  Optional spawnCount As Integer = 0,
                  Optional minimumExitCount As Integer = 1,
                  Optional maximumExitCount As Integer = 4,
                  Optional minimumX As Integer = 1,
                  Optional minimumY As Integer = 1,
                  Optional maximumX As Integer = 13,
                  Optional maximumY As Integer = 13,
                  Optional defaultWeaponType As ItemTypeIdentifier = ItemTypeIdentifier.None,
                  Optional xp As Integer = 0,
                  Optional drop As ItemTypeIdentifier = ItemTypeIdentifier.None)
        Me.Identifier = identifier
        Me.Name = name
        Me.TileIndex = tileIndex
        Me.MinimumExitCount = minimumExitCount
        Me.MaximumExitCount = maximumExitCount
        Me.SpawnCount = spawnCount
        Me.DefaultWeaponType = defaultWeaponType
        Me.MinimumX = minimumX
        Me.MaximumX = maximumX
        Me.MinimumY = minimumY
        Me.MaximumY = maximumY
        Me.MaximumHitPoints = hitPoints
        Me.XP = xp
        Me.Drop = drop
    End Sub

    Public ReadOnly Property Identifier As CreatureTypeIdentifier
    Public ReadOnly Name As String
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property MinimumExitCount As Integer
    Public ReadOnly Property MaximumExitCount As Integer
    Public ReadOnly Property SpawnCount As Integer
    Public ReadOnly Property DefaultWeaponType As ItemTypeIdentifier
    Public ReadOnly Property MaximumHitPoints As Integer
    Public ReadOnly Property MinimumX As Integer
    Public ReadOnly Property MinimumY As Integer
    Public ReadOnly Property MaximumX As Integer
    Public ReadOnly Property MaximumY As Integer
    Public ReadOnly Property XP As Integer
    Public ReadOnly Property Drop As ItemTypeIdentifier
    Function Generate(maze As Maze) As Integer
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
                cell = Worlds.world.GetRoom(mazeColumn, mazeRow).Map.GetCell(roomColumn, roomRow)
            Loop Until cell.CanSpawn AndAlso roomColumn >= MinimumX AndAlso roomColumn <= MaximumX AndAlso roomRow >= MinimumY AndAlso roomRow <= MaximumY
        Loop Until exitCount >= MinimumExitCount AndAlso exitCount <= MaximumExitCount
        Return Worlds.world.AddCreature(Identifier, mazeColumn, mazeRow, roomColumn, roomRow)
    End Function
End Class
