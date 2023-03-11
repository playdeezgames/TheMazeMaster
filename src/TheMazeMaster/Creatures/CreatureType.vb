Public Class CreatureType
    Public Sub New(
                  tileIndex As Integer,
                  hitPoints As Integer,
                  Optional minimumExitCount As Integer = 1,
                  Optional maximumExitCount As Integer = 4,
                  Optional spawnCount As Integer = 0,
                  Optional defaultWeaponType As String = ITEMTYPE_NONE)
        Me.TileIndex = tileIndex
        Me.MinimumExitCount = minimumExitCount
        Me.MaximumExitCount = maximumExitCount
        Me.SpawnCount = spawnCount
        Me.DefaultWeaponType = defaultWeaponType
    End Sub
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property MinimumExitCount As Integer
    Public ReadOnly Property MaximumExitCount As Integer
    Public ReadOnly Property SpawnCount As Integer
    Public ReadOnly Property DefaultWeaponType As String
    Public ReadOnly Property HitPoints As Integer
End Class
