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
        AllCreatures.Add(New Creature(Identifier, mX, m_y, x, y))
        Dim WT = AllCreatureTypes(Identifier).DefaultWeaponType
        AllCreatures(I).Place()
        Return I
    End Function
End Class
