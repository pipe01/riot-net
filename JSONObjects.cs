using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET
{
    public interface RiotData
    { }

    public interface StaticRiotData : RiotData
    { }

    #region Status
    public class Status
    {
        [JsonProperty("status")]
        public StatusStruct Struct;
    }
    public class StatusStruct
    {
        public string Message;
        [JsonProperty("status_code")]
        public int StatusCode;
    }
    #endregion

    #region Summoner
    public class PlayerSBN : RiotData
    {
        /// <summary>
        /// Summoner name.
        /// </summary>
        [JsonProperty("name")]
        public string SummonerName;

        /// <summary>
        /// Summoner ID.
        /// </summary>
        [JsonProperty("id")]
        public long SummonerID;

        /// <summary>
        /// ID of the summoner icon associated with the summoner.
        /// </summary>
        public int ProfileIconID;

        /// <summary>
        /// Date summoner was last modified specified as epoch milliseconds.
        /// The following events will update this timestamp: profile icon change,
        /// playing the tutorial or advanced tutorial, finishing a game,
        /// summoner name change.
        /// </summary>
        public long RevisionDate;

        /// <summary>
        /// Summoner level associated with the summoner.
        /// </summary>
        public long SummonerLevel;
    }

    #region Masteries
    public class MasteryPagesDto : RiotData
    {
        /// <summary>
        /// Collection of pages associated with the summoner.
        /// </summary>
        public List<MasteryPageDto> Pages;

        /// <summary>
        /// Summoner ID.
        /// </summary>
        public long SummonerID;
    }
    public class MasteryPageDto : RiotData
    {
        /// <summary>
        /// Indicates if the mastery page is the current mastery page.
        /// </summary>
        public bool Current;

        /// <summary>
        /// Mastery page ID.
        /// </summary>
        public long ID;

        /// <summary>
        /// Collection of masteries associated with the mastery page.
        /// </summary>
        public List<MasteryDto> Masteries;

        /// <summary>
        /// Mastery page name.
        /// </summary>
        public string Name;
    }
    public class MasteryDto : RiotData
    {
        /// <summary>
        /// Mastery ID. For static information correlating to masteries, please refer 
        /// to the LoL Static Data API.
        /// </summary>
        public int ID;

        /// <summary>
        /// Mastery rank (i.e., the number of points put into this mastery).
        /// </summary>
        public int Rank;
    }
    #endregion

    #endregion

    #region League
    public class LeagueDto : RiotData
    {
        /// <summary>
        /// The requested league entries.
        /// </summary>
        public List<LeagueEntryDto> Entries;

        /// <summary>
        /// This name is an internal place-holder name only. Display and localization of
        /// names in the game client are handled client-side.
        /// </summary>
        public string Name;

        /// <summary>
        /// Specifies the relevant participant that is a member of this league (i.e.,
        /// a requested summoner ID, a requested team ID, or the ID of a team to which
        /// one of the requested summoners belongs). Only present when full league is
        /// requested so that participant's entry can be identified. Not present when
        /// individual entry is requested.
        /// </summary>
        public string ParticipantID;

        /// <summary>
        /// The league's queue type. (Legal values: RANKED_SOLO_5x5, RANKED_TEAM_3x3,
        /// RANKED_TEAM_5x5)
        /// </summary>
        public string Queue;

        /// <summary>
        /// The league's tier. (Legal values: CHALLENGER, MASTER, DIAMOND, PLATINUM,
        /// GOLD, SILVER, BRONZE)
        /// </summary>
        public string Tier;
    }
    public class LeagueEntryDto : RiotData
    {
        /// <summary>
        /// The league division of the participant.
        /// </summary>
        public string Division;

        /// <summary>
        /// Specifies if the participant is fresh blood.
        /// </summary>
        public bool IsFreshBlood;

        /// <summary>
        /// Specifies if the participant is on a hot streak.
        /// </summary>
        public bool IsHotStreak;

        /// <summary>
        /// Specifies if the participant is inactive.
        /// </summary>
        public bool IsInactive;

        /// <summary>
        /// Specifies if the participant is a veteran.
        /// </summary>
        public bool IsVeteran;

        /// <summary>
        /// The league points of the participant.
        /// </summary>
        public int LeaguePoints;

        /// <summary>
        /// The number of losses for the participant.
        /// </summary>
        public int Losses;

        /// <summary>
        /// Mini series data for the participant. Only present if the participant is
        /// currently in a mini series.
        /// </summary>
        public MiniSeriesDto MiniSeries;

        /// <summary>
        /// The ID of the participant (i.e., summoner or team) represented by this entry.
        /// </summary>
        public string PlayerOrTeamID;

        /// <summary>
        /// The name of the the participant (i.e., summoner or team) represented by
        /// this entry.
        /// </summary>
        public string PlayerOrTeamName;

        /// <summary>
        /// The number of wins for the participant.
        /// </summary>
        public int Wins;
    }
    public class MiniSeriesDto : RiotData
    {
        /// <summary>
        /// Number of current losses in the mini series.
        /// </summary>
        public int Losses;

        /// <summary>
        /// String showing the current, sequential mini series progress where 'W'
        /// represents a win, 'L' represents a loss, and 'N' represents a game that
        /// hasn't been played yet.
        /// </summary>
        public string Progress;

        /// <summary>
        /// Number of wins required for promotion.
        /// </summary>
        public int Target;

        /// <summary>
        /// Number of current wins in the mini series.
        /// </summary>
        public int Wins;
    }
    #endregion

    #region Game

    public class CurrentGameInfo
    {
        public static readonly string[] LegalGameModes = new[] { "CLASSIC", "ODIN", "ARAM",
            "TUTORIAL", "ONEFORALL", "ASCENSION", "FIRSTBLOOD", "KINGPORO" };
        public static readonly string[] LegalGameTypes = new[] { "CUSTOM_GAME", "MATCHED_GAME",
            "TUTORIAL_GAME" };


        /// <summary>
        /// Banned champion information.
        /// </summary>
        public List<BannedChampion> BannedChampions;

        /// <summary>
        /// The ID of the game.
        /// </summary>
        public long GameID;

        /// <summary>
        /// The amount of time in seconds that has passed since the game started
        /// </summary>
        public long GameLength;

        /// <summary>
        /// The game mode (Legal values: CurrentGameInfo.LegalGameModes[])
        /// </summary>
        public string GameMode;

        /// <summary>
        /// The queue type (queue types are documented on the Game Constants page)
        /// </summary>
        public long GameQueueConfigID;

        /// <summary>
        /// The game start time represented in epoch milliseconds
        /// </summary>
        public long GameStartTime;

        /// <summary>
        /// The ID of the map
        /// </summary>
        public long MapID;

        /// <summary>
        /// The observer information
        /// </summary>
        public Observer Observers;

        /// <summary>
        /// The participant information
        /// </summary>
        public List<CurrentGameParticipant> Participants;

        /// <summary>
        /// The ID of the platform on which the game is being played
        /// </summary>
        public string PlatformID;
    }

    public class BannedChampion
    {
        /// <summary>
        /// The ID of the banned champion
        /// </summary>
        public long ChampionID;

        /// <summary>
        /// The turn during which the champion was banned
        /// </summary>
        public int PickTurn;

        /// <summary>
        /// The ID of the team that banned the champion
        /// </summary>
        public long TeamID;
    }

    public class CurrentGameParticipant
    {
        /// <summary>
        /// Flag indicating whether or not this participant is a bot
        /// </summary>
        public bool Bot;

        /// <summary>
        /// The ID of the champion played by this participant
        /// </summary>
        public long ChampionID;

        /// <summary>
        /// The masteries used by this participant
        /// </summary>
        public List<Mastery> Masteries;

        /// <summary>
        /// The ID of the profile icon used by this participant
        /// </summary>
        public long ProfileIconID;

        /// <summary>
        /// The runes used by this participant
        /// </summary>
        public List<Rune> Runes;

        /// <summary>
        /// The ID of the first summoner spell used by this participant
        /// </summary>
        public long Spell1ID;

        /// <summary>
        /// The ID of the second summoner spell used by this participant
        /// </summary>
        public long Spell2ID;

        /// <summary>
        /// The summoner ID of this participant
        /// </summary>
        public long SummonerID;

        /// <summary>
        /// The summoner name of this participant
        /// </summary>
        public string SummonerName;

        /// <summary>
        /// The team ID of this participant, indicating the participant's team
        /// </summary>
        public long TeamID;
    }

    public class Observer
    {
        /// <summary>
        /// Key used to decrypt the spectator grid game data for playback
        /// </summary>
        public string EncryptionKey;
    }

    public class Mastery
    {
        /// <summary>
        /// The ID of the mastery
        /// </summary>
        public long MasteryID;

        /// <summary>
        /// The number of points put into this mastery by the user
        /// </summary>
        public int Rank;
    }

    public class Rune
    {
        /// <summary>
        /// The count of this rune used by the participant
        /// </summary>
        public int Count;

        /// <summary>
        /// The ID of the rune
        /// </summary>
        public long RuneID;
    }
    #endregion

    public class ChampionListDto
    {
        /// <summary>
        /// The collection of champion information
        /// </summary>
        public List<ChampionDto> Champions;
    }
    public class ChampionDto
    {
        /// <summary>
        /// Indicates if the champion is active.
        /// </summary>
        public bool Active;

        /// <summary>
        /// Bot enabled flag (for custom games).
        /// </summary>
        public bool BotEnabled;

        /// <summary>
        /// Bot Match Made enabled flag (for Co-op vs. AI games).
        /// </summary>
        public bool BotMMEnabled;

        /// <summary>
        /// Indicates if the champion is free to play. Free to play champions are
        /// rotated periodically.
        /// </summary>
        public bool FreeToPlay;

        /// <summary>
        /// Champion ID. For static information correlating to champion IDs, please
        /// refer to the LoL Static Data API.
        /// </summary>
        public long ID;

        /// <summary>
        /// Ranked play enabled flag.
        /// </summary>
        public bool RankedPlayEnabled;
    }

    #region Static Data

    #region Masteries

    public class MasteryPageStatic : StaticRiotData
    {
        public string Type;
        public string Version;
        public List<MasteryTreeStatic> Tree;
        public MasteryDataStatic Data;

    }
    [JsonObject("tree")]
    public class MasteryTreeStatic : StaticRiotData
    {
        public string Name;
        public List<MasteryEntryStatic> TreeLevels;
    }
    public class MasteryEntryStatic : StaticRiotData
    {
        public string MasteryID;
        [JsonProperty("prereq")]
        public string Prequisites;
    }
    public class MasteryDataStatic : StaticRiotData
    {
        public List<MasteryDataEntryStatic> Entries;
    }
    public class MasteryDataEntryStatic : StaticRiotData
    {
        public int ID;
        public string Name;
        public List<string> Description;
        public MasteryDataImageStatic Image;
        public int Ranks;
        [JsonProperty("prereq")]
        public string Prequisites;
    }
    public class MasteryDataImageStatic : StaticRiotData
    {
        public string Full;
        public string Sprite;
        public string Group;
        public int X;
        public int Y;
        [JsonProperty("w")]
        public int Width;
        [JsonProperty("h")]
        public int Height;
    }

    #endregion

    #endregion
}
