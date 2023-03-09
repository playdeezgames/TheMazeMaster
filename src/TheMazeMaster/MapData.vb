Friend Class MapLayerData
    <JsonPropertyName("width")>
    Public Property Width As Integer
    <JsonPropertyName("height")>
    Public Property Height As Integer
    <JsonPropertyName("depth")>
    Public Property Depth As Integer
    <JsonPropertyName("data")>
    Public Property Data As New List(Of Integer)
End Class
Friend Class LayerData
    <JsonPropertyName("type")>
    Public Property Type As String
    <JsonPropertyName("overlap_x")>
    Public Property OverlapX As Integer
    <JsonPropertyName("overlap_y")>
    Public Property OverlapY As Integer
    <JsonPropertyName("data")>
    Public Property Data() As MapLayerData
End Class
Friend Class MapData
    <JsonPropertyName("layers")>
    Public Property Layers As New List(Of LayerData)
End Class
