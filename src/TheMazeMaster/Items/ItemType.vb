Friend Class ItemType
    Friend Sub New(
                  name As String,
                  Optional tileIndex As Integer = TILE_EMPTY,
                  Optional stacks As Boolean = False)
        Me.Stacks = stacks
        Me.Name = name
        Me.TileIndex = tileIndex
    End Sub
    Friend ReadOnly Property Stacks As Boolean
    Public ReadOnly Property Name As String
    Public ReadOnly Property TileIndex As Integer
End Class
