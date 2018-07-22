﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace NosCore.Shared.I18N
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum LanguageKey
    {
        UNREGISTRED_FROM_MASTER,
        REGISTRED_ON_MASTER,
        UNRECOGNIZED_MASTER_PACKET,
        AUTHENTICATED_SUCCESS,
        AUTHENTICATED_ERROR,
        DATABASE_INITIALIZED,
        DATABASE_NOT_UPTODATE,
        CLIENT_DISCONNECTED,
        CHARACTER_NOT_INIT,
        ERROR_CHANGE_MAP,
        AUTH_INCORRECT,
        AUTH_ERROR,
        FORCED_DISCONNECTION,
        CLIENT_CONNECTED,
        REGISTRED_FROM_MASTER,
        CLIENT_ARRIVED,
        CORRUPTED_KEEPALIVE,
        INVALID_PASSWORD,
        INVALID_ACCOUNT,
        ACCOUNT_ARRIVED,
        SUCCESSFULLY_LOADED,
        MASTER_SERVER_RETRY,
        LISTENING_PORT,
        MASTER_SERVER_LISTENING,
        ENTER_PATH,
        PARSE_ALL,
        PARSE_MAPS,
        PARSE_MAPTYPES,
        PARSE_ACCOUNTS,
        PARSE_PORTALS,
        PARSE_TIMESPACES,
        PARSE_ITEMS,
        PARSE_NPCMONSTERS,
        PARSE_NPCMONSTERDATA,
        PARSE_CARDS,
        PARSE_SKILLS,
        PARSE_MAPNPCS,
        PARSE_MONSTERS,
        PARSE_SHOPS,
        PARSE_TELEPORTERS,
        PARSE_SHOPITEMS,
        PARSE_SHOPSKILLS,
        PARSE_RECIPES,
        PARSE_QUESTS,
        DONE,
        AT_LEAST_ONE_FILE_MISSING,
        CARDS_PARSED,
        ITEMS_PARSED,
        MAPS_PARSED,
        PORTALS_PARSED,
        MAPS_LOADED,
        NO_MAP,
        MAPMONSTERS_LOADED,
        CORRUPT_PACKET,
        HANDLER_ERROR,
        HANDLER_NOT_FOUND,
        SELECT_MAPID,
        WRONG_SELECTED_MAPID,
        I18N_ACTDESC_PARSED,
        I18N_CARD_PARSED,
        I18N_BCARD_PARSED,
        I18N_ITEM_PARSED,
        I18N_MAPIDDATA_PARSED,
        I18N_MAPPOINTDATA_PARSED,
        I18N_MPCMONSTER_PARSED,
        I18N_NPCMONSTERTALK_PARSED,
        I18N_QUEST_PARSED,
        I18N_SKILL_PARSED,
        PARSE_I18N,
        ALREADY_TAKEN,
        INVALID_CHARNAME,
        BAD_PASSWORD,
        SUPPORT,
        [UsedImplicitly] ADVENTURER,
        [UsedImplicitly] SWORDMAN,
        [UsedImplicitly] ARCHER,
        [UsedImplicitly] MAGICIAN,
        [UsedImplicitly] WRESTLER,
        NPCMONSTERS_PARSED,
        PARSE_DROPS,
        MAPTYPES_PARSED,
        RESPAWNTYPE_PARSED,
        SKILLS_PARSED,
        NPCS_PARSED,
        MONSTERS_PARSED,
        CHANNEL,
        ADMINISTRATOR,
        CHARACTER_OFFLINE,
        SEND_MESSAGE_TO_CHARACTER,
        MAPNPCS_LOADED,
        CANT_FIND_CHARACTER,
        BLACKLIST_BLOCKED,
        FRIEND_OFFLINE,
        FRIEND_DELETED,
        FRIENDLIST_FULL,
        ALREADY_FRIEND,
        FRIEND_REQUEST_BLOCKED,
        FRIEND_ADD,
        FRIEND_ADDED,
        FRIEND_REJECTED,
        CANT_BLOCK_FRIEND,
        NOT_IN_BLACKLIST,
        BLACKLIST_ADDED
    }
}