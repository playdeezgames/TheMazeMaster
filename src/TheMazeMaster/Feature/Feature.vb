Friend Class Feature
    ReadOnly Property FeatureIndex As Integer
    ReadOnly Property FeatureTypeIdentifier As FeatureTypeIdentifier
    Sub New(
           featureIndex As Integer,
           featureTypeIdentifier As FeatureTypeIdentifier)
        Me.FeatureIndex = featureIndex
        Me.FeatureTypeIdentifier = featureTypeIdentifier
    End Sub
End Class
