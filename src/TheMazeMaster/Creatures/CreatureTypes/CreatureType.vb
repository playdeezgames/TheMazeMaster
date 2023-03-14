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
        Me.HitPoints = hitPoints
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
    Public ReadOnly Property HitPoints As Integer
    Public ReadOnly Property MinimumX As Integer
    Public ReadOnly Property MinimumY As Integer
    Public ReadOnly Property MaximumX As Integer
    Public ReadOnly Property MaximumY As Integer
    Public ReadOnly Property XP As Integer
    Public ReadOnly Property Drop As ItemTypeIdentifier
    Function Create(mX As Integer, m_y As Integer, x As Integer, y As Integer) As Integer
        'TODO: REUSE DEAD CREATURES WHEN POSSIBLE
        Dim I = AllCreatures.Count
        AllCreatures.Add(New Creature(I, Identifier, mX, m_y, x, y))
        Dim WT = AllCreatureTypes(Identifier).DefaultWeaponType
        AllCreatures(I).Place()
        Return I
    End Function
    Function Generate() As Integer
        Dim L = MinimumExitCount
        Dim H = MaximumExitCount
        Dim LX = MinimumX
        Dim LY = MinimumY
        Dim HX = MaximumX
        Dim HY = MaximumY
        Dim e As Integer
        Dim x As Integer
        Dim y As Integer
        Dim mx As Integer
        Dim m_y As Integer
        Do
            mx = Rnd(0, MAZE_COLUMNS - 1)
            m_y = Rnd(0, MAZE_ROWS - 1)
            e = GET_MAZE_CELL_EXITS(mx, m_y)
            Dim ti As Integer
            Dim cti As Integer
            Do
                x = Rnd(0, ROOM_COLUMNS - 1)
                y = Rnd(0, ROOM_ROWS - 1)
                Dim RM = GET_ROOM_MAP_ASSET(mx, m_y)
                ti = MGET(RM, 1, x, y)
                cti = MGET(RM, 2, x, y)
            Loop Until ti = TILE_FLOOR And cti = TILE_EMPTY And x >= LX And x <= HX And y >= LY And y <= HY
        Loop Until e >= L And e <= H
        Return Create(mx, m_y, x, y)
    End Function
End Class
