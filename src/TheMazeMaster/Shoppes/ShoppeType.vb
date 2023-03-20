Friend Class ShoppeType
    Friend ReadOnly Property Identifier As ShoppeTypeIdentifier
    Friend ReadOnly Property Name As String
    Friend ReadOnly Property CanSell As Boolean
        Get
            Return Offers.Any
        End Get
    End Property
    Friend ReadOnly Property Offers As IReadOnlyList(Of Trade)
    Sub New(
           Identifier As ShoppeTypeIdentifier,
           Name As String,
           offers As IReadOnlyList(Of Trade))
        Me.Identifier = Identifier
        Me.Name = Name
        Me.Offers = offers
    End Sub
End Class
