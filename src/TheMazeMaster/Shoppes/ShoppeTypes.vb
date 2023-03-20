Friend Module ShoppeTypes
    Friend ReadOnly AllShoppeTypes As IReadOnlyDictionary(Of ShoppeTypeIdentifier, ShoppeType) =
        New List(Of ShoppeType) From
        {
            New ShoppeType(
                ShoppeTypeIdentifier.Knacker,
                "the Knacker's hovel",
                New List(Of Trade) From
                {
                    New Trade((ItemTypeIdentifier.RatTail, 5), (ItemTypeIdentifier.Penny, 1))
                }),
            New ShoppeType(
                ShoppeTypeIdentifier.Chef,
                "the Bork Bork Bork Bistro",
                New List(Of Trade) From
                {
                    New Trade((ItemTypeIdentifier.Penny, 2), (ItemTypeIdentifier.Köttbulle, 1))
                })
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module
