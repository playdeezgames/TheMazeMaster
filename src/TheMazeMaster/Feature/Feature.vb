Friend Class Feature
    ReadOnly Property FeatureIndex As Integer
    ReadOnly Property FeatureTypeIdentifier As FeatureTypeIdentifier
    ReadOnly Property FeatureType As FeatureType
        Get
            Return AllFeatureTypes(FeatureTypeIdentifier)
        End Get
    End Property
    Sub New(
           featureIndex As Integer,
           featureTypeIdentifier As FeatureTypeIdentifier)
        Me.FeatureIndex = featureIndex
        Me.FeatureTypeIdentifier = featureTypeIdentifier
    End Sub
End Class
