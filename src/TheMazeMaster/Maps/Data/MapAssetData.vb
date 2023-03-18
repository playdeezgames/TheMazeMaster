Friend Class MapLayerAssetData
    <JsonPropertyName("width")>
    Public Property Width As Integer
    <JsonPropertyName("height")>
    Public Property Height As Integer
    <JsonPropertyName("depth")>
    Public Property Depth As Integer
    <JsonPropertyName("data")>
    Public Property Data As New List(Of Integer)
End Class
Friend Class LayerAssetData
    <JsonPropertyName("type")>
    Public Property Type As String
    <JsonPropertyName("overlap_x")>
    Public Property OverlapX As Integer
    <JsonPropertyName("overlap_y")>
    Public Property OverlapY As Integer
    <JsonPropertyName("data")>
    Public Property Data() As MapLayerAssetData
End Class
Friend Class MapAssetData
    <JsonPropertyName("layers")>
    Public Property Layers As New List(Of LayerAssetData)
    Private Shared ReadOnly terrainTable As IReadOnlyDictionary(Of Integer, TerrainIdentifier) =
        New Dictionary(Of Integer, TerrainIdentifier) From
        {
            {TILE_WALL_NORTH, TerrainIdentifier.WALL_NORTH},
            {TILE_WALL_EAST, TerrainIdentifier.WALL_EAST},
            {TILE_WALL_SOUTH, TerrainIdentifier.WALL_SOUTH},
            {TILE_WALL_WEST, TerrainIdentifier.WALL_WEST},
            {TILE_CORNER_INSIDE_NORTHEAST, TerrainIdentifier.CORNER_INSIDE_NORTHEAST},
            {TILE_CORNER_INSIDE_SOUTHEAST, TerrainIdentifier.CORNER_INSIDE_SOUTHEAST},
            {TILE_CORNER_INSIDE_SOUTHWEST, TerrainIdentifier.CORNER_INSIDE_SOUTHWEST},
            {TILE_CORNER_INSIDE_NORTHWEST, TerrainIdentifier.CORNER_INSIDE_NORTHWEST},
            {TILE_CORNER_OUTSIDE_NORTHEAST, TerrainIdentifier.CORNER_OUTSIDE_NORTHEAST},
            {TILE_CORNER_OUTSIDE_SOUTHEAST, TerrainIdentifier.CORNER_OUTSIDE_SOUTHEAST},
            {TILE_CORNER_OUTSIDE_SOUTHWEST, TerrainIdentifier.CORNER_OUTSIDE_SOUTHWEST},
            {TILE_CORNER_OUTSIDE_NORTHWEST, TerrainIdentifier.CORNER_OUTSIDE_NORTHWEST},
            {TILE_DOOR_NORTH, TerrainIdentifier.DOOR_NORTH},
            {TILE_DOOR_EAST, TerrainIdentifier.DOOR_EAST},
            {TILE_DOOR_SOUTH, TerrainIdentifier.DOOR_SOUTH},
            {TILE_DOOR_WEST, TerrainIdentifier.DOOR_WEST},
            {TILE_SOLID, TerrainIdentifier.SOLID},
            {TILE_FLOOR, TerrainIdentifier.FLOOR},
            {TILE_BLOOD, TerrainIdentifier.BLOOD},
            {TILE_EMPTY, TerrainIdentifier.EMPTY}
        }
    Friend Function ToMap() As Map
        Dim columns = Layers.First.Data.Width
        Dim rows = Layers.First.Data.Height
        Dim result = New Map(columns, rows)
        For Each layer In Layers
            If layer.Type = "indexed" Then
                Continue For
            End If
            For column = 0 To columns - 1
                For row = 0 To rows - 1
                    Dim tileIndex = layer.Data.Data(column + row * columns)
                    Dim cell = result.GetCell(column, row)
                    If tileIndex = TILE_EMPTY Then
                        Continue For
                    ElseIf tileIndex >= TILE_TERRAIN_START AndAlso tileIndex <= TILE_TERRAIN_END Then
                        cell.Terrain = terrainTable(tileIndex)
                    Else
                        Throw New NotImplementedException
                    End If
                Next
            Next
        Next
        Return result
    End Function
End Class
