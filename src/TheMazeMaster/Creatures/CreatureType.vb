Public Class CreatureType
    Public Sub New(
                  tileIndex As Integer,
                  Optional minimumExitCount As Integer = 1,
                  Optional maximumExitCount As Integer = 4)
        Me.TileIndex = tileIndex
        Me.MinimumExitCount = minimumExitCount
        Me.MaximumExitCount = maximumExitCount
    End Sub
    Public ReadOnly Property TileIndex As Integer
    Public ReadOnly Property MinimumExitCount As Integer
    Public ReadOnly Property MaximumExitCount As Integer
End Class
