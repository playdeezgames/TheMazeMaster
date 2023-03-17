Friend Class Room
    Sub New(map As Map, isChamber As Boolean)
        Me.Map = map
        Me.IsChamber = isChamber
    End Sub
    Friend ReadOnly Property Map As Map
    Friend ReadOnly Property IsChamber As Boolean
End Class
