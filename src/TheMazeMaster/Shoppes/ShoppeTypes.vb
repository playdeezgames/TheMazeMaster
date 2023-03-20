Friend Module ShoppeTypes
    Friend ReadOnly AllShoppeType As IReadOnlyDictionary(Of ShoppeTypeIdentifier, ShoppeType) =
        New List(Of ShoppeType) From
        {
            New ShoppeType(ShoppeTypeIdentifier.Knacker)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module
