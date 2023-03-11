Module CreatureTypes
    Friend ReadOnly AllCreatureTypes As IReadOnlyDictionary(Of String, CreatureType) =
        New Dictionary(Of String, CreatureType) From
        {
            {
                CREATURETYPE_DUDE,
                New CreatureType(
                    TILE_DUDE,
                    minimumExitCount:=2)
            },
            {
                CREATURETYPE_RAT,
                New CreatureType(
                    TILE_RAT)
            }
        }
    Friend Const CREATURETYPE_DUDE = "DUDE"
    Friend Const CREATURETYPE_RAT = "RAT"
    Friend ReadOnly ALL_CREATURETYPES As IReadOnlyList(Of String) = New List(Of String) From {CREATURETYPE_DUDE, CREATURETYPE_RAT}
    Friend ReadOnly CREATURETYPE_SPAWN_COUNTS As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {CREATURETYPE_DUDE, 0},
            {CREATURETYPE_RAT, 192}
        }
    Friend ReadOnly CREATURETYPE_DEFAULT_WEAPONTYPES As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {{CREATURETYPE_DUDE, ITEMTYPE_FIST}, {CREATURETYPE_RAT, ITEMTYPE_BITE}}
    Friend ReadOnly CREATURETYPE_HITPOINTS As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 3}, {CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_MINIMUM_X As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 1}, {CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_MINIMUM_Y As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 1}, {CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_MAXIMUM_X As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 13}, {CREATURETYPE_RAT, 13}}
    Friend ReadOnly CREATURETYPE_MAXIMUM_Y As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 13}, {CREATURETYPE_RAT, 13}}
    Friend ReadOnly CREATURETYPE_NAME As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {{CREATURETYPE_DUDE, "DUDE"}, {CREATURETYPE_RAT, "RAT"}}
    Friend ReadOnly CREATURETYPE_XP As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_ITEMTYPE_DROP As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {{CREATURETYPE_RAT, ITEMTYPE_RATTAIL}}
End Module

