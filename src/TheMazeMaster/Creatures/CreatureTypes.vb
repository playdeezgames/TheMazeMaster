Module CreatureTypes
    Friend ReadOnly AllCreatureTypes As IReadOnlyDictionary(Of String, CreatureType) =
        New Dictionary(Of String, CreatureType) From
        {
            {
                CREATURETYPE_DUDE,
                New CreatureType(
                    "Dude",
                    TILE_DUDE,
                    3,
                    minimumExitCount:=2,
                    defaultWeaponType:=ITEMTYPE_FIST)
            },
            {
                CREATURETYPE_RAT,
                New CreatureType(
                    "Rat",
                    TILE_RAT,
                    1,
                    spawnCount:=192,
                    defaultWeaponType:=ITEMTYPE_BITE,
                    xp:=1,
                    drop:=ITEMTYPE_RATTAIL)
            }
        }
    Friend Const CREATURETYPE_DUDE = "DUDE"
    Friend Const CREATURETYPE_RAT = "RAT"
    Friend ReadOnly ALL_CREATURETYPES As IReadOnlyList(Of String) = New List(Of String) From {CREATURETYPE_DUDE, CREATURETYPE_RAT}
End Module

