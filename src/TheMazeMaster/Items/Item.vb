Friend Class Item
    Friend Sub New(itemTypeIdentifier As ItemTypeIdentifier)
        Me.ItemTypeIdentifier = itemTypeIdentifier
    End Sub
    Private Property ItemTypeIdentifier As ItemTypeIdentifier
    Friend ReadOnly Property ItemType As ItemType
        Get
            If AllItemTypes.ContainsKey(ItemTypeIdentifier) Then
                Return AllItemTypes(ItemTypeIdentifier)
            End If
            Return Nothing
        End Get
    End Property
    Friend Sub SetItemType(itemTypeIdentifier As ItemTypeIdentifier)
        Me.ItemTypeIdentifier = itemTypeIdentifier
    End Sub
End Class
