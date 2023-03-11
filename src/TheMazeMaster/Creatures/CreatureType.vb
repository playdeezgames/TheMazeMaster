Public Class CreatureType
    Public Sub New(
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
                  Optional defaultWeaponType As String = ITEMTYPE_NONE,
                  Optional xp As Integer = 0,
                  Optional drop As String = ITEMTYPE_NONE)
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
    Public ReadOnly Name As String
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property MinimumExitCount As Integer
    Public ReadOnly Property MaximumExitCount As Integer
    Public ReadOnly Property SpawnCount As Integer
    Public ReadOnly Property DefaultWeaponType As String
    Public ReadOnly Property HitPoints As Integer
    Public ReadOnly Property MinimumX As Integer
    Public ReadOnly Property MinimumY As Integer
    Public ReadOnly Property MaximumX As Integer
    Public ReadOnly Property MaximumY As Integer
    Public ReadOnly Property XP As Integer
    Public ReadOnly Property Drop As String
End Class
