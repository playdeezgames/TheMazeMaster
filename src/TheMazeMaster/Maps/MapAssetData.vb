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
End Class
