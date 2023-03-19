Friend Class MapCell
    Property Terrain As TerrainIdentifier
    Private Property CreatureIndex As Integer?
    Property Creature As Creature
        Get
            If CreatureIndex Is Nothing Then
                Return Nothing
            End If
            Return Worlds.world.GetCreature(CreatureIndex.Value)
        End Get
        Set(value As Creature)
            If value Is Nothing Then
                CreatureIndex = Nothing
                Return
            End If
            CreatureIndex = value.CreatureIndex
        End Set
    End Property
    Property ItemIndex As Integer?
    Property FeatureIndex As Integer?
    ReadOnly Property Feature As Feature
        Get
            If FeatureIndex Is Nothing Then
                Return Nothing
            End If
            Return Worlds.world.GetFeature(FeatureIndex.Value)
        End Get
    End Property
    Friend ReadOnly Property CanSpawn As Boolean
        Get
            Return Not FeatureIndex.HasValue AndAlso Not CreatureIndex.HasValue AndAlso Not ItemIndex.HasValue AndAlso AllTerrains(Terrain).CanWalk
        End Get
    End Property
End Class
