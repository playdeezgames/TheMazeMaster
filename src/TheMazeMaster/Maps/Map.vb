Friend Class MapCell
    Property Terrain As TerrainIdentifier
    Property Creature As Integer?
    Property Item As Integer?

    Friend ReadOnly Property CanSpawn As Boolean
        Get
            Return Not Creature.HasValue AndAlso Not Item.HasValue AndAlso AllTerrains(Terrain).CanWalk
        End Get
    End Property
End Class
Friend Class Map
    Friend ReadOnly Property Columns As Integer
    Friend ReadOnly Property Rows As Integer
    Private Cells As List(Of MapCell)
    Sub New(columns As Integer, rows As Integer)
        Me.Columns = columns
        Me.Rows = rows
        Cells = New List(Of MapCell)
        While Cells.Count < columns * rows
            Cells.Add(New MapCell)
        End While
    End Sub
    Function GetCell(column As Integer, row As Integer) As MapCell
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return Cells(column + row * Columns)
    End Function
End Class
