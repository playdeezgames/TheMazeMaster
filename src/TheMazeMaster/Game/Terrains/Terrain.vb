Friend Class Terrain
    Friend Sub New(
                  identifier As TerrainIdentifier,
                  tileIndex As Integer,
                  Optional canWalk As Boolean = False)
        Me.Identifier = identifier
        Me.TileIndex = tileIndex
        Me.CanWalk = canWalk
    End Sub
    ReadOnly Property Identifier As TerrainIdentifier
    ReadOnly Property TileIndex As Integer
    ReadOnly Property CanWalk As Boolean
End Class
