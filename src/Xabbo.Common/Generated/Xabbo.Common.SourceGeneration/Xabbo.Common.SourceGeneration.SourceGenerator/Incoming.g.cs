namespace Xabbo.Messages;

#nullable disable

public sealed partial class Incoming : Headers
{
		
	/// <summary>
	/// Gets the incoming header for the CfhSanction (Flash) message.
	/// </summary>
	public Header CfhSanction => Get("CfhSanction");
		
	/// <summary>
	/// Gets the incoming header for the CfhTopicsInit (Unity/Flash) message.
	/// </summary>
	public Header CfhTopicsInit => Get("CfhTopicsInit");
		
	/// <summary>
	/// Gets the incoming header for the SanctionStatus (Flash) message.
	/// </summary>
	public Header SanctionStatus => Get("SanctionStatus");
		
	/// <summary>
	/// Gets the incoming header for the UserNftWardrobe (Flash) message.
	/// </summary>
	public Header UserNftWardrobe => Get("UserNftWardrobe");
		
	/// <summary>
	/// Gets the incoming header for the UserNftWardrobeSelection (Flash) message.
	/// </summary>
	public Header UserNftWardrobeSelection => Get("UserNftWardrobeSelection");
		
	/// <summary>
	/// Gets the incoming header for the JukeboxPlayListFull (Unity/Flash) message.
	/// </summary>
	public Header JukeboxPlayListFull => Get("JukeboxPlayListFull");
		
	/// <summary>
	/// Gets the incoming header for the JukeboxSongDisks (Flash) message.
	/// </summary>
	public Header JukeboxSongDisks => Get("JukeboxSongDisks");
		
	/// <summary>
	/// Gets the incoming header for the NowPlaying (Unity/Flash) message.
	/// </summary>
	public Header NowPlaying => Get("NowPlaying");
		
	/// <summary>
	/// Gets the incoming header for the OfficialSongId (Flash) message.
	/// </summary>
	public Header OfficialSongId => Get("OfficialSongId");
		
	/// <summary>
	/// Gets the incoming header for the PlayList (Flash) message.
	/// </summary>
	public Header PlayList => Get("PlayList");
		
	/// <summary>
	/// Gets the incoming header for the PlayListSongAdded (Flash) message.
	/// </summary>
	public Header PlayListSongAdded => Get("PlayListSongAdded");
		
	/// <summary>
	/// Gets the incoming header for the TraxSongInfo (Flash) message.
	/// </summary>
	public Header TraxSongInfo => Get("TraxSongInfo");
		
	/// <summary>
	/// Gets the incoming header for the UserSongDisksInventory (Flash) message.
	/// </summary>
	public Header UserSongDisksInventory => Get("UserSongDisksInventory");
		
	/// <summary>
	/// Gets the incoming header for the ErrorReport (Unity/Flash) message.
	/// </summary>
	public Header ErrorReport => Get("ErrorReport");
		
	/// <summary>
	/// Gets the incoming header for the PhoneCollectionState (Flash) message.
	/// </summary>
	public Header PhoneCollectionState => Get("PhoneCollectionState");
		
	/// <summary>
	/// Gets the incoming header for the TryPhoneNumberResult (Unity/Flash) message.
	/// </summary>
	public Header TryPhoneNumberResult => Get("TryPhoneNumberResult");
		
	/// <summary>
	/// Gets the incoming header for the TryVerificationCodeResult (Flash) message.
	/// </summary>
	public Header TryVerificationCodeResult => Get("TryVerificationCodeResult");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpDisabledNotify (Flash) message.
	/// </summary>
	public Header CallForHelpDisabledNotify => Get("CallForHelpDisabledNotify");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpPendingCallsDeleted (Flash) message.
	/// </summary>
	public Header CallForHelpPendingCallsDeleted => Get("CallForHelpPendingCallsDeleted");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpPendingCalls (Flash) message.
	/// </summary>
	public Header CallForHelpPendingCalls => Get("CallForHelpPendingCalls");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpReply (Flash) message.
	/// </summary>
	public Header CallForHelpReply => Get("CallForHelpReply");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpResult (Unity/Flash) message.
	/// </summary>
	public Header CallForHelpResult => Get("CallForHelpResult");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewSessionDetached (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionDetached => Get("ChatReviewSessionDetached");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewSessionOfferedToGuide (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionOfferedToGuide => Get("ChatReviewSessionOfferedToGuide");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewSessionResults (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionResults => Get("ChatReviewSessionResults");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewSessionStarted (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionStarted => Get("ChatReviewSessionStarted");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewSessionVotingStatus (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionVotingStatus => Get("ChatReviewSessionVotingStatus");
		
	/// <summary>
	/// Gets the incoming header for the GuideOnDutyStatus (Unity/Flash) message.
	/// </summary>
	public Header GuideOnDutyStatus => Get("GuideOnDutyStatus");
		
	/// <summary>
	/// Gets the incoming header for the GuideReportingStatus (Unity/Flash) message.
	/// </summary>
	public Header GuideReportingStatus => Get("GuideReportingStatus");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionAttached (Flash) message.
	/// </summary>
	public Header GuideSessionAttached => Get("GuideSessionAttached");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionDetached (Flash) message.
	/// </summary>
	public Header GuideSessionDetached => Get("GuideSessionDetached");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionEnded (Flash) message.
	/// </summary>
	public Header GuideSessionEnded => Get("GuideSessionEnded");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionError (Flash) message.
	/// </summary>
	public Header GuideSessionError => Get("GuideSessionError");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionInvitedToGuideRoom (Flash) message.
	/// </summary>
	public Header GuideSessionInvitedToGuideRoom => Get("GuideSessionInvitedToGuideRoom");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionMessage (Flash) message.
	/// </summary>
	public Header GuideSessionMessage => Get("GuideSessionMessage");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionPartnerIsTyping (Flash) message.
	/// </summary>
	public Header GuideSessionPartnerIsTyping => Get("GuideSessionPartnerIsTyping");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionRequesterRoom (Flash) message.
	/// </summary>
	public Header GuideSessionRequesterRoom => Get("GuideSessionRequesterRoom");
		
	/// <summary>
	/// Gets the incoming header for the GuideSessionStarted (Flash) message.
	/// </summary>
	public Header GuideSessionStarted => Get("GuideSessionStarted");
		
	/// <summary>
	/// Gets the incoming header for the GuideTicketCreationResult (Flash) message.
	/// </summary>
	public Header GuideTicketCreationResult => Get("GuideTicketCreationResult");
		
	/// <summary>
	/// Gets the incoming header for the GuideTicketResolution (Flash) message.
	/// </summary>
	public Header GuideTicketResolution => Get("GuideTicketResolution");
		
	/// <summary>
	/// Gets the incoming header for the IssueCloseNotification (Unity/Flash) message.
	/// </summary>
	public Header IssueCloseNotification => Get("IssueCloseNotification");
		
	/// <summary>
	/// Gets the incoming header for the QuizData (Unity/Flash) message.
	/// </summary>
	public Header QuizData => Get("QuizData");
		
	/// <summary>
	/// Gets the incoming header for the QuizResults (Flash) message.
	/// </summary>
	public Header QuizResults => Get("QuizResults");
		
	/// <summary>
	/// Gets the incoming header for the CommunityGoalHallOfFame (Unity/Flash) message.
	/// </summary>
	public Header CommunityGoalHallOfFame => Get("CommunityGoalHallOfFame");
		
	/// <summary>
	/// Gets the incoming header for the CommunityGoalProgress (Unity/Flash) message.
	/// </summary>
	public Header CommunityGoalProgress => Get("CommunityGoalProgress");
		
	/// <summary>
	/// Gets the incoming header for the ConcurrentUsersGoalProgress (Unity/Flash) message.
	/// </summary>
	public Header ConcurrentUsersGoalProgress => Get("ConcurrentUsersGoalProgress");
		
	/// <summary>
	/// Gets the incoming header for the EpicPopup (Unity/Flash) message.
	/// </summary>
	public Header EpicPopup => Get("EpicPopup");
		
	/// <summary>
	/// Gets the incoming header for the QuestCancelled (Unity/Flash) message.
	/// </summary>
	public Header QuestCancelled => Get("QuestCancelled");
		
	/// <summary>
	/// Gets the incoming header for the QuestCompleted (Unity/Flash) message.
	/// </summary>
	public Header QuestCompleted => Get("QuestCompleted");
		
	/// <summary>
	/// Gets the incoming header for the QuestDaily (Unity/Flash) message.
	/// </summary>
	public Header QuestDaily => Get("QuestDaily");
		
	/// <summary>
	/// Gets the incoming header for the Quest (Unity/Flash) message.
	/// </summary>
	public Header Quest => Get("Quest");
		
	/// <summary>
	/// Gets the incoming header for the Quests (Unity/Flash) message.
	/// </summary>
	public Header Quests => Get("Quests");
		
	/// <summary>
	/// Gets the incoming header for the SeasonalQuests (Flash) message.
	/// </summary>
	public Header SeasonalQuests => Get("SeasonalQuests");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceBuyOfferResult (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceBuyOfferResult => Get("MarketplaceBuyOfferResult");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceCancelOfferResult (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceCancelOfferResult => Get("MarketplaceCancelOfferResult");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceCanMakeOfferResult (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceCanMakeOfferResult => Get("MarketplaceCanMakeOfferResult");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceConfiguration (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceConfiguration => Get("MarketplaceConfiguration");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceItemStats (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceItemStats => Get("MarketplaceItemStats");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceMakeOfferResult (Unity/Flash) message.
	/// </summary>
	public Header MarketplaceMakeOfferResult => Get("MarketplaceMakeOfferResult");
		
	/// <summary>
	/// Gets the incoming header for the MarketPlaceOffers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MarketplaceOpenOfferList" />.
	/// </summary>
	public Header MarketPlaceOffers => Get("MarketPlaceOffers");
		
	/// <summary>
	/// Gets the incoming header for the MarketPlaceOwnOffers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MarketplaceOwnOfferList" />.
	/// </summary>
	public Header MarketPlaceOwnOffers => Get("MarketPlaceOwnOffers");
		
	/// <summary>
	/// Gets the incoming header for the Achievement (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.PossibleAchievement" />.
	/// </summary>
	public Header Achievement => Get("Achievement");
		
	/// <summary>
	/// Gets the incoming header for the Achievements (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.PossibleUserAchievements" />.
	/// </summary>
	public Header Achievements => Get("Achievements");
		
	/// <summary>
	/// Gets the incoming header for the AchievementsScore (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.AchievementScore" />.
	/// </summary>
	public Header AchievementsScore => Get("AchievementsScore");
		
	/// <summary>
	/// Gets the incoming header for the CitizenshipVipOfferPromoEnabled (Flash) message.
	/// </summary>
	public Header CitizenshipVipOfferPromoEnabled => Get("CitizenshipVipOfferPromoEnabled");
		
	/// <summary>
	/// Gets the incoming header for the PerkAllowances (Unity/Flash) message.
	/// </summary>
	public Header PerkAllowances => Get("PerkAllowances");
		
	/// <summary>
	/// Gets the incoming header for the PromoArticles (Unity/Flash) message.
	/// </summary>
	public Header PromoArticles => Get("PromoArticles");
		
	/// <summary>
	/// Gets the incoming header for the FigureSetIds (Unity/Flash) message.
	/// </summary>
	public Header FigureSetIds => Get("FigureSetIds");
		
	/// <summary>
	/// Gets the incoming header for the CantConnect (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.CanNotConnect" />.
	/// </summary>
	public Header CantConnect => Get("CantConnect");
		
	/// <summary>
	/// Gets the incoming header for the CloseConnection (Unity/Flash) message.
	/// </summary>
	public Header CloseConnection => Get("CloseConnection");
		
	/// <summary>
	/// Gets the incoming header for the FlatAccessible (Flash) message.
	/// </summary>
	public Header FlatAccessible => Get("FlatAccessible");
		
	/// <summary>
	/// Gets the incoming header for the GamePlayerValue (Flash) message.
	/// </summary>
	public Header GamePlayerValue => Get("GamePlayerValue");
		
	/// <summary>
	/// Gets the incoming header for the OpenConnection (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.OpenConnectionConfirmation" />.
	/// </summary>
	public Header OpenConnection => Get("OpenConnection");
		
	/// <summary>
	/// Gets the incoming header for the RoomForward (Unity/Flash) message.
	/// </summary>
	public Header RoomForward => Get("RoomForward");
		
	/// <summary>
	/// Gets the incoming header for the RoomQueueStatus (Unity/Flash) message.
	/// </summary>
	public Header RoomQueueStatus => Get("RoomQueueStatus");
		
	/// <summary>
	/// Gets the incoming header for the RoomReady (Unity/Flash) message.
	/// </summary>
	public Header RoomReady => Get("RoomReady");
		
	/// <summary>
	/// Gets the incoming header for the YouArePlayingGame (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.GameYouArePlayer" />.
	/// </summary>
	public Header YouArePlayingGame => Get("YouArePlayingGame");
		
	/// <summary>
	/// Gets the incoming header for the YouAreSpectator (Unity/Flash) message.
	/// </summary>
	public Header YouAreSpectator => Get("YouAreSpectator");
		
	/// <summary>
	/// Gets the incoming header for the CanCreateRoom (Unity/Flash) message.
	/// </summary>
	public Header CanCreateRoom => Get("CanCreateRoom");
		
	/// <summary>
	/// Gets the incoming header for the CanCreateRoomEvent (Flash) message.
	/// </summary>
	public Header CanCreateRoomEvent => Get("CanCreateRoomEvent");
		
	/// <summary>
	/// Gets the incoming header for the CategoriesWithVisitorCount (Flash) message.
	/// </summary>
	public Header CategoriesWithVisitorCount => Get("CategoriesWithVisitorCount");
		
	/// <summary>
	/// Gets the incoming header for the CompetitionRoomsData (Unity/Flash) message.
	/// </summary>
	public Header CompetitionRoomsData => Get("CompetitionRoomsData");
		
	/// <summary>
	/// Gets the incoming header for the ConvertedRoomId (Unity/Flash) message.
	/// </summary>
	public Header ConvertedRoomId => Get("ConvertedRoomId");
		
	/// <summary>
	/// Gets the incoming header for the Doorbell (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.DoorbellRinging" />.
	/// </summary>
	public Header Doorbell => Get("Doorbell");
		
	/// <summary>
	/// Gets the incoming header for the FavouriteChanged (Unity/Flash) message.
	/// </summary>
	public Header FavouriteChanged => Get("FavouriteChanged");
		
	/// <summary>
	/// Gets the incoming header for the Favourites (Unity/Flash) message.
	/// </summary>
	public Header Favourites => Get("Favourites");
		
	/// <summary>
	/// Gets the incoming header for the FlatAccessDenied (Flash) message.
	/// </summary>
	public Header FlatAccessDenied => Get("FlatAccessDenied");
		
	/// <summary>
	/// Gets the incoming header for the FlatCreated (Unity/Flash) message.
	/// </summary>
	public Header FlatCreated => Get("FlatCreated");
		
	/// <summary>
	/// Gets the incoming header for the GetGuestRoomResult (Unity/Flash) message.
	/// </summary>
	public Header GetGuestRoomResult => Get("GetGuestRoomResult");
		
	/// <summary>
	/// Gets the incoming header for the GuestRoomSearchResult (Unity/Flash) message.
	/// </summary>
	public Header GuestRoomSearchResult => Get("GuestRoomSearchResult");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorSettings (Unity/Flash) message.
	/// </summary>
	public Header NavigatorSettings => Get("NavigatorSettings");
		
	/// <summary>
	/// Gets the incoming header for the OfficialRooms (Unity/Flash) message.
	/// </summary>
	public Header OfficialRooms => Get("OfficialRooms");
		
	/// <summary>
	/// Gets the incoming header for the PopularRoomTagsResult (Unity/Flash) message.
	/// </summary>
	public Header PopularRoomTagsResult => Get("PopularRoomTagsResult");
		
	/// <summary>
	/// Gets the incoming header for the RoomEventCancel (Flash) message.
	/// </summary>
	public Header RoomEventCancel => Get("RoomEventCancel");
		
	/// <summary>
	/// Gets the incoming header for the RoomEvent (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomEventEventInfo" />.
	/// </summary>
	public Header RoomEvent => Get("RoomEvent");
		
	/// <summary>
	/// Gets the incoming header for the RoomInfoUpdated (Flash) message.
	/// </summary>
	public Header RoomInfoUpdated => Get("RoomInfoUpdated");
		
	/// <summary>
	/// Gets the incoming header for the RoomRating (Unity/Flash) message.
	/// </summary>
	public Header RoomRating => Get("RoomRating");
		
	/// <summary>
	/// Gets the incoming header for the UserEventCats (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.EventFlatCategories" />.
	/// </summary>
	public Header UserEventCats => Get("UserEventCats");
		
	/// <summary>
	/// Gets the incoming header for the UserFlatCats (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UserFlatCategories" />.
	/// </summary>
	public Header UserFlatCats => Get("UserFlatCats");
		
	/// <summary>
	/// Gets the incoming header for the Game2FullGameStatus (Flash) message.
	/// </summary>
	public Header Game2FullGameStatus => Get("Game2FullGameStatus");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameStatus (Flash) message.
	/// </summary>
	public Header Game2GameStatus => Get("Game2GameStatus");
		
	/// <summary>
	/// Gets the incoming header for the BannedUsersFromRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UsersBannedFromRoom" />.
	/// </summary>
	public Header BannedUsersFromRoom => Get("BannedUsersFromRoom");
		
	/// <summary>
	/// Gets the incoming header for the FlatControllerAdded (Unity/Flash) message.
	/// </summary>
	public Header FlatControllerAdded => Get("FlatControllerAdded");
		
	/// <summary>
	/// Gets the incoming header for the FlatControllerRemoved (Unity/Flash) message.
	/// </summary>
	public Header FlatControllerRemoved => Get("FlatControllerRemoved");
		
	/// <summary>
	/// Gets the incoming header for the FlatControllers (Unity/Flash) message.
	/// </summary>
	public Header FlatControllers => Get("FlatControllers");
		
	/// <summary>
	/// Gets the incoming header for the MuteAllInRoom (Unity/Flash) message.
	/// </summary>
	public Header MuteAllInRoom => Get("MuteAllInRoom");
		
	/// <summary>
	/// Gets the incoming header for the NoSuchFlat (Unity/Flash) message.
	/// </summary>
	public Header NoSuchFlat => Get("NoSuchFlat");
		
	/// <summary>
	/// Gets the incoming header for the RoomSettingsData (Unity/Flash) message.
	/// </summary>
	public Header RoomSettingsData => Get("RoomSettingsData");
		
	/// <summary>
	/// Gets the incoming header for the RoomSettingsError (Unity/Flash) message.
	/// </summary>
	public Header RoomSettingsError => Get("RoomSettingsError");
		
	/// <summary>
	/// Gets the incoming header for the RoomSettingsSaved (Unity/Flash) message.
	/// </summary>
	public Header RoomSettingsSaved => Get("RoomSettingsSaved");
		
	/// <summary>
	/// Gets the incoming header for the RoomSettingsSaveError (Unity/Flash) message.
	/// </summary>
	public Header RoomSettingsSaveError => Get("RoomSettingsSaveError");
		
	/// <summary>
	/// Gets the incoming header for the ShowEnforceRoomCategoryDialog (Flash) message.
	/// </summary>
	public Header ShowEnforceRoomCategoryDialog => Get("ShowEnforceRoomCategoryDialog");
		
	/// <summary>
	/// Gets the incoming header for the UserUnbannedFromRoom (Unity/Flash) message.
	/// </summary>
	public Header UserUnbannedFromRoom => Get("UserUnbannedFromRoom");
		
	/// <summary>
	/// Gets the incoming header for the CfhChatLog (Unity/Flash) message.
	/// </summary>
	public Header CfhChatLog => Get("CfhChatLog");
		
	/// <summary>
	/// Gets the incoming header for the IssueDeleted (Flash) message.
	/// </summary>
	public Header IssueDeleted => Get("IssueDeleted");
		
	/// <summary>
	/// Gets the incoming header for the IssueInfo (Unity/Flash) message.
	/// </summary>
	public Header IssueInfo => Get("IssueInfo");
		
	/// <summary>
	/// Gets the incoming header for the IssuePickFailed (Unity/Flash) message.
	/// </summary>
	public Header IssuePickFailed => Get("IssuePickFailed");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorActionResult (Unity/Flash) message.
	/// </summary>
	public Header ModeratorActionResult => Get("ModeratorActionResult");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorCaution (Unity/Flash) message.
	/// </summary>
	public Header ModeratorCaution => Get("ModeratorCaution");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorInit (Unity/Flash) message.
	/// </summary>
	public Header ModeratorInit => Get("ModeratorInit");
		
	/// <summary>
	/// Gets the incoming header for the Moderator (Flash) message.
	/// </summary>
	public Header Moderator => Get("Moderator");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorRoomInfo (Unity/Flash) message.
	/// </summary>
	public Header ModeratorRoomInfo => Get("ModeratorRoomInfo");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorToolPreferences (Unity/Flash) message.
	/// </summary>
	public Header ModeratorToolPreferences => Get("ModeratorToolPreferences");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorUserInfo (Unity/Flash) message.
	/// </summary>
	public Header ModeratorUserInfo => Get("ModeratorUserInfo");
		
	/// <summary>
	/// Gets the incoming header for the RoomChatLog (Unity/Flash) message.
	/// </summary>
	public Header RoomChatLog => Get("RoomChatLog");
		
	/// <summary>
	/// Gets the incoming header for the RoomVisits (Unity/Flash) message.
	/// </summary>
	public Header RoomVisits => Get("RoomVisits");
		
	/// <summary>
	/// Gets the incoming header for the UserBanned (Unity/Flash) message.
	/// </summary>
	public Header UserBanned => Get("UserBanned");
		
	/// <summary>
	/// Gets the incoming header for the UserChatLog (Unity/Flash) message.
	/// </summary>
	public Header UserChatLog => Get("UserChatLog");
		
	/// <summary>
	/// Gets the incoming header for the BadgePointLimits (Unity/Flash) message.
	/// </summary>
	public Header BadgePointLimits => Get("BadgePointLimits");
		
	/// <summary>
	/// Gets the incoming header for the BadgeReceived (Unity/Flash) message.
	/// </summary>
	public Header BadgeReceived => Get("BadgeReceived");
		
	/// <summary>
	/// Gets the incoming header for the Badges (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.AvailableBadges" />.
	/// </summary>
	public Header Badges => Get("Badges");
		
	/// <summary>
	/// Gets the incoming header for the IsBadgeRequestFulfilled (Unity/Flash) message.
	/// </summary>
	public Header IsBadgeRequestFulfilled => Get("IsBadgeRequestFulfilled");
		
	/// <summary>
	/// Gets the incoming header for the BotAddedToInventory (Unity/Flash) message.
	/// </summary>
	public Header BotAddedToInventory => Get("BotAddedToInventory");
		
	/// <summary>
	/// Gets the incoming header for the BotInventory (Unity/Flash) message.
	/// </summary>
	public Header BotInventory => Get("BotInventory");
		
	/// <summary>
	/// Gets the incoming header for the BotRemovedFromInventory (Unity/Flash) message.
	/// </summary>
	public Header BotRemovedFromInventory => Get("BotRemovedFromInventory");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffect (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomAvatarEffect" />.
	/// </summary>
	public Header AvatarEffect => Get("AvatarEffect");
		
	/// <summary>
	/// Gets the incoming header for the CarryObject (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomCarryObject" />.
	/// </summary>
	public Header CarryObject => Get("CarryObject");
		
	/// <summary>
	/// Gets the incoming header for the Dance (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomDance" />.
	/// </summary>
	public Header Dance => Get("Dance");
		
	/// <summary>
	/// Gets the incoming header for the Expression (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomExpression" />.
	/// </summary>
	public Header Expression => Get("Expression");
		
	/// <summary>
	/// Gets the incoming header for the Sleep (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RoomAvatarSleeping" />.
	/// </summary>
	public Header Sleep => Get("Sleep");
		
	/// <summary>
	/// Gets the incoming header for the UseObject (Flash) message.
	/// </summary>
	public Header UseObject => Get("UseObject");
		
	/// <summary>
	/// Gets the incoming header for the TalentLevelUp (Flash) message.
	/// </summary>
	public Header TalentLevelUp => Get("TalentLevelUp");
		
	/// <summary>
	/// Gets the incoming header for the TalentTrackLevel (Unity/Flash) message.
	/// </summary>
	public Header TalentTrackLevel => Get("TalentTrackLevel");
		
	/// <summary>
	/// Gets the incoming header for the TalentTrack (Unity/Flash) message.
	/// </summary>
	public Header TalentTrack => Get("TalentTrack");
		
	/// <summary>
	/// Gets the incoming header for the BotCommandConfiguration (Flash) message.
	/// </summary>
	public Header BotCommandConfiguration => Get("BotCommandConfiguration");
		
	/// <summary>
	/// Gets the incoming header for the BotError (Unity/Flash) message.
	/// </summary>
	public Header BotError => Get("BotError");
		
	/// <summary>
	/// Gets the incoming header for the BotForceOpenContextMenu (Unity/Flash) message.
	/// </summary>
	public Header BotForceOpenContextMenu => Get("BotForceOpenContextMenu");
		
	/// <summary>
	/// Gets the incoming header for the BotSkillListUpdate (Flash) message.
	/// </summary>
	public Header BotSkillListUpdate => Get("BotSkillListUpdate");
		
	/// <summary>
	/// Gets the incoming header for the TradeOpenFailed (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeOpenFail" />.
	/// </summary>
	public Header TradeOpenFailed => Get("TradeOpenFailed");
		
	/// <summary>
	/// Gets the incoming header for the TradingAccept (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeAccept" />.
	/// </summary>
	public Header TradingAccept => Get("TradingAccept");
		
	/// <summary>
	/// Gets the incoming header for the TradingClose (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeClose" />.
	/// </summary>
	public Header TradingClose => Get("TradingClose");
		
	/// <summary>
	/// Gets the incoming header for the TradingCompleted (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeCompleted" />.
	/// </summary>
	public Header TradingCompleted => Get("TradingCompleted");
		
	/// <summary>
	/// Gets the incoming header for the TradingConfirmation (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeConfirmation" />.
	/// </summary>
	public Header TradingConfirmation => Get("TradingConfirmation");
		
	/// <summary>
	/// Gets the incoming header for the TradingItemList (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeItems" />.
	/// </summary>
	public Header TradingItemList => Get("TradingItemList");
		
	/// <summary>
	/// Gets the incoming header for the TradingNotOpen (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeNoTradeOpen" />.
	/// </summary>
	public Header TradingNotOpen => Get("TradingNotOpen");
		
	/// <summary>
	/// Gets the incoming header for the TradingOpen (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeOpen" />.
	/// </summary>
	public Header TradingOpen => Get("TradingOpen");
		
	/// <summary>
	/// Gets the incoming header for the TradingOtherNotAllowed (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeOtherNotAllowed" />.
	/// </summary>
	public Header TradingOtherNotAllowed => Get("TradingOtherNotAllowed");
		
	/// <summary>
	/// Gets the incoming header for the TradingYouAreNotAllowed (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.TradeYouAreNotAllowed" />.
	/// </summary>
	public Header TradingYouAreNotAllowed => Get("TradingYouAreNotAllowed");
		
	/// <summary>
	/// Gets the incoming header for the CameraPublishStatus (Flash) message.
	/// </summary>
	public Header CameraPublishStatus => Get("CameraPublishStatus");
		
	/// <summary>
	/// Gets the incoming header for the CameraPurchaseOK (Flash) message.
	/// </summary>
	public Header CameraPurchaseOK => Get("CameraPurchaseOK");
		
	/// <summary>
	/// Gets the incoming header for the CameraStorageUrl (Flash) message.
	/// </summary>
	public Header CameraStorageUrl => Get("CameraStorageUrl");
		
	/// <summary>
	/// Gets the incoming header for the CompetitionStatus (Flash) message.
	/// </summary>
	public Header CompetitionStatus => Get("CompetitionStatus");
		
	/// <summary>
	/// Gets the incoming header for the InitCamera (Flash) message.
	/// </summary>
	public Header InitCamera => Get("InitCamera");
		
	/// <summary>
	/// Gets the incoming header for the ThumbnailStatus (Flash) message.
	/// </summary>
	public Header ThumbnailStatus => Get("ThumbnailStatus");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffectActivated (Unity/Flash) message.
	/// </summary>
	public Header AvatarEffectActivated => Get("AvatarEffectActivated");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffectAdded (Unity/Flash) message.
	/// </summary>
	public Header AvatarEffectAdded => Get("AvatarEffectAdded");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffectExpired (Unity/Flash) message.
	/// </summary>
	public Header AvatarEffectExpired => Get("AvatarEffectExpired");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffectSelected (Unity/Flash) message.
	/// </summary>
	public Header AvatarEffectSelected => Get("AvatarEffectSelected");
		
	/// <summary>
	/// Gets the incoming header for the AvatarEffects (Unity/Flash) message.
	/// </summary>
	public Header AvatarEffects => Get("AvatarEffects");
		
	/// <summary>
	/// Gets the incoming header for the NewUserExperienceGiftOffer (Flash) message.
	/// </summary>
	public Header NewUserExperienceGiftOffer => Get("NewUserExperienceGiftOffer");
		
	/// <summary>
	/// Gets the incoming header for the NewUserExperienceNotComplete (Flash) message.
	/// </summary>
	public Header NewUserExperienceNotComplete => Get("NewUserExperienceNotComplete");
		
	/// <summary>
	/// Gets the incoming header for the SelectInitialRoom (Flash) message.
	/// </summary>
	public Header SelectInitialRoom => Get("SelectInitialRoom");
		
	/// <summary>
	/// Gets the incoming header for the ChangeUserNameResult (Unity/Flash) message.
	/// </summary>
	public Header ChangeUserNameResult => Get("ChangeUserNameResult");
		
	/// <summary>
	/// Gets the incoming header for the CheckUserNameResult (Unity/Flash) message.
	/// </summary>
	public Header CheckUserNameResult => Get("CheckUserNameResult");
		
	/// <summary>
	/// Gets the incoming header for the FigureUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UpdateFigure" />.
	/// </summary>
	public Header FigureUpdate => Get("FigureUpdate");
		
	/// <summary>
	/// Gets the incoming header for the Wardrobe (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UserWardrobe" />.
	/// </summary>
	public Header Wardrobe => Get("Wardrobe");
		
	/// <summary>
	/// Gets the incoming header for the CreditBalance (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.WalletBalance" />.
	/// </summary>
	public Header CreditBalance => Get("CreditBalance");
		
	/// <summary>
	/// Gets the incoming header for the ForumData (Flash) message.
	/// </summary>
	public Header ForumData => Get("ForumData");
		
	/// <summary>
	/// Gets the incoming header for the ForumsList (Unity/Flash) message.
	/// </summary>
	public Header ForumsList => Get("ForumsList");
		
	/// <summary>
	/// Gets the incoming header for the ForumThreads (Unity/Flash) message.
	/// </summary>
	public Header ForumThreads => Get("ForumThreads");
		
	/// <summary>
	/// Gets the incoming header for the PostMessage (Flash) message.
	/// </summary>
	public Header PostMessage => Get("PostMessage");
		
	/// <summary>
	/// Gets the incoming header for the PostThread (Flash) message.
	/// </summary>
	public Header PostThread => Get("PostThread");
		
	/// <summary>
	/// Gets the incoming header for the ThreadMessages (Flash) message.
	/// </summary>
	public Header ThreadMessages => Get("ThreadMessages");
		
	/// <summary>
	/// Gets the incoming header for the UnreadForumsCount (Unity/Flash) message.
	/// </summary>
	public Header UnreadForumsCount => Get("UnreadForumsCount");
		
	/// <summary>
	/// Gets the incoming header for the UpdateMessage (Flash) message.
	/// </summary>
	public Header UpdateMessage => Get("UpdateMessage");
		
	/// <summary>
	/// Gets the incoming header for the UpdateThread (Flash) message.
	/// </summary>
	public Header UpdateThread => Get("UpdateThread");
		
	/// <summary>
	/// Gets the incoming header for the ConfirmBreedingRequest (Unity/Flash) message.
	/// </summary>
	public Header ConfirmBreedingRequest => Get("ConfirmBreedingRequest");
		
	/// <summary>
	/// Gets the incoming header for the ConfirmBreedingResult (Unity/Flash) message.
	/// </summary>
	public Header ConfirmBreedingResult => Get("ConfirmBreedingResult");
		
	/// <summary>
	/// Gets the incoming header for the GoToBreedingNestFailure (Unity/Flash) message.
	/// </summary>
	public Header GoToBreedingNestFailure => Get("GoToBreedingNestFailure");
		
	/// <summary>
	/// Gets the incoming header for the NestBreedingSuccess (Unity/Flash) message.
	/// </summary>
	public Header NestBreedingSuccess => Get("NestBreedingSuccess");
		
	/// <summary>
	/// Gets the incoming header for the PetAddedToInventory (Unity/Flash) message.
	/// </summary>
	public Header PetAddedToInventory => Get("PetAddedToInventory");
		
	/// <summary>
	/// Gets the incoming header for the PetBreeding (Unity/Flash) message.
	/// </summary>
	public Header PetBreeding => Get("PetBreeding");
		
	/// <summary>
	/// Gets the incoming header for the PetInventory (Unity/Flash) message.
	/// </summary>
	public Header PetInventory => Get("PetInventory");
		
	/// <summary>
	/// Gets the incoming header for the PetReceived (Flash) message.
	/// </summary>
	public Header PetReceived => Get("PetReceived");
		
	/// <summary>
	/// Gets the incoming header for the PetRemovedFromInventory (Unity/Flash) message.
	/// </summary>
	public Header PetRemovedFromInventory => Get("PetRemovedFromInventory");
		
	/// <summary>
	/// Gets the incoming header for the HotLooks (Unity/Flash) message.
	/// </summary>
	public Header HotLooks => Get("HotLooks");
		
	/// <summary>
	/// Gets the incoming header for the FriendFurniCancelLock (Unity/Flash) message.
	/// </summary>
	public Header FriendFurniCancelLock => Get("FriendFurniCancelLock");
		
	/// <summary>
	/// Gets the incoming header for the FriendFurniOtherLockConfirmed (Flash) message.
	/// </summary>
	public Header FriendFurniOtherLockConfirmed => Get("FriendFurniOtherLockConfirmed");
		
	/// <summary>
	/// Gets the incoming header for the FriendFurniStartConfirmation (Flash) message.
	/// </summary>
	public Header FriendFurniStartConfirmation => Get("FriendFurniStartConfirmation");
		
	/// <summary>
	/// Gets the incoming header for the FurniListAddOrUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.InventoryAddOrUpdateFurni" />.
	/// </summary>
	public Header FurniListAddOrUpdate => Get("FurniListAddOrUpdate");
		
	/// <summary>
	/// Gets the incoming header for the FurniList (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.InventoryPush" />.
	/// </summary>
	public Header FurniList => Get("FurniList");
		
	/// <summary>
	/// Gets the incoming header for the FurniListInvalidate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.InventoryInvalidate" />.
	/// </summary>
	public Header FurniListInvalidate => Get("FurniListInvalidate");
		
	/// <summary>
	/// Gets the incoming header for the FurniListRemove (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.InventoryRemoveFurni" />.
	/// </summary>
	public Header FurniListRemove => Get("FurniListRemove");
		
	/// <summary>
	/// Gets the incoming header for the PostItPlaced (Unity/Flash) message.
	/// </summary>
	public Header PostItPlaced => Get("PostItPlaced");
		
	/// <summary>
	/// Gets the incoming header for the UserClassification (Flash) message.
	/// </summary>
	public Header UserClassification => Get("UserClassification");
		
	/// <summary>
	/// Gets the incoming header for the Game2FriendsLeaderboard (Flash) message.
	/// </summary>
	public Header Game2FriendsLeaderboard => Get("Game2FriendsLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the Game2TotalGroupLeaderboard (Flash) message.
	/// </summary>
	public Header Game2TotalGroupLeaderboard => Get("Game2TotalGroupLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the Game2TotalLeaderboard (Flash) message.
	/// </summary>
	public Header Game2TotalLeaderboard => Get("Game2TotalLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the Game2WeeklyFriendsLeaderboard (Flash) message.
	/// </summary>
	public Header Game2WeeklyFriendsLeaderboard => Get("Game2WeeklyFriendsLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the Game2WeeklyGroupLeaderboard (Flash) message.
	/// </summary>
	public Header Game2WeeklyGroupLeaderboard => Get("Game2WeeklyGroupLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the Game2WeeklyLeaderboard (Flash) message.
	/// </summary>
	public Header Game2WeeklyLeaderboard => Get("Game2WeeklyLeaderboard");
		
	/// <summary>
	/// Gets the incoming header for the PollContents (Unity/Flash) message.
	/// </summary>
	public Header PollContents => Get("PollContents");
		
	/// <summary>
	/// Gets the incoming header for the PollError (Unity/Flash) message.
	/// </summary>
	public Header PollError => Get("PollError");
		
	/// <summary>
	/// Gets the incoming header for the PollOffer (Unity/Flash) message.
	/// </summary>
	public Header PollOffer => Get("PollOffer");
		
	/// <summary>
	/// Gets the incoming header for the QuestionAnswered (Unity/Flash) message.
	/// </summary>
	public Header QuestionAnswered => Get("QuestionAnswered");
		
	/// <summary>
	/// Gets the incoming header for the Question (Unity/Flash) message.
	/// </summary>
	public Header Question => Get("Question");
		
	/// <summary>
	/// Gets the incoming header for the QuestionFinished (Unity/Flash) message.
	/// </summary>
	public Header QuestionFinished => Get("QuestionFinished");
		
	/// <summary>
	/// Gets the incoming header for the CreditVaultStatus (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.VaultStatus" />.
	/// </summary>
	public Header CreditVaultStatus => Get("CreditVaultStatus");
		
	/// <summary>
	/// Gets the incoming header for the IncomeRewardClaimResponse (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ClaimEarningResult" />.
	/// </summary>
	public Header IncomeRewardClaimResponse => Get("IncomeRewardClaimResponse");
		
	/// <summary>
	/// Gets the incoming header for the IncomeRewardStatus (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.EarningStatus" />.
	/// </summary>
	public Header IncomeRewardStatus => Get("IncomeRewardStatus");
		
	/// <summary>
	/// Gets the incoming header for the CraftableProducts (Unity/Flash) message.
	/// </summary>
	public Header CraftableProducts => Get("CraftableProducts");
		
	/// <summary>
	/// Gets the incoming header for the CraftingRecipe (Unity/Flash) message.
	/// </summary>
	public Header CraftingRecipe => Get("CraftingRecipe");
		
	/// <summary>
	/// Gets the incoming header for the CraftingRecipesAvailable (Unity/Flash) message.
	/// </summary>
	public Header CraftingRecipesAvailable => Get("CraftingRecipesAvailable");
		
	/// <summary>
	/// Gets the incoming header for the CraftingResult (Unity/Flash) message.
	/// </summary>
	public Header CraftingResult => Get("CraftingResult");
		
	/// <summary>
	/// Gets the incoming header for the CustomStackingHeightUpdate (Flash) message.
	/// </summary>
	public Header CustomStackingHeightUpdate => Get("CustomStackingHeightUpdate");
		
	/// <summary>
	/// Gets the incoming header for the CustomUserNotification (Unity/Flash) message.
	/// </summary>
	public Header CustomUserNotification => Get("CustomUserNotification");
		
	/// <summary>
	/// Gets the incoming header for the DiceValue (Unity/Flash) message.
	/// </summary>
	public Header DiceValue => Get("DiceValue");
		
	/// <summary>
	/// Gets the incoming header for the FurniRentOrBuyoutOffer (Unity/Flash) message.
	/// </summary>
	public Header FurniRentOrBuyoutOffer => Get("FurniRentOrBuyoutOffer");
		
	/// <summary>
	/// Gets the incoming header for the GuildFurniContextmenuInfo (Unity/Flash) message.
	/// </summary>
	public Header GuildFurniContextmenuInfo => Get("GuildFurniContextmenuInfo");
		
	/// <summary>
	/// Gets the incoming header for the OneWayDoorStatus (Flash) message.
	/// </summary>
	public Header OneWayDoorStatus => Get("OneWayDoorStatus");
		
	/// <summary>
	/// Gets the incoming header for the OpenPetPackageRequested (Unity/Flash) message.
	/// </summary>
	public Header OpenPetPackageRequested => Get("OpenPetPackageRequested");
		
	/// <summary>
	/// Gets the incoming header for the OpenPetPackageResult (Unity/Flash) message.
	/// </summary>
	public Header OpenPetPackageResult => Get("OpenPetPackageResult");
		
	/// <summary>
	/// Gets the incoming header for the PresentOpened (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.PresentOpen" />.
	/// </summary>
	public Header PresentOpened => Get("PresentOpened");
		
	/// <summary>
	/// Gets the incoming header for the RentableSpaceRentFailed (Unity/Flash) message.
	/// </summary>
	public Header RentableSpaceRentFailed => Get("RentableSpaceRentFailed");
		
	/// <summary>
	/// Gets the incoming header for the RentableSpaceRentOk (Unity/Flash) message.
	/// </summary>
	public Header RentableSpaceRentOk => Get("RentableSpaceRentOk");
		
	/// <summary>
	/// Gets the incoming header for the RentableSpaceStatus (Flash) message.
	/// </summary>
	public Header RentableSpaceStatus => Get("RentableSpaceStatus");
		
	/// <summary>
	/// Gets the incoming header for the RequestSpamWallPostIt (Flash) message.
	/// </summary>
	public Header RequestSpamWallPostIt => Get("RequestSpamWallPostIt");
		
	/// <summary>
	/// Gets the incoming header for the RoomDimmerPresets (Unity/Flash) message.
	/// </summary>
	public Header RoomDimmerPresets => Get("RoomDimmerPresets");
		
	/// <summary>
	/// Gets the incoming header for the RoomMessageNotification (Unity/Flash) message.
	/// </summary>
	public Header RoomMessageNotification => Get("RoomMessageNotification");
		
	/// <summary>
	/// Gets the incoming header for the YoutubeControlVideo (Unity/Flash) message.
	/// </summary>
	public Header YoutubeControlVideo => Get("YoutubeControlVideo");
		
	/// <summary>
	/// Gets the incoming header for the YoutubeDisplayPlaylists (Flash) message.
	/// </summary>
	public Header YoutubeDisplayPlaylists => Get("YoutubeDisplayPlaylists");
		
	/// <summary>
	/// Gets the incoming header for the YoutubeDisplayVideo (Unity/Flash) message.
	/// </summary>
	public Header YoutubeDisplayVideo => Get("YoutubeDisplayVideo");
		
	/// <summary>
	/// Gets the incoming header for the AvailabilityStatus (Unity/Flash) message.
	/// </summary>
	public Header AvailabilityStatus => Get("AvailabilityStatus");
		
	/// <summary>
	/// Gets the incoming header for the InfoHotelClosed (Unity/Flash) message.
	/// </summary>
	public Header InfoHotelClosed => Get("InfoHotelClosed");
		
	/// <summary>
	/// Gets the incoming header for the InfoHotelClosing (Unity/Flash) message.
	/// </summary>
	public Header InfoHotelClosing => Get("InfoHotelClosing");
		
	/// <summary>
	/// Gets the incoming header for the LoginFailedHotelClosed (Unity/Flash) message.
	/// </summary>
	public Header LoginFailedHotelClosed => Get("LoginFailedHotelClosed");
		
	/// <summary>
	/// Gets the incoming header for the MaintenanceStatus (Unity/Flash) message.
	/// </summary>
	public Header MaintenanceStatus => Get("MaintenanceStatus");
		
	/// <summary>
	/// Gets the incoming header for the Interstitial (Unity/Flash) message.
	/// </summary>
	public Header Interstitial => Get("Interstitial");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdError (Unity/Flash) message.
	/// </summary>
	public Header RoomAdError => Get("RoomAdError");
		
	/// <summary>
	/// Gets the incoming header for the AccountPreferences (Unity/Flash) message.
	/// </summary>
	public Header AccountPreferences => Get("AccountPreferences");
		
	/// <summary>
	/// Gets the incoming header for the AuthenticationOK (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Ok" />.
	/// </summary>
	public Header AuthenticationOK => Get("AuthenticationOK");
		
	/// <summary>
	/// Gets the incoming header for the CompleteDiffieHandshake (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.DhCompleteHandshake" />.
	/// </summary>
	public Header CompleteDiffieHandshake => Get("CompleteDiffieHandshake");
		
	/// <summary>
	/// Gets the incoming header for the DisconnectReason (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.DisconnectionReason" />.
	/// </summary>
	public Header DisconnectReason => Get("DisconnectReason");
		
	/// <summary>
	/// Gets the incoming header for the GenericError (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Error" />.
	/// </summary>
	public Header GenericError => Get("GenericError");
		
	/// <summary>
	/// Gets the incoming header for the IdentityAccounts (Flash) message.
	/// </summary>
	public Header IdentityAccounts => Get("IdentityAccounts");
		
	/// <summary>
	/// Gets the incoming header for the InitDiffieHandshake (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.DhInitHandshake" />.
	/// </summary>
	public Header InitDiffieHandshake => Get("InitDiffieHandshake");
		
	/// <summary>
	/// Gets the incoming header for the IsFirstLoginOfDay (Unity/Flash) message.
	/// </summary>
	public Header IsFirstLoginOfDay => Get("IsFirstLoginOfDay");
		
	/// <summary>
	/// Gets the incoming header for the NoobnessLevel (Unity/Flash) message.
	/// </summary>
	public Header NoobnessLevel => Get("NoobnessLevel");
		
	/// <summary>
	/// Gets the incoming header for the Ping (Unity/Flash) message.
	/// </summary>
	public Header Ping => Get("Ping");
		
	/// <summary>
	/// Gets the incoming header for the UniqueMachineId (Unity/Flash) message.
	/// </summary>
	public Header UniqueMachineId => Get("UniqueMachineId");
		
	/// <summary>
	/// Gets the incoming header for the UserObject (Unity/Flash) message.
	/// </summary>
	public Header UserObject => Get("UserObject");
		
	/// <summary>
	/// Gets the incoming header for the UserRights (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ClubAndSecurityLevels" />.
	/// </summary>
	public Header UserRights => Get("UserRights");
		
	/// <summary>
	/// Gets the incoming header for the RoomEntryTile (Unity/Flash) message.
	/// </summary>
	public Header RoomEntryTile => Get("RoomEntryTile");
		
	/// <summary>
	/// Gets the incoming header for the RoomOccupiedTiles (Unity/Flash) message.
	/// </summary>
	public Header RoomOccupiedTiles => Get("RoomOccupiedTiles");
		
	/// <summary>
	/// Gets the incoming header for the Chat (Unity/Flash) message.
	/// </summary>
	public Header Chat => Get("Chat");
		
	/// <summary>
	/// Gets the incoming header for the FloodControl (Unity/Flash) message.
	/// </summary>
	public Header FloodControl => Get("FloodControl");
		
	/// <summary>
	/// Gets the incoming header for the RemainingMutePeriod (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MuteTimeRemaining" />.
	/// </summary>
	public Header RemainingMutePeriod => Get("RemainingMutePeriod");
		
	/// <summary>
	/// Gets the incoming header for the RoomChatSettings (Unity/Flash) message.
	/// </summary>
	public Header RoomChatSettings => Get("RoomChatSettings");
		
	/// <summary>
	/// Gets the incoming header for the RoomFilterSettings (Unity/Flash) message.
	/// </summary>
	public Header RoomFilterSettings => Get("RoomFilterSettings");
		
	/// <summary>
	/// Gets the incoming header for the Shout (Unity/Flash) message.
	/// </summary>
	public Header Shout => Get("Shout");
		
	/// <summary>
	/// Gets the incoming header for the UserTyping (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UserTypingStatusChange" />.
	/// </summary>
	public Header UserTyping => Get("UserTyping");
		
	/// <summary>
	/// Gets the incoming header for the Whisper (Unity/Flash) message.
	/// </summary>
	public Header Whisper => Get("Whisper");
		
	/// <summary>
	/// Gets the incoming header for the AcceptFriendResult (Unity/Flash) message.
	/// </summary>
	public Header AcceptFriendResult => Get("AcceptFriendResult");
		
	/// <summary>
	/// Gets the incoming header for the FindFriendsProcessResult (Flash) message.
	/// </summary>
	public Header FindFriendsProcessResult => Get("FindFriendsProcessResult");
		
	/// <summary>
	/// Gets the incoming header for the FollowFriendFailed (Unity/Flash) message.
	/// </summary>
	public Header FollowFriendFailed => Get("FollowFriendFailed");
		
	/// <summary>
	/// Gets the incoming header for the FriendListFragment (Unity/Flash) message.
	/// </summary>
	public Header FriendListFragment => Get("FriendListFragment");
		
	/// <summary>
	/// Gets the incoming header for the FriendListUpdate (Unity/Flash) message.
	/// </summary>
	public Header FriendListUpdate => Get("FriendListUpdate");
		
	/// <summary>
	/// Gets the incoming header for the FriendNotification (Flash) message.
	/// </summary>
	public Header FriendNotification => Get("FriendNotification");
		
	/// <summary>
	/// Gets the incoming header for the FriendRequests (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.BuddyRequests" />.
	/// </summary>
	public Header FriendRequests => Get("FriendRequests");
		
	/// <summary>
	/// Gets the incoming header for the HabboSearchResult (Unity/Flash) message.
	/// </summary>
	public Header HabboSearchResult => Get("HabboSearchResult");
		
	/// <summary>
	/// Gets the incoming header for the InstantMessageError (Unity/Flash) message.
	/// </summary>
	public Header InstantMessageError => Get("InstantMessageError");
		
	/// <summary>
	/// Gets the incoming header for the MessengerError (Unity/Flash) message.
	/// </summary>
	public Header MessengerError => Get("MessengerError");
		
	/// <summary>
	/// Gets the incoming header for the MessengerInit (Unity/Flash) message.
	/// </summary>
	public Header MessengerInit => Get("MessengerInit");
		
	/// <summary>
	/// Gets the incoming header for the MiniMailNew (Flash) message.
	/// </summary>
	public Header MiniMailNew => Get("MiniMailNew");
		
	/// <summary>
	/// Gets the incoming header for the MiniMailUnreadCount (Unity/Flash) message.
	/// </summary>
	public Header MiniMailUnreadCount => Get("MiniMailUnreadCount");
		
	/// <summary>
	/// Gets the incoming header for the NewConsole (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MessengerNewConsoleMessage" />.
	/// </summary>
	public Header NewConsole => Get("NewConsole");
		
	/// <summary>
	/// Gets the incoming header for the NewFriendRequest (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.NewBuddyRequest" />.
	/// </summary>
	public Header NewFriendRequest => Get("NewFriendRequest");
		
	/// <summary>
	/// Gets the incoming header for the RoomInviteError (Unity/Flash) message.
	/// </summary>
	public Header RoomInviteError => Get("RoomInviteError");
		
	/// <summary>
	/// Gets the incoming header for the RoomInvite (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MessengerRoomInvite" />.
	/// </summary>
	public Header RoomInvite => Get("RoomInvite");
		
	/// <summary>
	/// Gets the incoming header for the BonusRareInfo (Unity/Flash) message.
	/// </summary>
	public Header BonusRareInfo => Get("BonusRareInfo");
		
	/// <summary>
	/// Gets the incoming header for the BuildersClubFurniCount (Unity/Flash) message.
	/// </summary>
	public Header BuildersClubFurniCount => Get("BuildersClubFurniCount");
		
	/// <summary>
	/// Gets the incoming header for the BuildersClubSubscriptionStatus (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.BuildersClubMembershipStatus" />.
	/// </summary>
	public Header BuildersClubSubscriptionStatus => Get("BuildersClubSubscriptionStatus");
		
	/// <summary>
	/// Gets the incoming header for the BundleDiscountRuleset (Unity/Flash) message.
	/// </summary>
	public Header BundleDiscountRuleset => Get("BundleDiscountRuleset");
		
	/// <summary>
	/// Gets the incoming header for the CatalogIndex (Unity/Flash) message.
	/// </summary>
	public Header CatalogIndex => Get("CatalogIndex");
		
	/// <summary>
	/// Gets the incoming header for the CatalogPage (Unity/Flash) message.
	/// </summary>
	public Header CatalogPage => Get("CatalogPage");
		
	/// <summary>
	/// Gets the incoming header for the CatalogPageWithEarliestExpiry (Unity/Flash) message.
	/// </summary>
	public Header CatalogPageWithEarliestExpiry => Get("CatalogPageWithEarliestExpiry");
		
	/// <summary>
	/// Gets the incoming header for the CatalogPublished (Flash) message.
	/// </summary>
	public Header CatalogPublished => Get("CatalogPublished");
		
	/// <summary>
	/// Gets the incoming header for the ClubGiftInfo (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.SelectableClubGiftInfo" />.
	/// </summary>
	public Header ClubGiftInfo => Get("ClubGiftInfo");
		
	/// <summary>
	/// Gets the incoming header for the ClubGiftSelected (Unity/Flash) message.
	/// </summary>
	public Header ClubGiftSelected => Get("ClubGiftSelected");
		
	/// <summary>
	/// Gets the incoming header for the GiftReceiverNotFound (Flash) message.
	/// </summary>
	public Header GiftReceiverNotFound => Get("GiftReceiverNotFound");
		
	/// <summary>
	/// Gets the incoming header for the GiftWrappingConfiguration (Unity/Flash) message.
	/// </summary>
	public Header GiftWrappingConfiguration => Get("GiftWrappingConfiguration");
		
	/// <summary>
	/// Gets the incoming header for the HabboClubExtendOffer (Unity/Flash) message.
	/// </summary>
	public Header HabboClubExtendOffer => Get("HabboClubExtendOffer");
		
	/// <summary>
	/// Gets the incoming header for the HabboClubOffers (Unity/Flash) message.
	/// </summary>
	public Header HabboClubOffers => Get("HabboClubOffers");
		
	/// <summary>
	/// Gets the incoming header for the LimitedEditionSoldOut (Unity/Flash) message.
	/// </summary>
	public Header LimitedEditionSoldOut => Get("LimitedEditionSoldOut");
		
	/// <summary>
	/// Gets the incoming header for the LimitedOfferAppearingNext (Flash) message.
	/// </summary>
	public Header LimitedOfferAppearingNext => Get("LimitedOfferAppearingNext");
		
	/// <summary>
	/// Gets the incoming header for the NotEnoughBalance (Unity/Flash) message.
	/// </summary>
	public Header NotEnoughBalance => Get("NotEnoughBalance");
		
	/// <summary>
	/// Gets the incoming header for the ProductOffer (Unity/Flash) message.
	/// </summary>
	public Header ProductOffer => Get("ProductOffer");
		
	/// <summary>
	/// Gets the incoming header for the PurchaseError (Unity/Flash) message.
	/// </summary>
	public Header PurchaseError => Get("PurchaseError");
		
	/// <summary>
	/// Gets the incoming header for the PurchaseNotAllowed (Unity/Flash) message.
	/// </summary>
	public Header PurchaseNotAllowed => Get("PurchaseNotAllowed");
		
	/// <summary>
	/// Gets the incoming header for the PurchaseOk (Unity/Flash) message.
	/// </summary>
	public Header PurchaseOk => Get("PurchaseOk");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdPurchaseInfo (Flash) message.
	/// </summary>
	public Header RoomAdPurchaseInfo => Get("RoomAdPurchaseInfo");
		
	/// <summary>
	/// Gets the incoming header for the SeasonalCalendarDailyOffer (Flash) message.
	/// </summary>
	public Header SeasonalCalendarDailyOffer => Get("SeasonalCalendarDailyOffer");
		
	/// <summary>
	/// Gets the incoming header for the SellablePetPalettes (Unity/Flash) message.
	/// </summary>
	public Header SellablePetPalettes => Get("SellablePetPalettes");
		
	/// <summary>
	/// Gets the incoming header for the SnowWarGameTokens (Flash) message.
	/// </summary>
	public Header SnowWarGameTokens => Get("SnowWarGameTokens");
		
	/// <summary>
	/// Gets the incoming header for the TargetedOffer (Unity/Flash) message.
	/// </summary>
	public Header TargetedOffer => Get("TargetedOffer");
		
	/// <summary>
	/// Gets the incoming header for the TargetedOfferNotFound (Unity/Flash) message.
	/// </summary>
	public Header TargetedOfferNotFound => Get("TargetedOfferNotFound");
		
	/// <summary>
	/// Gets the incoming header for the VoucherRedeemError (Unity/Flash) message.
	/// </summary>
	public Header VoucherRedeemError => Get("VoucherRedeemError");
		
	/// <summary>
	/// Gets the incoming header for the VoucherRedeemOk (Unity/Flash) message.
	/// </summary>
	public Header VoucherRedeemOk => Get("VoucherRedeemOk");
		
	/// <summary>
	/// Gets the incoming header for the LatencyPingResponse (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ClientLatencyPingResponse" />.
	/// </summary>
	public Header LatencyPingResponse => Get("LatencyPingResponse");
		
	/// <summary>
	/// Gets the incoming header for the CompetitionEntrySubmitResult (Unity/Flash) message.
	/// </summary>
	public Header CompetitionEntrySubmitResult => Get("CompetitionEntrySubmitResult");
		
	/// <summary>
	/// Gets the incoming header for the CompetitionVotingInfo (Unity/Flash) message.
	/// </summary>
	public Header CompetitionVotingInfo => Get("CompetitionVotingInfo");
		
	/// <summary>
	/// Gets the incoming header for the CurrentTimingCode (Unity/Flash) message.
	/// </summary>
	public Header CurrentTimingCode => Get("CurrentTimingCode");
		
	/// <summary>
	/// Gets the incoming header for the IsUserPartOfCompetition (Unity/Flash) message.
	/// </summary>
	public Header IsUserPartOfCompetition => Get("IsUserPartOfCompetition");
		
	/// <summary>
	/// Gets the incoming header for the NoOwnedRoomsAlert (Unity/Flash) message.
	/// </summary>
	public Header NoOwnedRoomsAlert => Get("NoOwnedRoomsAlert");
		
	/// <summary>
	/// Gets the incoming header for the SecondsUntil (Unity/Flash) message.
	/// </summary>
	public Header SecondsUntil => Get("SecondsUntil");
		
	/// <summary>
	/// Gets the incoming header for the CancelMysteryBoxWait (Unity/Flash) message.
	/// </summary>
	public Header CancelMysteryBoxWait => Get("CancelMysteryBoxWait");
		
	/// <summary>
	/// Gets the incoming header for the GotMysteryBoxPrize (Unity/Flash) message.
	/// </summary>
	public Header GotMysteryBoxPrize => Get("GotMysteryBoxPrize");
		
	/// <summary>
	/// Gets the incoming header for the MysteryBoxKeys (Unity/Flash) message.
	/// </summary>
	public Header MysteryBoxKeys => Get("MysteryBoxKeys");
		
	/// <summary>
	/// Gets the incoming header for the ShowMysteryBoxWait (Unity/Flash) message.
	/// </summary>
	public Header ShowMysteryBoxWait => Get("ShowMysteryBoxWait");
		
	/// <summary>
	/// Gets the incoming header for the CampaignCalendarData (Unity/Flash) message.
	/// </summary>
	public Header CampaignCalendarData => Get("CampaignCalendarData");
		
	/// <summary>
	/// Gets the incoming header for the CampaignCalendarDoorOpened (Unity/Flash) message.
	/// </summary>
	public Header CampaignCalendarDoorOpened => Get("CampaignCalendarDoorOpened");
		
	/// <summary>
	/// Gets the incoming header for the FavoriteMembershipUpdate (Unity/Flash) message.
	/// </summary>
	public Header FavoriteMembershipUpdate => Get("FavoriteMembershipUpdate");
		
	/// <summary>
	/// Gets the incoming header for the FloorHeightmap (Unity/Flash) message.
	/// </summary>
	public Header FloorHeightmap => Get("FloorHeightmap");
		
	/// <summary>
	/// Gets the incoming header for the FurnitureAliases (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.FurniAliases" />.
	/// </summary>
	public Header FurnitureAliases => Get("FurnitureAliases");
		
	/// <summary>
	/// Gets the incoming header for the HeightMap (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.StackingHeightmap" />.
	/// </summary>
	public Header HeightMap => Get("HeightMap");
		
	/// <summary>
	/// Gets the incoming header for the HeightMapUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.StackingHeightmapDiff" />.
	/// </summary>
	public Header HeightMapUpdate => Get("HeightMapUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ItemAdd (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.AddItem" />.
	/// </summary>
	public Header ItemAdd => Get("ItemAdd");
		
	/// <summary>
	/// Gets the incoming header for the ItemDataUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ItemData" />.
	/// </summary>
	public Header ItemDataUpdate => Get("ItemDataUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ItemRemove (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RemoveItem" />.
	/// </summary>
	public Header ItemRemove => Get("ItemRemove");
		
	/// <summary>
	/// Gets the incoming header for the Items (Unity/Flash) message.
	/// </summary>
	public Header Items => Get("Items");
		
	/// <summary>
	/// Gets the incoming header for the ItemsStateUpdate (Flash) message.
	/// </summary>
	public Header ItemsStateUpdate => Get("ItemsStateUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ItemStateUpdate (Flash) message.
	/// </summary>
	public Header ItemStateUpdate => Get("ItemStateUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ItemUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UpdateItem" />.
	/// </summary>
	public Header ItemUpdate => Get("ItemUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ObjectAdd (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ActiveObjectAdd" />.
	/// </summary>
	public Header ObjectAdd => Get("ObjectAdd");
		
	/// <summary>
	/// Gets the incoming header for the ObjectDataUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.StuffDataUpdate" />.
	/// </summary>
	public Header ObjectDataUpdate => Get("ObjectDataUpdate");
		
	/// <summary>
	/// Gets the incoming header for the ObjectRemove (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ActiveObjectRemove" />.
	/// </summary>
	public Header ObjectRemove => Get("ObjectRemove");
		
	/// <summary>
	/// Gets the incoming header for the ObjectsDataUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.MultipleStuffDataUpdate" />.
	/// </summary>
	public Header ObjectsDataUpdate => Get("ObjectsDataUpdate");
		
	/// <summary>
	/// Gets the incoming header for the Objects (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ActiveObjects" />.
	/// </summary>
	public Header Objects => Get("Objects");
		
	/// <summary>
	/// Gets the incoming header for the ObjectUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ActiveObjectUpdate" />.
	/// </summary>
	public Header ObjectUpdate => Get("ObjectUpdate");
		
	/// <summary>
	/// Gets the incoming header for the RoomEntryInfo (Unity/Flash) message.
	/// </summary>
	public Header RoomEntryInfo => Get("RoomEntryInfo");
		
	/// <summary>
	/// Gets the incoming header for the RoomProperty (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.FlatProperty" />.
	/// </summary>
	public Header RoomProperty => Get("RoomProperty");
		
	/// <summary>
	/// Gets the incoming header for the RoomVisualizationSettings (Unity/Flash) message.
	/// </summary>
	public Header RoomVisualizationSettings => Get("RoomVisualizationSettings");
		
	/// <summary>
	/// Gets the incoming header for the SlideObjectBundle (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.QueueMoveUpdate" />.
	/// </summary>
	public Header SlideObjectBundle => Get("SlideObjectBundle");
		
	/// <summary>
	/// Gets the incoming header for the SpecialRoomEffect (Unity/Flash) message.
	/// </summary>
	public Header SpecialRoomEffect => Get("SpecialRoomEffect");
		
	/// <summary>
	/// Gets the incoming header for the UserChange (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UpdateAvatar" />.
	/// </summary>
	public Header UserChange => Get("UserChange");
		
	/// <summary>
	/// Gets the incoming header for the UserRemove (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UserLoggedOut" />.
	/// </summary>
	public Header UserRemove => Get("UserRemove");
		
	/// <summary>
	/// Gets the incoming header for the Users (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UsersInRoom" />.
	/// </summary>
	public Header Users => Get("Users");
		
	/// <summary>
	/// Gets the incoming header for the UserUpdate (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Status" />.
	/// </summary>
	public Header UserUpdate => Get("UserUpdate");
		
	/// <summary>
	/// Gets the incoming header for the WiredMovements (Flash) message.
	/// </summary>
	public Header WiredMovements => Get("WiredMovements");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorCollapsedCategories (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2UserCollapsedCategories" />.
	/// </summary>
	public Header NavigatorCollapsedCategories => Get("NavigatorCollapsedCategories");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorLiftedRooms (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2LiftArea" />.
	/// </summary>
	public Header NavigatorLiftedRooms => Get("NavigatorLiftedRooms");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorMetaData (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2MetaData" />.
	/// </summary>
	public Header NavigatorMetaData => Get("NavigatorMetaData");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorSavedSearches (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2UserSavedSearches" />.
	/// </summary>
	public Header NavigatorSavedSearches => Get("NavigatorSavedSearches");
		
	/// <summary>
	/// Gets the incoming header for the NavigatorSearchResultBlocks (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2SearchResultBlocks" />.
	/// </summary>
	public Header NavigatorSearchResultBlocks => Get("NavigatorSearchResultBlocks");
		
	/// <summary>
	/// Gets the incoming header for the NewNavigatorPreferences (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Navigator2UserPreferences" />.
	/// </summary>
	public Header NewNavigatorPreferences => Get("NewNavigatorPreferences");
		
	/// <summary>
	/// Gets the incoming header for the CommunityVoteReceived (Flash) message.
	/// </summary>
	public Header CommunityVoteReceived => Get("CommunityVoteReceived");
		
	/// <summary>
	/// Gets the incoming header for the Open (Flash) message.
	/// </summary>
	public Header Open => Get("Open");
		
	/// <summary>
	/// Gets the incoming header for the WiredFurniAction (Flash) message.
	/// </summary>
	public Header WiredFurniAction => Get("WiredFurniAction");
		
	/// <summary>
	/// Gets the incoming header for the WiredFurniAddon (Flash) message.
	/// </summary>
	public Header WiredFurniAddon => Get("WiredFurniAddon");
		
	/// <summary>
	/// Gets the incoming header for the WiredFurniCondition (Flash) message.
	/// </summary>
	public Header WiredFurniCondition => Get("WiredFurniCondition");
		
	/// <summary>
	/// Gets the incoming header for the WiredFurniSelector (Flash) message.
	/// </summary>
	public Header WiredFurniSelector => Get("WiredFurniSelector");
		
	/// <summary>
	/// Gets the incoming header for the WiredFurniTrigger (Flash) message.
	/// </summary>
	public Header WiredFurniTrigger => Get("WiredFurniTrigger");
		
	/// <summary>
	/// Gets the incoming header for the WiredRewardResult (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UserDefinedRoomEventsRewardResult" />.
	/// </summary>
	public Header WiredRewardResult => Get("WiredRewardResult");
		
	/// <summary>
	/// Gets the incoming header for the WiredSaveSuccess (Flash) message.
	/// </summary>
	public Header WiredSaveSuccess => Get("WiredSaveSuccess");
		
	/// <summary>
	/// Gets the incoming header for the WiredValidationError (Flash) message.
	/// </summary>
	public Header WiredValidationError => Get("WiredValidationError");
		
	/// <summary>
	/// Gets the incoming header for the YouAreController (Unity/Flash) message.
	/// </summary>
	public Header YouAreController => Get("YouAreController");
		
	/// <summary>
	/// Gets the incoming header for the YouAreNotController (Unity/Flash) message.
	/// </summary>
	public Header YouAreNotController => Get("YouAreNotController");
		
	/// <summary>
	/// Gets the incoming header for the YouAreOwner (Unity/Flash) message.
	/// </summary>
	public Header YouAreOwner => Get("YouAreOwner");
		
	/// <summary>
	/// Gets the incoming header for the Game2AccountGameStatus (Flash) message.
	/// </summary>
	public Header Game2AccountGameStatus => Get("Game2AccountGameStatus");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameCancelled (Flash) message.
	/// </summary>
	public Header Game2GameCancelled => Get("Game2GameCancelled");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameCreated (Flash) message.
	/// </summary>
	public Header Game2GameCreated => Get("Game2GameCreated");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameDirectoryStatus (Flash) message.
	/// </summary>
	public Header Game2GameDirectoryStatus => Get("Game2GameDirectoryStatus");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameLongData (Flash) message.
	/// </summary>
	public Header Game2GameLongData => Get("Game2GameLongData");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameStarted (Flash) message.
	/// </summary>
	public Header Game2GameStarted => Get("Game2GameStarted");
		
	/// <summary>
	/// Gets the incoming header for the Game2InArenaQueue (Flash) message.
	/// </summary>
	public Header Game2InArenaQueue => Get("Game2InArenaQueue");
		
	/// <summary>
	/// Gets the incoming header for the Game2JoiningGameFailed (Flash) message.
	/// </summary>
	public Header Game2JoiningGameFailed => Get("Game2JoiningGameFailed");
		
	/// <summary>
	/// Gets the incoming header for the Game2StartCounter (Flash) message.
	/// </summary>
	public Header Game2StartCounter => Get("Game2StartCounter");
		
	/// <summary>
	/// Gets the incoming header for the Game2StartingGameFailed (Flash) message.
	/// </summary>
	public Header Game2StartingGameFailed => Get("Game2StartingGameFailed");
		
	/// <summary>
	/// Gets the incoming header for the Game2StopCounter (Flash) message.
	/// </summary>
	public Header Game2StopCounter => Get("Game2StopCounter");
		
	/// <summary>
	/// Gets the incoming header for the Game2UserBlocked (Flash) message.
	/// </summary>
	public Header Game2UserBlocked => Get("Game2UserBlocked");
		
	/// <summary>
	/// Gets the incoming header for the Game2UserJoinedGame (Flash) message.
	/// </summary>
	public Header Game2UserJoinedGame => Get("Game2UserJoinedGame");
		
	/// <summary>
	/// Gets the incoming header for the Game2UserLeftGame (Flash) message.
	/// </summary>
	public Header Game2UserLeftGame => Get("Game2UserLeftGame");
		
	/// <summary>
	/// Gets the incoming header for the AccountSafetyLockStatusChange (Flash) message.
	/// </summary>
	public Header AccountSafetyLockStatusChange => Get("AccountSafetyLockStatusChange");
		
	/// <summary>
	/// Gets the incoming header for the ApproveName (Flash) message.
	/// </summary>
	public Header ApproveName => Get("ApproveName");
		
	/// <summary>
	/// Gets the incoming header for the ChangeEmailResult (Flash) message.
	/// </summary>
	public Header ChangeEmailResult => Get("ChangeEmailResult");
		
	/// <summary>
	/// Gets the incoming header for the EmailStatusResult (Flash) message.
	/// </summary>
	public Header EmailStatusResult => Get("EmailStatusResult");
		
	/// <summary>
	/// Gets the incoming header for the ExtendedProfileChanged (Unity/Flash) message.
	/// </summary>
	public Header ExtendedProfileChanged => Get("ExtendedProfileChanged");
		
	/// <summary>
	/// Gets the incoming header for the ExtendedProfile (Unity/Flash) message.
	/// </summary>
	public Header ExtendedProfile => Get("ExtendedProfile");
		
	/// <summary>
	/// Gets the incoming header for the GroupDetailsChanged (Unity/Flash) message.
	/// </summary>
	public Header GroupDetailsChanged => Get("GroupDetailsChanged");
		
	/// <summary>
	/// Gets the incoming header for the GroupMembershipRequested (Unity/Flash) message.
	/// </summary>
	public Header GroupMembershipRequested => Get("GroupMembershipRequested");
		
	/// <summary>
	/// Gets the incoming header for the GuildCreated (Unity/Flash) message.
	/// </summary>
	public Header GuildCreated => Get("GuildCreated");
		
	/// <summary>
	/// Gets the incoming header for the GuildCreationInfo (Unity/Flash) message.
	/// </summary>
	public Header GuildCreationInfo => Get("GuildCreationInfo");
		
	/// <summary>
	/// Gets the incoming header for the GuildEditFailed (Unity/Flash) message.
	/// </summary>
	public Header GuildEditFailed => Get("GuildEditFailed");
		
	/// <summary>
	/// Gets the incoming header for the GuildEditInfo (Unity/Flash) message.
	/// </summary>
	public Header GuildEditInfo => Get("GuildEditInfo");
		
	/// <summary>
	/// Gets the incoming header for the GuildEditorData (Unity/Flash) message.
	/// </summary>
	public Header GuildEditorData => Get("GuildEditorData");
		
	/// <summary>
	/// Gets the incoming header for the GuildMemberFurniCountInHq (Unity/Flash) message.
	/// </summary>
	public Header GuildMemberFurniCountInHq => Get("GuildMemberFurniCountInHq");
		
	/// <summary>
	/// Gets the incoming header for the GuildMemberMgmtFailed (Unity/Flash) message.
	/// </summary>
	public Header GuildMemberMgmtFailed => Get("GuildMemberMgmtFailed");
		
	/// <summary>
	/// Gets the incoming header for the GuildMembershipRejected (Unity/Flash) message.
	/// </summary>
	public Header GuildMembershipRejected => Get("GuildMembershipRejected");
		
	/// <summary>
	/// Gets the incoming header for the GuildMemberships (Unity/Flash) message.
	/// </summary>
	public Header GuildMemberships => Get("GuildMemberships");
		
	/// <summary>
	/// Gets the incoming header for the GuildMembershipUpdated (Unity/Flash) message.
	/// </summary>
	public Header GuildMembershipUpdated => Get("GuildMembershipUpdated");
		
	/// <summary>
	/// Gets the incoming header for the GuildMembers (Unity/Flash) message.
	/// </summary>
	public Header GuildMembers => Get("GuildMembers");
		
	/// <summary>
	/// Gets the incoming header for the HabboGroupBadges (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.HabboGroupBadge" />.
	/// </summary>
	public Header HabboGroupBadges => Get("HabboGroupBadges");
		
	/// <summary>
	/// Gets the incoming header for the HabboGroupDeactivated (Unity/Flash) message.
	/// </summary>
	public Header HabboGroupDeactivated => Get("HabboGroupDeactivated");
		
	/// <summary>
	/// Gets the incoming header for the HabboGroupDetails (Unity/Flash) message.
	/// </summary>
	public Header HabboGroupDetails => Get("HabboGroupDetails");
		
	/// <summary>
	/// Gets the incoming header for the HabboGroupJoinFailed (Unity/Flash) message.
	/// </summary>
	public Header HabboGroupJoinFailed => Get("HabboGroupJoinFailed");
		
	/// <summary>
	/// Gets the incoming header for the HabboUserBadges (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.SelectedBadges" />.
	/// </summary>
	public Header HabboUserBadges => Get("HabboUserBadges");
		
	/// <summary>
	/// Gets the incoming header for the HandItemReceived (Unity/Flash) message.
	/// </summary>
	public Header HandItemReceived => Get("HandItemReceived");
		
	/// <summary>
	/// Gets the incoming header for the IgnoredUsers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.RequestIgnoreList" />.
	/// </summary>
	public Header IgnoredUsers => Get("IgnoredUsers");
		
	/// <summary>
	/// Gets the incoming header for the IgnoreResult (Flash) message.
	/// </summary>
	public Header IgnoreResult => Get("IgnoreResult");
		
	/// <summary>
	/// Gets the incoming header for the InClientLink (Unity/Flash) message.
	/// </summary>
	public Header InClientLink => Get("InClientLink");
		
	/// <summary>
	/// Gets the incoming header for the PetRespectNotification (Unity/Flash) message.
	/// </summary>
	public Header PetRespectNotification => Get("PetRespectNotification");
		
	/// <summary>
	/// Gets the incoming header for the PetSupplementedNotification (Unity/Flash) message.
	/// </summary>
	public Header PetSupplementedNotification => Get("PetSupplementedNotification");
		
	/// <summary>
	/// Gets the incoming header for the RelationshipStatusInfo (Unity/Flash) message.
	/// </summary>
	public Header RelationshipStatusInfo => Get("RelationshipStatusInfo");
		
	/// <summary>
	/// Gets the incoming header for the RespectNotification (Unity/Flash) message.
	/// </summary>
	public Header RespectNotification => Get("RespectNotification");
		
	/// <summary>
	/// Gets the incoming header for the ScrSendKickbackInfo (Unity/Flash) message.
	/// </summary>
	public Header ScrSendKickbackInfo => Get("ScrSendKickbackInfo");
		
	/// <summary>
	/// Gets the incoming header for the ScrSendUserInfo (Unity/Flash) message.
	/// </summary>
	public Header ScrSendUserInfo => Get("ScrSendUserInfo");
		
	/// <summary>
	/// Gets the incoming header for the UserNameChanged (Unity/Flash) message.
	/// </summary>
	public Header UserNameChanged => Get("UserNameChanged");
		
	/// <summary>
	/// Gets the incoming header for the PetBreedingResult (Unity/Flash) message.
	/// </summary>
	public Header PetBreedingResult => Get("PetBreedingResult");
		
	/// <summary>
	/// Gets the incoming header for the PetCommands (Unity/Flash) message.
	/// </summary>
	public Header PetCommands => Get("PetCommands");
		
	/// <summary>
	/// Gets the incoming header for the PetExperience (Unity/Flash) message.
	/// </summary>
	public Header PetExperience => Get("PetExperience");
		
	/// <summary>
	/// Gets the incoming header for the PetFigureUpdate (Flash) message.
	/// </summary>
	public Header PetFigureUpdate => Get("PetFigureUpdate");
		
	/// <summary>
	/// Gets the incoming header for the PetInfo (Unity/Flash) message.
	/// </summary>
	public Header PetInfo => Get("PetInfo");
		
	/// <summary>
	/// Gets the incoming header for the PetLevelUpdate (Unity/Flash) message.
	/// </summary>
	public Header PetLevelUpdate => Get("PetLevelUpdate");
		
	/// <summary>
	/// Gets the incoming header for the PetPlacingError (Flash) message.
	/// </summary>
	public Header PetPlacingError => Get("PetPlacingError");
		
	/// <summary>
	/// Gets the incoming header for the PetRespectFailed (Unity/Flash) message.
	/// </summary>
	public Header PetRespectFailed => Get("PetRespectFailed");
		
	/// <summary>
	/// Gets the incoming header for the PetStatusUpdate (Unity/Flash) message.
	/// </summary>
	public Header PetStatusUpdate => Get("PetStatusUpdate");
		
	/// <summary>
	/// Gets the incoming header for the AchievementResolutionCompleted (Flash) message.
	/// </summary>
	public Header AchievementResolutionCompleted => Get("AchievementResolutionCompleted");
		
	/// <summary>
	/// Gets the incoming header for the AchievementResolutionProgress (Flash) message.
	/// </summary>
	public Header AchievementResolutionProgress => Get("AchievementResolutionProgress");
		
	/// <summary>
	/// Gets the incoming header for the AchievementResolutions (Flash) message.
	/// </summary>
	public Header AchievementResolutions => Get("AchievementResolutions");
		
	/// <summary>
	/// Gets the incoming header for the ActivityPoints (Unity/Flash) message.
	/// </summary>
	public Header ActivityPoints => Get("ActivityPoints");
		
	/// <summary>
	/// Gets the incoming header for the ClubGiftNotification (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.CSubscriptionUserGifts" />.
	/// </summary>
	public Header ClubGiftNotification => Get("ClubGiftNotification");
		
	/// <summary>
	/// Gets the incoming header for the ElementPointer (Flash) message.
	/// </summary>
	public Header ElementPointer => Get("ElementPointer");
		
	/// <summary>
	/// Gets the incoming header for the HabboAchievementNotification (Flash) message.
	/// </summary>
	public Header HabboAchievementNotification => Get("HabboAchievementNotification");
		
	/// <summary>
	/// Gets the incoming header for the HabboActivityPointNotification (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ActivityPointNotification" />.
	/// </summary>
	public Header HabboActivityPointNotification => Get("HabboActivityPointNotification");
		
	/// <summary>
	/// Gets the incoming header for the HabboBroadcast (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.SystemBroadcast" />.
	/// </summary>
	public Header HabboBroadcast => Get("HabboBroadcast");
		
	/// <summary>
	/// Gets the incoming header for the InfoFeedEnable (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.ClientInfoFeedEnabled" />.
	/// </summary>
	public Header InfoFeedEnable => Get("InfoFeedEnable");
		
	/// <summary>
	/// Gets the incoming header for the MOTDNotification (Flash) message.
	/// </summary>
	public Header MOTDNotification => Get("MOTDNotification");
		
	/// <summary>
	/// Gets the incoming header for the NotificationDialog (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.Notification" />.
	/// </summary>
	public Header NotificationDialog => Get("NotificationDialog");
		
	/// <summary>
	/// Gets the incoming header for the OfferRewardDelivered (Unity/Flash) message.
	/// </summary>
	public Header OfferRewardDelivered => Get("OfferRewardDelivered");
		
	/// <summary>
	/// Gets the incoming header for the PetLevelNotification (Unity/Flash) message.
	/// </summary>
	public Header PetLevelNotification => Get("PetLevelNotification");
		
	/// <summary>
	/// Gets the incoming header for the RestoreClient (Flash) message.
	/// </summary>
	public Header RestoreClient => Get("RestoreClient");
		
	/// <summary>
	/// Gets the incoming header for the UnseenItems (Flash) message.
	/// The Unity equivalent for this message is <see cref="Incoming.UnseenElements" />.
	/// </summary>
	public Header UnseenItems => Get("UnseenItems");
		
	/// <summary>
	/// Gets the incoming header for the Game2ArenaEntered (Flash) message.
	/// </summary>
	public Header Game2ArenaEntered => Get("Game2ArenaEntered");
		
	/// <summary>
	/// Gets the incoming header for the Game2EnterArenaFailed (Flash) message.
	/// </summary>
	public Header Game2EnterArenaFailed => Get("Game2EnterArenaFailed");
		
	/// <summary>
	/// Gets the incoming header for the Game2EnterArena (Flash) message.
	/// </summary>
	public Header Game2EnterArena => Get("Game2EnterArena");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameChatFromPlayer (Flash) message.
	/// </summary>
	public Header Game2GameChatFromPlayer => Get("Game2GameChatFromPlayer");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameEnding (Flash) message.
	/// </summary>
	public Header Game2GameEnding => Get("Game2GameEnding");
		
	/// <summary>
	/// Gets the incoming header for the Game2GameRejoin (Flash) message.
	/// </summary>
	public Header Game2GameRejoin => Get("Game2GameRejoin");
		
	/// <summary>
	/// Gets the incoming header for the Game2PlayerExitedGameArena (Flash) message.
	/// </summary>
	public Header Game2PlayerExitedGameArena => Get("Game2PlayerExitedGameArena");
		
	/// <summary>
	/// Gets the incoming header for the Game2PlayerRematches (Flash) message.
	/// </summary>
	public Header Game2PlayerRematches => Get("Game2PlayerRematches");
		
	/// <summary>
	/// Gets the incoming header for the Game2StageEnding (Flash) message.
	/// </summary>
	public Header Game2StageEnding => Get("Game2StageEnding");
		
	/// <summary>
	/// Gets the incoming header for the Game2StageLoad (Flash) message.
	/// </summary>
	public Header Game2StageLoad => Get("Game2StageLoad");
		
	/// <summary>
	/// Gets the incoming header for the Game2StageRunning (Flash) message.
	/// </summary>
	public Header Game2StageRunning => Get("Game2StageRunning");
		
	/// <summary>
	/// Gets the incoming header for the Game2StageStarting (Flash) message.
	/// </summary>
	public Header Game2StageStarting => Get("Game2StageStarting");
		
	/// <summary>
	/// Gets the incoming header for the Game2StageStillLoading (Flash) message.
	/// </summary>
	public Header Game2StageStillLoading => Get("Game2StageStillLoading");
		
	/// <summary>
	/// Gets the incoming header for the LegacyBannerPublicKey (Unity) message.
	/// </summary>
	public Header LegacyBannerPublicKey => Get("LegacyBannerPublicKey");
		
	/// <summary>
	/// Gets the incoming header for the ClubAndSecurityLevels (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserRights" />.
	/// </summary>
	public Header ClubAndSecurityLevels => Get("ClubAndSecurityLevels");
		
	/// <summary>
	/// Gets the incoming header for the Ok (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.AuthenticationOK" />.
	/// </summary>
	public Header Ok => Get("Ok");
		
	/// <summary>
	/// Gets the incoming header for the Film (Unity) message.
	/// </summary>
	public Header Film => Get("Film");
		
	/// <summary>
	/// Gets the incoming header for the WalletBalance (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.CreditBalance" />.
	/// </summary>
	public Header WalletBalance => Get("WalletBalance");
		
	/// <summary>
	/// Gets the incoming header for the Token (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header Token => Get("Token");
		
	/// <summary>
	/// Gets the incoming header for the OpenConnectionConfirmation (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.OpenConnection" />.
	/// </summary>
	public Header OpenConnectionConfirmation => Get("OpenConnectionConfirmation");
		
	/// <summary>
	/// Gets the incoming header for the NoLoginPermission (Unity) message.
	/// </summary>
	public Header NoLoginPermission => Get("NoLoginPermission");
		
	/// <summary>
	/// Gets the incoming header for the RoomExitReason (Unity) message.
	/// </summary>
	public Header RoomExitReason => Get("RoomExitReason");
		
	/// <summary>
	/// Gets the incoming header for the DeleteFlatResult (Unity) message.
	/// </summary>
	public Header DeleteFlatResult => Get("DeleteFlatResult");
		
	/// <summary>
	/// Gets the incoming header for the UsersInRoom (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Users" />.
	/// </summary>
	public Header UsersInRoom => Get("UsersInRoom");
		
	/// <summary>
	/// Gets the incoming header for the UserLoggedOut (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserRemove" />.
	/// </summary>
	public Header UserLoggedOut => Get("UserLoggedOut");
		
	/// <summary>
	/// Gets the incoming header for the StackingHeightmap (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HeightMap" />.
	/// </summary>
	public Header StackingHeightmap => Get("StackingHeightmap");
		
	/// <summary>
	/// Gets the incoming header for the ActiveObjects (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Objects" />.
	/// </summary>
	public Header ActiveObjects => Get("ActiveObjects");
		
	/// <summary>
	/// Gets the incoming header for the Error (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.GenericError" />.
	/// </summary>
	public Header Error => Get("Error");
		
	/// <summary>
	/// Gets the incoming header for the Status (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserUpdate" />.
	/// </summary>
	public Header Status => Get("Status");
		
	/// <summary>
	/// Gets the incoming header for the PetNameApproved (Unity) message.
	/// </summary>
	public Header PetNameApproved => Get("PetNameApproved");
		
	/// <summary>
	/// Gets the incoming header for the FlatProperty (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.RoomProperty" />.
	/// </summary>
	public Header FlatProperty => Get("FlatProperty");
		
	/// <summary>
	/// Gets the incoming header for the ItemData (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ItemDataUpdate" />.
	/// </summary>
	public Header ItemData => Get("ItemData");
		
	/// <summary>
	/// Gets the incoming header for the ServiceIsClosed (Unity) message.
	/// </summary>
	public Header ServiceIsClosed => Get("ServiceIsClosed");
		
	/// <summary>
	/// Gets the incoming header for the RegOk (Unity) message.
	/// </summary>
	public Header RegOk => Get("RegOk");
		
	/// <summary>
	/// Gets the incoming header for the PickedUpPetFromRoom (Unity) message.
	/// </summary>
	public Header PickedUpPetFromRoom => Get("PickedUpPetFromRoom");
		
	/// <summary>
	/// Gets the incoming header for the SubFlatCreated (Unity) message.
	/// </summary>
	public Header SubFlatCreated => Get("SubFlatCreated");
		
	/// <summary>
	/// Gets the incoming header for the PickedUpAllItems (Unity) message.
	/// </summary>
	public Header PickedUpAllItems => Get("PickedUpAllItems");
		
	/// <summary>
	/// Gets the incoming header for the DoorFlat (Unity) message.
	/// </summary>
	public Header DoorFlat => Get("DoorFlat");
		
	/// <summary>
	/// Gets the incoming header for the DoorOtherEndDeleted (Unity) message.
	/// </summary>
	public Header DoorOtherEndDeleted => Get("DoorOtherEndDeleted");
		
	/// <summary>
	/// Gets the incoming header for the DoorNotInstalled (Unity) message.
	/// </summary>
	public Header DoorNotInstalled => Get("DoorNotInstalled");
		
	/// <summary>
	/// Gets the incoming header for the CatalogUrl (Unity) message.
	/// </summary>
	public Header CatalogUrl => Get("CatalogUrl");
		
	/// <summary>
	/// Gets the incoming header for the YouAreModerator (Unity) message.
	/// </summary>
	public Header YouAreModerator => Get("YouAreModerator");
		
	/// <summary>
	/// Gets the incoming header for the PhTickets (Unity) message.
	/// </summary>
	public Header PhTickets => Get("PhTickets");
		
	/// <summary>
	/// Gets the incoming header for the PhNoTickets (Unity) message.
	/// </summary>
	public Header PhNoTickets => Get("PhNoTickets");
		
	/// <summary>
	/// Gets the incoming header for the JumpData (Unity) message.
	/// </summary>
	public Header JumpData => Get("JumpData");
		
	/// <summary>
	/// Gets the incoming header for the JumpDataSaved (Unity) message.
	/// </summary>
	public Header JumpDataSaved => Get("JumpDataSaved");
		
	/// <summary>
	/// Gets the incoming header for the UserNotFound (Unity) message.
	/// </summary>
	public Header UserNotFound => Get("UserNotFound");
		
	/// <summary>
	/// Gets the incoming header for the JumpsForUser (Unity) message.
	/// </summary>
	public Header JumpsForUser => Get("JumpsForUser");
		
	/// <summary>
	/// Gets the incoming header for the GotoJumpingPlace (Unity) message.
	/// </summary>
	public Header GotoJumpingPlace => Get("GotoJumpingPlace");
		
	/// <summary>
	/// Gets the incoming header for the OwnerPresence (Unity) message.
	/// </summary>
	public Header OwnerPresence => Get("OwnerPresence");
		
	/// <summary>
	/// Gets the incoming header for the AddItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ItemAdd" />.
	/// </summary>
	public Header AddItem => Get("AddItem");
		
	/// <summary>
	/// Gets the incoming header for the RemoveItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ItemRemove" />.
	/// </summary>
	public Header RemoveItem => Get("RemoveItem");
		
	/// <summary>
	/// Gets the incoming header for the UpdateItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ItemUpdate" />.
	/// </summary>
	public Header UpdateItem => Get("UpdateItem");
		
	/// <summary>
	/// Gets the incoming header for the StuffDataUpdate (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ObjectDataUpdate" />.
	/// </summary>
	public Header StuffDataUpdate => Get("StuffDataUpdate");
		
	/// <summary>
	/// Gets the incoming header for the DoorOut (Unity) message.
	/// </summary>
	public Header DoorOut => Get("DoorOut");
		
	/// <summary>
	/// Gets the incoming header for the DoorbellRinging (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Doorbell" />.
	/// </summary>
	public Header DoorbellRinging => Get("DoorbellRinging");
		
	/// <summary>
	/// Gets the incoming header for the DoorIn (Unity) message.
	/// </summary>
	public Header DoorIn => Get("DoorIn");
		
	/// <summary>
	/// Gets the incoming header for the ActiveObjectAdd (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ObjectAdd" />.
	/// </summary>
	public Header ActiveObjectAdd => Get("ActiveObjectAdd");
		
	/// <summary>
	/// Gets the incoming header for the ActiveObjectRemove (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ObjectRemove" />.
	/// </summary>
	public Header ActiveObjectRemove => Get("ActiveObjectRemove");
		
	/// <summary>
	/// Gets the incoming header for the ActiveObjectUpdate (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ObjectUpdate" />.
	/// </summary>
	public Header ActiveObjectUpdate => Get("ActiveObjectUpdate");
		
	/// <summary>
	/// Gets the incoming header for the InventoryAddOrUpdateFurni (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FurniListAddOrUpdate" />.
	/// </summary>
	public Header InventoryAddOrUpdateFurni => Get("InventoryAddOrUpdateFurni");
		
	/// <summary>
	/// Gets the incoming header for the InventoryRemoveFurni (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FurniListRemove" />.
	/// </summary>
	public Header InventoryRemoveFurni => Get("InventoryRemoveFurni");
		
	/// <summary>
	/// Gets the incoming header for the InventoryInvalidate (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FurniListInvalidate" />.
	/// </summary>
	public Header InventoryInvalidate => Get("InventoryInvalidate");
		
	/// <summary>
	/// Gets the incoming header for the TradeYouAreNotAllowed (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingYouAreNotAllowed" />.
	/// </summary>
	public Header TradeYouAreNotAllowed => Get("TradeYouAreNotAllowed");
		
	/// <summary>
	/// Gets the incoming header for the TradeOtherNotAllowed (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingOtherNotAllowed" />.
	/// </summary>
	public Header TradeOtherNotAllowed => Get("TradeOtherNotAllowed");
		
	/// <summary>
	/// Gets the incoming header for the TradeOpen (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingOpen" />.
	/// </summary>
	public Header TradeOpen => Get("TradeOpen");
		
	/// <summary>
	/// Gets the incoming header for the TradeOpenFail (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradeOpenFailed" />.
	/// </summary>
	public Header TradeOpenFail => Get("TradeOpenFail");
		
	/// <summary>
	/// Gets the incoming header for the TradeNoTradeOpen (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingNotOpen" />.
	/// </summary>
	public Header TradeNoTradeOpen => Get("TradeNoTradeOpen");
		
	/// <summary>
	/// Gets the incoming header for the TradeNoSuchItem (Unity) message.
	/// </summary>
	public Header TradeNoSuchItem => Get("TradeNoSuchItem");
		
	/// <summary>
	/// Gets the incoming header for the TradeItems (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingItemList" />.
	/// </summary>
	public Header TradeItems => Get("TradeItems");
		
	/// <summary>
	/// Gets the incoming header for the TradeAccept (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingAccept" />.
	/// </summary>
	public Header TradeAccept => Get("TradeAccept");
		
	/// <summary>
	/// Gets the incoming header for the TradeClose (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingClose" />.
	/// </summary>
	public Header TradeClose => Get("TradeClose");
		
	/// <summary>
	/// Gets the incoming header for the TradeConfirmation (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingConfirmation" />.
	/// </summary>
	public Header TradeConfirmation => Get("TradeConfirmation");
		
	/// <summary>
	/// Gets the incoming header for the TradeCompleted (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.TradingCompleted" />.
	/// </summary>
	public Header TradeCompleted => Get("TradeCompleted");
		
	/// <summary>
	/// Gets the incoming header for the Trigger (Unity) message.
	/// </summary>
	public Header Trigger => Get("Trigger");
		
	/// <summary>
	/// Gets the incoming header for the TeleportRelinkResponse (Unity) message.
	/// </summary>
	public Header TeleportRelinkResponse => Get("TeleportRelinkResponse");
		
	/// <summary>
	/// Gets the incoming header for the DonationSettings (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header DonationSettings => Get("DonationSettings");
		
	/// <summary>
	/// Gets the incoming header for the DonateResult (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header DonateResult => Get("DonateResult");
		
	/// <summary>
	/// Gets the incoming header for the PhLiftDoorOpen (Unity) message.
	/// </summary>
	public Header PhLiftDoorOpen => Get("PhLiftDoorOpen");
		
	/// <summary>
	/// Gets the incoming header for the PhLiftDoorClose (Unity) message.
	/// </summary>
	public Header PhLiftDoorClose => Get("PhLiftDoorClose");
		
	/// <summary>
	/// Gets the incoming header for the PhTicketsBuy (Unity) message.
	/// </summary>
	public Header PhTicketsBuy => Get("PhTicketsBuy");
		
	/// <summary>
	/// Gets the incoming header for the PhJumpingPlaceOk (Unity) message.
	/// </summary>
	public Header PhJumpingPlaceOk => Get("PhJumpingPlaceOk");
		
	/// <summary>
	/// Gets the incoming header for the MemberInfo (Unity) message.
	/// </summary>
	public Header MemberInfo => Get("MemberInfo");
		
	/// <summary>
	/// Gets the incoming header for the PresentOpen (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.PresentOpened" />.
	/// </summary>
	public Header PresentOpen => Get("PresentOpen");
		
	/// <summary>
	/// Gets the incoming header for the FlatPasswordOk (Unity) message.
	/// </summary>
	public Header FlatPasswordOk => Get("FlatPasswordOk");
		
	/// <summary>
	/// Gets the incoming header for the NewBuddyRequest (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NewFriendRequest" />.
	/// </summary>
	public Header NewBuddyRequest => Get("NewBuddyRequest");
		
	/// <summary>
	/// Gets the incoming header for the CancelBuddyRequest (Unity) message.
	/// </summary>
	public Header CancelBuddyRequest => Get("CancelBuddyRequest");
		
	/// <summary>
	/// Gets the incoming header for the MessengerNewConsoleMessage (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NewConsole" />.
	/// </summary>
	public Header MessengerNewConsoleMessage => Get("MessengerNewConsoleMessage");
		
	/// <summary>
	/// Gets the incoming header for the MessengerRoomInvite (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.RoomInvite" />.
	/// </summary>
	public Header MessengerRoomInvite => Get("MessengerRoomInvite");
		
	/// <summary>
	/// Gets the incoming header for the SystemBroadcast (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HabboBroadcast" />.
	/// </summary>
	public Header SystemBroadcast => Get("SystemBroadcast");
		
	/// <summary>
	/// Gets the incoming header for the InventoryPush (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FurniList" />.
	/// </summary>
	public Header InventoryPush => Get("InventoryPush");
		
	/// <summary>
	/// Gets the incoming header for the MemberRoom (Unity) message.
	/// </summary>
	public Header MemberRoom => Get("MemberRoom");
		
	/// <summary>
	/// Gets the incoming header for the ServiceClosed (Unity) message.
	/// </summary>
	public Header ServiceClosed => Get("ServiceClosed");
		
	/// <summary>
	/// Gets the incoming header for the RoomUsers (Unity) message.
	/// </summary>
	public Header RoomUsers => Get("RoomUsers");
		
	/// <summary>
	/// Gets the incoming header for the NewUnit (Unity) message.
	/// </summary>
	public Header NewUnit => Get("NewUnit");
		
	/// <summary>
	/// Gets the incoming header for the UnitRemove (Unity) message.
	/// </summary>
	public Header UnitRemove => Get("UnitRemove");
		
	/// <summary>
	/// Gets the incoming header for the UnitUpdated (Unity) message.
	/// </summary>
	public Header UnitUpdated => Get("UnitUpdated");
		
	/// <summary>
	/// Gets the incoming header for the SystemDown (Unity) message.
	/// </summary>
	public Header SystemDown => Get("SystemDown");
		
	/// <summary>
	/// Gets the incoming header for the SystemOpened (Unity) message.
	/// </summary>
	public Header SystemOpened => Get("SystemOpened");
		
	/// <summary>
	/// Gets the incoming header for the UserProfile (Unity) message.
	/// </summary>
	public Header UserProfile => Get("UserProfile");
		
	/// <summary>
	/// Gets the incoming header for the UserMatch (Unity) message.
	/// </summary>
	public Header UserMatch => Get("UserMatch");
		
	/// <summary>
	/// Gets the incoming header for the CloseVotingDialog (Unity) message.
	/// </summary>
	public Header CloseVotingDialog => Get("CloseVotingDialog");
		
	/// <summary>
	/// Gets the incoming header for the UnitClosed (Unity) message.
	/// </summary>
	public Header UnitClosed => Get("UnitClosed");
		
	/// <summary>
	/// Gets the incoming header for the ModeratorMessage (Unity) message.
	/// </summary>
	public Header ModeratorMessage => Get("ModeratorMessage");
		
	/// <summary>
	/// Gets the incoming header for the AgeCheckResult (Unity) message.
	/// </summary>
	public Header AgeCheckResult => Get("AgeCheckResult");
		
	/// <summary>
	/// Gets the incoming header for the TrackingRequest (Unity) message.
	/// </summary>
	public Header TrackingRequest => Get("TrackingRequest");
		
	/// <summary>
	/// Gets the incoming header for the ReregistrationRequired (Unity) message.
	/// </summary>
	public Header ReregistrationRequired => Get("ReregistrationRequired");
		
	/// <summary>
	/// Gets the incoming header for the AccountUpdateStatus (Unity) message.
	/// </summary>
	public Header AccountUpdateStatus => Get("AccountUpdateStatus");
		
	/// <summary>
	/// Gets the incoming header for the PurchaseRoomAdResult (Unity) message.
	/// </summary>
	public Header PurchaseRoomAdResult => Get("PurchaseRoomAdResult");
		
	/// <summary>
	/// Gets the incoming header for the RoomAd (Unity) message.
	/// </summary>
	public Header RoomAd => Get("RoomAd");
		
	/// <summary>
	/// Gets the incoming header for the UserCreditTransactions (Unity) message.
	/// </summary>
	public Header UserCreditTransactions => Get("UserCreditTransactions");
		
	/// <summary>
	/// Gets the incoming header for the UpdateOk (Unity) message.
	/// </summary>
	public Header UpdateOk => Get("UpdateOk");
		
	/// <summary>
	/// Gets the incoming header for the StackingHeightmapDiff (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HeightMapUpdate" />.
	/// </summary>
	public Header StackingHeightmapDiff => Get("StackingHeightmapDiff");
		
	/// <summary>
	/// Gets the incoming header for the UserFlatCategories (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserFlatCats" />.
	/// </summary>
	public Header UserFlatCategories => Get("UserFlatCategories");
		
	/// <summary>
	/// Gets the incoming header for the EventFlatCategories (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserEventCats" />.
	/// </summary>
	public Header EventFlatCategories => Get("EventFlatCategories");
		
	/// <summary>
	/// Gets the incoming header for the CanNotConnect (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.CantConnect" />.
	/// </summary>
	public Header CanNotConnect => Get("CanNotConnect");
		
	/// <summary>
	/// Gets the incoming header for the SelectedBadges (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HabboUserBadges" />.
	/// </summary>
	public Header SelectedBadges => Get("SelectedBadges");
		
	/// <summary>
	/// Gets the incoming header for the AvailableBadges (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Badges" />.
	/// </summary>
	public Header AvailableBadges => Get("AvailableBadges");
		
	/// <summary>
	/// Gets the incoming header for the QueueMoveUpdate (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.SlideObjectBundle" />.
	/// </summary>
	public Header QueueMoveUpdate => Get("QueueMoveUpdate");
		
	/// <summary>
	/// Gets the incoming header for the FullGameStatus (Unity) message.
	/// </summary>
	public Header FullGameStatus => Get("FullGameStatus");
		
	/// <summary>
	/// Gets the incoming header for the GameStatus (Unity) message.
	/// </summary>
	public Header GameStatus => Get("GameStatus");
		
	/// <summary>
	/// Gets the incoming header for the LevelEditorNotification (Unity) message.
	/// </summary>
	public Header LevelEditorNotification => Get("LevelEditorNotification");
		
	/// <summary>
	/// Gets the incoming header for the SessionParams (Unity) message.
	/// </summary>
	public Header SessionParams => Get("SessionParams");
		
	/// <summary>
	/// Gets the incoming header for the BuddyList (Unity) message.
	/// </summary>
	public Header BuddyList => Get("BuddyList");
		
	/// <summary>
	/// Gets the incoming header for the EmailChangeResult (Unity) message.
	/// </summary>
	public Header EmailChangeResult => Get("EmailChangeResult");
		
	/// <summary>
	/// Gets the incoming header for the UpdateAvatar (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserChange" />.
	/// </summary>
	public Header UpdateAvatar => Get("UpdateAvatar");
		
	/// <summary>
	/// Gets the incoming header for the UserWardrobe (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Wardrobe" />.
	/// </summary>
	public Header UserWardrobe => Get("UserWardrobe");
		
	/// <summary>
	/// Gets the incoming header for the AccountSafetyLockStatus (Unity) message.
	/// </summary>
	public Header AccountSafetyLockStatus => Get("AccountSafetyLockStatus");
		
	/// <summary>
	/// Gets the incoming header for the EmailStatus (Unity) message.
	/// </summary>
	public Header EmailStatus => Get("EmailStatus");
		
	/// <summary>
	/// Gets the incoming header for the CallDeleted (Unity) message.
	/// </summary>
	public Header CallDeleted => Get("CallDeleted");
		
	/// <summary>
	/// Gets the incoming header for the CallReply (Unity) message.
	/// </summary>
	public Header CallReply => Get("CallReply");
		
	/// <summary>
	/// Gets the incoming header for the RegUpdateRequest (Unity) message.
	/// </summary>
	public Header RegUpdateRequest => Get("RegUpdateRequest");
		
	/// <summary>
	/// Gets the incoming header for the PartnerStatus (Unity) message.
	/// </summary>
	public Header PartnerStatus => Get("PartnerStatus");
		
	/// <summary>
	/// Gets the incoming header for the LegacyBannerHandshake (Unity) message.
	/// </summary>
	public Header LegacyBannerHandshake => Get("LegacyBannerHandshake");
		
	/// <summary>
	/// Gets the incoming header for the DhInitHandshake (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.InitDiffieHandshake" />.
	/// </summary>
	public Header DhInitHandshake => Get("DhInitHandshake");
		
	/// <summary>
	/// Gets the incoming header for the DhCompleteHandshake (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.CompleteDiffieHandshake" />.
	/// </summary>
	public Header DhCompleteHandshake => Get("DhCompleteHandshake");
		
	/// <summary>
	/// Gets the incoming header for the CSubscriptionUserGifts (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ClubGiftNotification" />.
	/// </summary>
	public Header CSubscriptionUserGifts => Get("CSubscriptionUserGifts");
		
	/// <summary>
	/// Gets the incoming header for the AccountSafetyLockQuestions (Unity) message.
	/// </summary>
	public Header AccountSafetyLockQuestions => Get("AccountSafetyLockQuestions");
		
	/// <summary>
	/// Gets the incoming header for the SpectatingEnded (Unity) message.
	/// </summary>
	public Header SpectatingEnded => Get("SpectatingEnded");
		
	/// <summary>
	/// Gets the incoming header for the AvailabilityTime (Unity) message.
	/// </summary>
	public Header AvailabilityTime => Get("AvailabilityTime");
		
	/// <summary>
	/// Gets the incoming header for the FurniAliases (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FurnitureAliases" />.
	/// </summary>
	public Header FurniAliases => Get("FurniAliases");
		
	/// <summary>
	/// Gets the incoming header for the SpectatorAmount (Unity) message.
	/// </summary>
	public Header SpectatorAmount => Get("SpectatorAmount");
		
	/// <summary>
	/// Gets the incoming header for the SongInfo (Unity) message.
	/// </summary>
	public Header SongInfo => Get("SongInfo");
		
	/// <summary>
	/// Gets the incoming header for the MachineSoundPackages (Unity) message.
	/// </summary>
	public Header MachineSoundPackages => Get("MachineSoundPackages");
		
	/// <summary>
	/// Gets the incoming header for the UserSoundPackages (Unity) message.
	/// </summary>
	public Header UserSoundPackages => Get("UserSoundPackages");
		
	/// <summary>
	/// Gets the incoming header for the SongId (Unity) message.
	/// </summary>
	public Header SongId => Get("SongId");
		
	/// <summary>
	/// Gets the incoming header for the HabboGroupBadge (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HabboGroupBadges" />.
	/// </summary>
	public Header HabboGroupBadge => Get("HabboGroupBadge");
		
	/// <summary>
	/// Gets the incoming header for the OneWayDoorStatusChange (Unity) message.
	/// </summary>
	public Header OneWayDoorStatusChange => Get("OneWayDoorStatusChange");
		
	/// <summary>
	/// Gets the incoming header for the MessengerConsoleMessages (Unity) message.
	/// </summary>
	public Header MessengerConsoleMessages => Get("MessengerConsoleMessages");
		
	/// <summary>
	/// Gets the incoming header for the BuddyRequests (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FriendRequests" />.
	/// </summary>
	public Header BuddyRequests => Get("BuddyRequests");
		
	/// <summary>
	/// Gets the incoming header for the PendingCallsForHelp (Unity) message.
	/// </summary>
	public Header PendingCallsForHelp => Get("PendingCallsForHelp");
		
	/// <summary>
	/// Gets the incoming header for the PendingCallsForHelpDeleted (Unity) message.
	/// </summary>
	public Header PendingCallsForHelpDeleted => Get("PendingCallsForHelpDeleted");
		
	/// <summary>
	/// Gets the incoming header for the SongList (Unity) message.
	/// </summary>
	public Header SongList => Get("SongList");
		
	/// <summary>
	/// Gets the incoming header for the SoundMachinePlayList (Unity) message.
	/// </summary>
	public Header SoundMachinePlayList => Get("SoundMachinePlayList");
		
	/// <summary>
	/// Gets the incoming header for the SongMissingPackages (Unity) message.
	/// </summary>
	public Header SongMissingPackages => Get("SongMissingPackages");
		
	/// <summary>
	/// Gets the incoming header for the PlayListInvalid (Unity) message.
	/// </summary>
	public Header PlayListInvalid => Get("PlayListInvalid");
		
	/// <summary>
	/// Gets the incoming header for the SongListFull (Unity) message.
	/// </summary>
	public Header SongListFull => Get("SongListFull");
		
	/// <summary>
	/// Gets the incoming header for the CallForHelpDisabled (Unity) message.
	/// </summary>
	public Header CallForHelpDisabled => Get("CallForHelpDisabled");
		
	/// <summary>
	/// Gets the incoming header for the SongAdded (Unity) message.
	/// </summary>
	public Header SongAdded => Get("SongAdded");
		
	/// <summary>
	/// Gets the incoming header for the InvalidSongName (Unity) message.
	/// </summary>
	public Header InvalidSongName => Get("InvalidSongName");
		
	/// <summary>
	/// Gets the incoming header for the UserSongDiscs (Unity) message.
	/// </summary>
	public Header UserSongDiscs => Get("UserSongDiscs");
		
	/// <summary>
	/// Gets the incoming header for the JukeboxDiscs (Unity) message.
	/// </summary>
	public Header JukeboxDiscs => Get("JukeboxDiscs");
		
	/// <summary>
	/// Gets the incoming header for the SongLocked (Unity) message.
	/// </summary>
	public Header SongLocked => Get("SongLocked");
		
	/// <summary>
	/// Gets the incoming header for the InvalidSongData (Unity) message.
	/// </summary>
	public Header InvalidSongData => Get("InvalidSongData");
		
	/// <summary>
	/// Gets the incoming header for the SongSaved (Unity) message.
	/// </summary>
	public Header SongSaved => Get("SongSaved");
		
	/// <summary>
	/// Gets the incoming header for the MuteAllInRoomResponse (Unity) message.
	/// </summary>
	public Header MuteAllInRoomResponse => Get("MuteAllInRoomResponse");
		
	/// <summary>
	/// Gets the incoming header for the UpdateFigure (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.FigureUpdate" />.
	/// </summary>
	public Header UpdateFigure => Get("UpdateFigure");
		
	/// <summary>
	/// Gets the incoming header for the ClientLatencyPingResponse (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.LatencyPingResponse" />.
	/// </summary>
	public Header ClientLatencyPingResponse => Get("ClientLatencyPingResponse");
		
	/// <summary>
	/// Gets the incoming header for the FriendFurniStartConfirmLock (Unity) message.
	/// </summary>
	public Header FriendFurniStartConfirmLock => Get("FriendFurniStartConfirmLock");
		
	/// <summary>
	/// Gets the incoming header for the FriendFurniLockConfirmedOther (Unity) message.
	/// </summary>
	public Header FriendFurniLockConfirmedOther => Get("FriendFurniLockConfirmedOther");
		
	/// <summary>
	/// Gets the incoming header for the UserTypingStatusChange (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UserTyping" />.
	/// </summary>
	public Header UserTypingStatusChange => Get("UserTypingStatusChange");
		
	/// <summary>
	/// Gets the incoming header for the HighlightUser (Unity) message.
	/// </summary>
	public Header HighlightUser => Get("HighlightUser");
		
	/// <summary>
	/// Gets the incoming header for the MiniMailArrived (Unity) message.
	/// </summary>
	public Header MiniMailArrived => Get("MiniMailArrived");
		
	/// <summary>
	/// Gets the incoming header for the RoomEventCanCreateEvent (Unity) message.
	/// </summary>
	public Header RoomEventCanCreateEvent => Get("RoomEventCanCreateEvent");
		
	/// <summary>
	/// Gets the incoming header for the RoomEventEventTypes (Unity) message.
	/// </summary>
	public Header RoomEventEventTypes => Get("RoomEventEventTypes");
		
	/// <summary>
	/// Gets the incoming header for the RoomEventEventsByEventType (Unity) message.
	/// </summary>
	public Header RoomEventEventsByEventType => Get("RoomEventEventsByEventType");
		
	/// <summary>
	/// Gets the incoming header for the RoomEventEventInfo (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.RoomEvent" />.
	/// </summary>
	public Header RoomEventEventInfo => Get("RoomEventEventInfo");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdAllowedRooms (Unity) message.
	/// </summary>
	public Header RoomAdAllowedRooms => Get("RoomAdAllowedRooms");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdListAds (Unity) message.
	/// </summary>
	public Header RoomAdListAds => Get("RoomAdListAds");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdUpdated (Unity) message.
	/// </summary>
	public Header RoomAdUpdated => Get("RoomAdUpdated");
		
	/// <summary>
	/// Gets the incoming header for the RoomAdCancelled (Unity) message.
	/// </summary>
	public Header RoomAdCancelled => Get("RoomAdCancelled");
		
	/// <summary>
	/// Gets the incoming header for the IgnoreUserResult (Unity) message.
	/// </summary>
	public Header IgnoreUserResult => Get("IgnoreUserResult");
		
	/// <summary>
	/// Gets the incoming header for the RequestIgnoreList (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.IgnoredUsers" />.
	/// </summary>
	public Header RequestIgnoreList => Get("RequestIgnoreList");
		
	/// <summary>
	/// Gets the incoming header for the PossibleUserAchievements (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Achievements" />.
	/// </summary>
	public Header PossibleUserAchievements => Get("PossibleUserAchievements");
		
	/// <summary>
	/// Gets the incoming header for the AchievementNotification (Unity) message.
	/// </summary>
	public Header AchievementNotification => Get("AchievementNotification");
		
	/// <summary>
	/// Gets the incoming header for the ActivityPointNotification (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.HabboActivityPointNotification" />.
	/// </summary>
	public Header ActivityPointNotification => Get("ActivityPointNotification");
		
	/// <summary>
	/// Gets the incoming header for the CatalogExpired (Unity) message.
	/// </summary>
	public Header CatalogExpired => Get("CatalogExpired");
		
	/// <summary>
	/// Gets the incoming header for the AchievementScore (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.AchievementsScore" />.
	/// </summary>
	public Header AchievementScore => Get("AchievementScore");
		
	/// <summary>
	/// Gets the incoming header for the ResolutionCompleted (Unity) message.
	/// </summary>
	public Header ResolutionCompleted => Get("ResolutionCompleted");
		
	/// <summary>
	/// Gets the incoming header for the RoomInfoUpdatedNotification (Unity) message.
	/// </summary>
	public Header RoomInfoUpdatedNotification => Get("RoomInfoUpdatedNotification");
		
	/// <summary>
	/// Gets the incoming header for the RoomThumbnailUpdateResult (Unity) message.
	/// </summary>
	public Header RoomThumbnailUpdateResult => Get("RoomThumbnailUpdateResult");
		
	/// <summary>
	/// Gets the incoming header for the MultipleStuffDataUpdate (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ObjectsDataUpdate" />.
	/// </summary>
	public Header MultipleStuffDataUpdate => Get("MultipleStuffDataUpdate");
		
	/// <summary>
	/// Gets the incoming header for the EnforceRoomCategorySelect (Unity) message.
	/// </summary>
	public Header EnforceRoomCategorySelect => Get("EnforceRoomCategorySelect");
		
	/// <summary>
	/// Gets the incoming header for the RoomTypes (Unity) message.
	/// </summary>
	public Header RoomTypes => Get("RoomTypes");
		
	/// <summary>
	/// Gets the incoming header for the RoomDance (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Dance" />.
	/// </summary>
	public Header RoomDance => Get("RoomDance");
		
	/// <summary>
	/// Gets the incoming header for the RoomExpression (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Expression" />.
	/// </summary>
	public Header RoomExpression => Get("RoomExpression");
		
	/// <summary>
	/// Gets the incoming header for the RoomCarryObject (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.CarryObject" />.
	/// </summary>
	public Header RoomCarryObject => Get("RoomCarryObject");
		
	/// <summary>
	/// Gets the incoming header for the RoomJoiningGame (Unity) message.
	/// </summary>
	public Header RoomJoiningGame => Get("RoomJoiningGame");
		
	/// <summary>
	/// Gets the incoming header for the RoomNotJoiningGame (Unity) message.
	/// </summary>
	public Header RoomNotJoiningGame => Get("RoomNotJoiningGame");
		
	/// <summary>
	/// Gets the incoming header for the RoomAvatarEffect (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.AvatarEffect" />.
	/// </summary>
	public Header RoomAvatarEffect => Get("RoomAvatarEffect");
		
	/// <summary>
	/// Gets the incoming header for the RoomAvatarSleeping (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Sleep" />.
	/// </summary>
	public Header RoomAvatarSleeping => Get("RoomAvatarSleeping");
		
	/// <summary>
	/// Gets the incoming header for the RoomUseObject (Unity) message.
	/// </summary>
	public Header RoomUseObject => Get("RoomUseObject");
		
	/// <summary>
	/// Gets the incoming header for the JudgeGuiStatus (Unity) message.
	/// </summary>
	public Header JudgeGuiStatus => Get("JudgeGuiStatus");
		
	/// <summary>
	/// Gets the incoming header for the StageOpenPerformerGui (Unity) message.
	/// </summary>
	public Header StageOpenPerformerGui => Get("StageOpenPerformerGui");
		
	/// <summary>
	/// Gets the incoming header for the StageClosePerformerGui (Unity) message.
	/// </summary>
	public Header StageClosePerformerGui => Get("StageClosePerformerGui");
		
	/// <summary>
	/// Gets the incoming header for the StageStartPlayingSong (Unity) message.
	/// </summary>
	public Header StageStartPlayingSong => Get("StageStartPlayingSong");
		
	/// <summary>
	/// Gets the incoming header for the StageStopPlayingSong (Unity) message.
	/// </summary>
	public Header StageStopPlayingSong => Get("StageStopPlayingSong");
		
	/// <summary>
	/// Gets the incoming header for the FlatCategoriesWithVisitorData (Unity) message.
	/// </summary>
	public Header FlatCategoriesWithVisitorData => Get("FlatCategoriesWithVisitorData");
		
	/// <summary>
	/// Gets the incoming header for the TradingAllowed (Unity) message.
	/// </summary>
	public Header TradingAllowed => Get("TradingAllowed");
		
	/// <summary>
	/// Gets the incoming header for the StripItemNotTradeable (Unity) message.
	/// </summary>
	public Header StripItemNotTradeable => Get("StripItemNotTradeable");
		
	/// <summary>
	/// Gets the incoming header for the ClientInfoFeedEnabled (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.InfoFeedEnable" />.
	/// </summary>
	public Header ClientInfoFeedEnabled => Get("ClientInfoFeedEnabled");
		
	/// <summary>
	/// Gets the incoming header for the FaqClientFaqs (Unity) message.
	/// </summary>
	public Header FaqClientFaqs => Get("FaqClientFaqs");
		
	/// <summary>
	/// Gets the incoming header for the FaqCategories (Unity) message.
	/// </summary>
	public Header FaqCategories => Get("FaqCategories");
		
	/// <summary>
	/// Gets the incoming header for the FaqText (Unity) message.
	/// </summary>
	public Header FaqText => Get("FaqText");
		
	/// <summary>
	/// Gets the incoming header for the FaqSearchResults (Unity) message.
	/// </summary>
	public Header FaqSearchResults => Get("FaqSearchResults");
		
	/// <summary>
	/// Gets the incoming header for the FaqCategory (Unity) message.
	/// </summary>
	public Header FaqCategory => Get("FaqCategory");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionAttached (Unity) message.
	/// </summary>
	public Header HelpRequestSessionAttached => Get("HelpRequestSessionAttached");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionStarted (Unity) message.
	/// </summary>
	public Header HelpRequestSessionStarted => Get("HelpRequestSessionStarted");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionEnded (Unity) message.
	/// </summary>
	public Header HelpRequestSessionEnded => Get("HelpRequestSessionEnded");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionDetached (Unity) message.
	/// </summary>
	public Header HelpRequestSessionDetached => Get("HelpRequestSessionDetached");
		
	/// <summary>
	/// Gets the incoming header for the GuideTicketSessionError (Unity) message.
	/// </summary>
	public Header GuideTicketSessionError => Get("GuideTicketSessionError");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionMessage (Unity) message.
	/// </summary>
	public Header HelpRequestSessionMessage => Get("HelpRequestSessionMessage");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionRequesterRoom (Unity) message.
	/// </summary>
	public Header HelpRequestSessionRequesterRoom => Get("HelpRequestSessionRequesterRoom");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionRequesterInvitedToGuideRoom (Unity) message.
	/// </summary>
	public Header HelpRequestSessionRequesterInvitedToGuideRoom => Get("HelpRequestSessionRequesterInvitedToGuideRoom");
		
	/// <summary>
	/// Gets the incoming header for the HelpRequestSessionTyping (Unity) message.
	/// </summary>
	public Header HelpRequestSessionTyping => Get("HelpRequestSessionTyping");
		
	/// <summary>
	/// Gets the incoming header for the CfhNextSanction (Unity) message.
	/// </summary>
	public Header CfhNextSanction => Get("CfhNextSanction");
		
	/// <summary>
	/// Gets the incoming header for the CfhSanctionStatus (Unity) message.
	/// </summary>
	public Header CfhSanctionStatus => Get("CfhSanctionStatus");
		
	/// <summary>
	/// Gets the incoming header for the UpdateRoomFloorPropertiesResponse (Unity) message.
	/// </summary>
	public Header UpdateRoomFloorPropertiesResponse => Get("UpdateRoomFloorPropertiesResponse");
		
	/// <summary>
	/// Gets the incoming header for the QuizResult (Unity) message.
	/// </summary>
	public Header QuizResult => Get("QuizResult");
		
	/// <summary>
	/// Gets the incoming header for the TalentTrackLevelUp (Unity) message.
	/// </summary>
	public Header TalentTrackLevelUp => Get("TalentTrackLevelUp");
		
	/// <summary>
	/// Gets the incoming header for the HotelMergeNameChange (Unity) message.
	/// </summary>
	public Header HotelMergeNameChange => Get("HotelMergeNameChange");
		
	/// <summary>
	/// Gets the incoming header for the ChangePetNameResult (Unity) message.
	/// </summary>
	public Header ChangePetNameResult => Get("ChangePetNameResult");
		
	/// <summary>
	/// Gets the incoming header for the GetPetConfigurationsResult (Unity) message.
	/// </summary>
	public Header GetPetConfigurationsResult => Get("GetPetConfigurationsResult");
		
	/// <summary>
	/// Gets the incoming header for the PetReceivedNotification (Unity) message.
	/// </summary>
	public Header PetReceivedNotification => Get("PetReceivedNotification");
		
	/// <summary>
	/// Gets the incoming header for the PetPlacingFailed (Unity) message.
	/// </summary>
	public Header PetPlacingFailed => Get("PetPlacingFailed");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceOpenOfferList (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.MarketPlaceOffers" />.
	/// </summary>
	public Header MarketplaceOpenOfferList => Get("MarketplaceOpenOfferList");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceOwnOfferList (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.MarketPlaceOwnOffers" />.
	/// </summary>
	public Header MarketplaceOwnOfferList => Get("MarketplaceOwnOfferList");
		
	/// <summary>
	/// Gets the incoming header for the IsOfferGiftable (Unity) message.
	/// </summary>
	public Header IsOfferGiftable => Get("IsOfferGiftable");
		
	/// <summary>
	/// Gets the incoming header for the SelectableClubGiftInfo (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.ClubGiftInfo" />.
	/// </summary>
	public Header SelectableClubGiftInfo => Get("SelectableClubGiftInfo");
		
	/// <summary>
	/// Gets the incoming header for the LoginWithPasswordAccountsDEPRECATED (Unity) message.
	/// </summary>
	public Header LoginWithPasswordAccountsDEPRECATED => Get("LoginWithPasswordAccountsDEPRECATED");
		
	/// <summary>
	/// Gets the incoming header for the DirectClubBuyAllowed (Unity) message.
	/// </summary>
	public Header DirectClubBuyAllowed => Get("DirectClubBuyAllowed");
		
	/// <summary>
	/// Gets the incoming header for the XmasCalendarDailyOffer (Unity) message.
	/// </summary>
	public Header XmasCalendarDailyOffer => Get("XmasCalendarDailyOffer");
		
	/// <summary>
	/// Gets the incoming header for the HabboSnowWarGameTokensOffer (Unity) message.
	/// </summary>
	public Header HabboSnowWarGameTokensOffer => Get("HabboSnowWarGameTokensOffer");
		
	/// <summary>
	/// Gets the incoming header for the LimitedFurniTimingInfo (Unity) message.
	/// </summary>
	public Header LimitedFurniTimingInfo => Get("LimitedFurniTimingInfo");
		
	/// <summary>
	/// Gets the incoming header for the EarnedCommunityGoalPrizes (Unity) message.
	/// </summary>
	public Header EarnedCommunityGoalPrizes => Get("EarnedCommunityGoalPrizes");
		
	/// <summary>
	/// Gets the incoming header for the CatalogPageExpirationData (Unity) message.
	/// </summary>
	public Header CatalogPageExpirationData => Get("CatalogPageExpirationData");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsTrigger (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsTrigger => Get("UserDefinedRoomEventsTrigger");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsAction (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsAction => Get("UserDefinedRoomEventsAction");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsCondition (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsCondition => Get("UserDefinedRoomEventsCondition");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsOpen (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsOpen => Get("UserDefinedRoomEventsOpen");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsRewardResult (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.WiredRewardResult" />.
	/// </summary>
	public Header UserDefinedRoomEventsRewardResult => Get("UserDefinedRoomEventsRewardResult");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsValidationError (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsValidationError => Get("UserDefinedRoomEventsValidationError");
		
	/// <summary>
	/// Gets the incoming header for the UserDefinedRoomEventsSaveSuccess (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsSaveSuccess => Get("UserDefinedRoomEventsSaveSuccess");
		
	/// <summary>
	/// Gets the incoming header for the CommunityGoalVoteAcknowledged (Unity) message.
	/// </summary>
	public Header CommunityGoalVoteAcknowledged => Get("CommunityGoalVoteAcknowledged");
		
	/// <summary>
	/// Gets the incoming header for the Notification (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NotificationDialog" />.
	/// </summary>
	public Header Notification => Get("Notification");
		
	/// <summary>
	/// Gets the incoming header for the GameYouArePlayer (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.YouArePlayingGame" />.
	/// </summary>
	public Header GameYouArePlayer => Get("GameYouArePlayer");
		
	/// <summary>
	/// Gets the incoming header for the GameNumberValue (Unity) message.
	/// </summary>
	public Header GameNumberValue => Get("GameNumberValue");
		
	/// <summary>
	/// Gets the incoming header for the ChangePassword (Unity) message.
	/// </summary>
	public Header ChangePassword => Get("ChangePassword");
		
	/// <summary>
	/// Gets the incoming header for the CitizenshipVipPromoEnable (Unity) message.
	/// </summary>
	public Header CitizenshipVipPromoEnable => Get("CitizenshipVipPromoEnable");
		
	/// <summary>
	/// Gets the incoming header for the CaptchaResponse (Unity) message.
	/// </summary>
	public Header CaptchaResponse => Get("CaptchaResponse");
		
	/// <summary>
	/// Gets the incoming header for the QuestsSeasonal (Unity) message.
	/// </summary>
	public Header QuestsSeasonal => Get("QuestsSeasonal");
		
	/// <summary>
	/// Gets the incoming header for the MotdResponse (Unity) message.
	/// </summary>
	public Header MotdResponse => Get("MotdResponse");
		
	/// <summary>
	/// Gets the incoming header for the MuteTimeRemaining (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.RemainingMutePeriod" />.
	/// </summary>
	public Header MuteTimeRemaining => Get("MuteTimeRemaining");
		
	/// <summary>
	/// Gets the incoming header for the GetDeviceTokensResponse (Unity) message.
	/// </summary>
	public Header GetDeviceTokensResponse => Get("GetDeviceTokensResponse");
		
	/// <summary>
	/// Gets the incoming header for the LoginTokensCleared (Unity) message.
	/// </summary>
	public Header LoginTokensCleared => Get("LoginTokensCleared");
		
	/// <summary>
	/// Gets the incoming header for the RestoreClientFromMinimizedState (Unity) message.
	/// </summary>
	public Header RestoreClientFromMinimizedState => Get("RestoreClientFromMinimizedState");
		
	/// <summary>
	/// Gets the incoming header for the FriendBarFindFriendsResult (Unity) message.
	/// </summary>
	public Header FriendBarFindFriendsResult => Get("FriendBarFindFriendsResult");
		
	/// <summary>
	/// Gets the incoming header for the UnseenElements (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.UnseenItems" />.
	/// </summary>
	public Header UnseenElements => Get("UnseenElements");
		
	/// <summary>
	/// Gets the incoming header for the FriendBarEventNotification (Unity) message.
	/// </summary>
	public Header FriendBarEventNotification => Get("FriendBarEventNotification");
		
	/// <summary>
	/// Gets the incoming header for the AccountProgressedInfo (Unity) message.
	/// </summary>
	public Header AccountProgressedInfo => Get("AccountProgressedInfo");
		
	/// <summary>
	/// Gets the incoming header for the AccountProgressionInfo (Unity) message.
	/// </summary>
	public Header AccountProgressionInfo => Get("AccountProgressionInfo");
		
	/// <summary>
	/// Gets the incoming header for the AvatarList (Unity) message.
	/// </summary>
	public Header AvatarList => Get("AvatarList");
		
	/// <summary>
	/// Gets the incoming header for the NewAvatarInfo (Unity) message.
	/// </summary>
	public Header NewAvatarInfo => Get("NewAvatarInfo");
		
	/// <summary>
	/// Gets the incoming header for the DeactivateAvatarInfo (Unity) message.
	/// </summary>
	public Header DeactivateAvatarInfo => Get("DeactivateAvatarInfo");
		
	/// <summary>
	/// Gets the incoming header for the InitialRooms (Unity) message.
	/// </summary>
	public Header InitialRooms => Get("InitialRooms");
		
	/// <summary>
	/// Gets the incoming header for the InitialRoomSelected (Unity) message.
	/// </summary>
	public Header InitialRoomSelected => Get("InitialRoomSelected");
		
	/// <summary>
	/// Gets the incoming header for the RequestSpamWallPostItMessage (Unity) message.
	/// </summary>
	public Header RequestSpamWallPostItMessage => Get("RequestSpamWallPostItMessage");
		
	/// <summary>
	/// Gets the incoming header for the PossibleAchievement (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.Achievement" />.
	/// </summary>
	public Header PossibleAchievement => Get("PossibleAchievement");
		
	/// <summary>
	/// Gets the incoming header for the UsersBannedFromRoom (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.BannedUsersFromRoom" />.
	/// </summary>
	public Header UsersBannedFromRoom => Get("UsersBannedFromRoom");
		
	/// <summary>
	/// Gets the incoming header for the UpdatePetFigure (Unity) message.
	/// </summary>
	public Header UpdatePetFigure => Get("UpdatePetFigure");
		
	/// <summary>
	/// Gets the incoming header for the AvailableResolutionAchievements (Unity) message.
	/// </summary>
	public Header AvailableResolutionAchievements => Get("AvailableResolutionAchievements");
		
	/// <summary>
	/// Gets the incoming header for the ResolutionProgress (Unity) message.
	/// </summary>
	public Header ResolutionProgress => Get("ResolutionProgress");
		
	/// <summary>
	/// Gets the incoming header for the BuildersClubMembershipStatus (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.BuildersClubSubscriptionStatus" />.
	/// </summary>
	public Header BuildersClubMembershipStatus => Get("BuildersClubMembershipStatus");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2MetaData (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NavigatorMetaData" />.
	/// </summary>
	public Header Navigator2MetaData => Get("Navigator2MetaData");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2SearchResultBlocks (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NavigatorSearchResultBlocks" />.
	/// </summary>
	public Header Navigator2SearchResultBlocks => Get("Navigator2SearchResultBlocks");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2LiftArea (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NavigatorLiftedRooms" />.
	/// </summary>
	public Header Navigator2LiftArea => Get("Navigator2LiftArea");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2UserPreferences (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NewNavigatorPreferences" />.
	/// </summary>
	public Header Navigator2UserPreferences => Get("Navigator2UserPreferences");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2UserSavedSearches (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NavigatorSavedSearches" />.
	/// </summary>
	public Header Navigator2UserSavedSearches => Get("Navigator2UserSavedSearches");
		
	/// <summary>
	/// Gets the incoming header for the Navigator2UserCollapsedCategories (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.NavigatorCollapsedCategories" />.
	/// </summary>
	public Header Navigator2UserCollapsedCategories => Get("Navigator2UserCollapsedCategories");
		
	/// <summary>
	/// Gets the incoming header for the ProductOffers (Unity) message.
	/// </summary>
	public Header ProductOffers => Get("ProductOffers");
		
	/// <summary>
	/// Gets the incoming header for the PickUpAllFurniAndResetHeightmap (Unity) message.
	/// </summary>
	public Header PickUpAllFurniAndResetHeightmap => Get("PickUpAllFurniAndResetHeightmap");
		
	/// <summary>
	/// Gets the incoming header for the StackingHelperHeightUpdate (Unity) message.
	/// </summary>
	public Header StackingHelperHeightUpdate => Get("StackingHelperHeightUpdate");
		
	/// <summary>
	/// Gets the incoming header for the YoutubePlayLists (Unity) message.
	/// </summary>
	public Header YoutubePlayLists => Get("YoutubePlayLists");
		
	/// <summary>
	/// Gets the incoming header for the RentableSpaceInfo (Unity) message.
	/// </summary>
	public Header RentableSpaceInfo => Get("RentableSpaceInfo");
		
	/// <summary>
	/// Gets the incoming header for the VerifyCodeResult (Unity) message.
	/// </summary>
	public Header VerifyCodeResult => Get("VerifyCodeResult");
		
	/// <summary>
	/// Gets the incoming header for the VerificationState (Unity) message.
	/// </summary>
	public Header VerificationState => Get("VerificationState");
		
	/// <summary>
	/// Gets the incoming header for the NuxNotComplete (Unity) message.
	/// </summary>
	public Header NuxNotComplete => Get("NuxNotComplete");
		
	/// <summary>
	/// Gets the incoming header for the NuxGiftOffer (Unity) message.
	/// </summary>
	public Header NuxGiftOffer => Get("NuxGiftOffer");
		
	/// <summary>
	/// Gets the incoming header for the TargetedOfferList (Unity) message.
	/// </summary>
	public Header TargetedOfferList => Get("TargetedOfferList");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewTicketCreationResult (Unity) message.
	/// </summary>
	public Header ChatReviewTicketCreationResult => Get("ChatReviewTicketCreationResult");
		
	/// <summary>
	/// Gets the incoming header for the ChatReviewTicketResolution (Unity) message.
	/// </summary>
	public Header ChatReviewTicketResolution => Get("ChatReviewTicketResolution");
		
	/// <summary>
	/// Gets the incoming header for the AccountSafetyLock (Unity) message.
	/// </summary>
	public Header AccountSafetyLock => Get("AccountSafetyLock");
		
	/// <summary>
	/// Gets the incoming header for the PointElement (Unity) message.
	/// </summary>
	public Header PointElement => Get("PointElement");
		
	/// <summary>
	/// Gets the incoming header for the SubmitGdprRequestResult (Unity) message.
	/// </summary>
	public Header SubmitGdprRequestResult => Get("SubmitGdprRequestResult");
		
	/// <summary>
	/// Gets the incoming header for the CancelGdprRequestResult (Unity) message.
	/// </summary>
	public Header CancelGdprRequestResult => Get("CancelGdprRequestResult");
		
	/// <summary>
	/// Gets the incoming header for the GetGdprRequestResult (Unity) message.
	/// </summary>
	public Header GetGdprRequestResult => Get("GetGdprRequestResult");
		
	/// <summary>
	/// Gets the incoming header for the ForumStats (Unity) message.
	/// </summary>
	public Header ForumStats => Get("ForumStats");
		
	/// <summary>
	/// Gets the incoming header for the ForumThreadMessages (Unity) message.
	/// </summary>
	public Header ForumThreadMessages => Get("ForumThreadMessages");
		
	/// <summary>
	/// Gets the incoming header for the ForumThread (Unity) message.
	/// </summary>
	public Header ForumThread => Get("ForumThread");
		
	/// <summary>
	/// Gets the incoming header for the ForumMessage (Unity) message.
	/// </summary>
	public Header ForumMessage => Get("ForumMessage");
		
	/// <summary>
	/// Gets the incoming header for the PostForumThreadOk (Unity) message.
	/// </summary>
	public Header PostForumThreadOk => Get("PostForumThreadOk");
		
	/// <summary>
	/// Gets the incoming header for the PostForumMessageOk (Unity) message.
	/// </summary>
	public Header PostForumMessageOk => Get("PostForumMessageOk");
		
	/// <summary>
	/// Gets the incoming header for the UserClassifications (Unity) message.
	/// </summary>
	public Header UserClassifications => Get("UserClassifications");
		
	/// <summary>
	/// Gets the incoming header for the CameraToken (Unity) message.
	/// </summary>
	public Header CameraToken => Get("CameraToken");
		
	/// <summary>
	/// Gets the incoming header for the PhotoPurchaseSuccess (Unity) message.
	/// </summary>
	public Header PhotoPurchaseSuccess => Get("PhotoPurchaseSuccess");
		
	/// <summary>
	/// Gets the incoming header for the CameraPictureUrl (Unity) message.
	/// </summary>
	public Header CameraPictureUrl => Get("CameraPictureUrl");
		
	/// <summary>
	/// Gets the incoming header for the CameraPhotoPrice (Unity) message.
	/// </summary>
	public Header CameraPhotoPrice => Get("CameraPhotoPrice");
		
	/// <summary>
	/// Gets the incoming header for the PhotoPublishStatus (Unity) message.
	/// </summary>
	public Header PhotoPublishStatus => Get("PhotoPublishStatus");
		
	/// <summary>
	/// Gets the incoming header for the PhotoCompetitionEntryStatus (Unity) message.
	/// </summary>
	public Header PhotoCompetitionEntryStatus => Get("PhotoCompetitionEntryStatus");
		
	/// <summary>
	/// Gets the incoming header for the RoomThumbnailStatus (Unity) message.
	/// </summary>
	public Header RoomThumbnailStatus => Get("RoomThumbnailStatus");
		
	/// <summary>
	/// Gets the incoming header for the Reputation (Unity) message.
	/// </summary>
	public Header Reputation => Get("Reputation");
		
	/// <summary>
	/// Gets the incoming header for the InventoryFurniByRoomResult (Unity) message.
	/// </summary>
	public Header InventoryFurniByRoomResult => Get("InventoryFurniByRoomResult");
		
	/// <summary>
	/// Gets the incoming header for the BotReceived (Unity) message.
	/// </summary>
	public Header BotReceived => Get("BotReceived");
		
	/// <summary>
	/// Gets the incoming header for the BotCommandConfigurationData (Unity) message.
	/// </summary>
	public Header BotCommandConfigurationData => Get("BotCommandConfigurationData");
		
	/// <summary>
	/// Gets the incoming header for the BotUpdateSkillList (Unity) message.
	/// </summary>
	public Header BotUpdateSkillList => Get("BotUpdateSkillList");
		
	/// <summary>
	/// Gets the incoming header for the MarketplaceCancelAllOffersResult (Unity) message.
	/// </summary>
	public Header MarketplaceCancelAllOffersResult => Get("MarketplaceCancelAllOffersResult");
		
	/// <summary>
	/// Gets the incoming header for the SendPetToHolidayResult (Unity) message.
	/// </summary>
	public Header SendPetToHolidayResult => Get("SendPetToHolidayResult");
		
	/// <summary>
	/// Gets the incoming header for the AgreementTypesResult (Unity) message.
	/// </summary>
	public Header AgreementTypesResult => Get("AgreementTypesResult");
		
	/// <summary>
	/// Gets the incoming header for the IdentityAgreements (Unity) message.
	/// </summary>
	public Header IdentityAgreements => Get("IdentityAgreements");
		
	/// <summary>
	/// Gets the incoming header for the GuildMembershipRequests (Unity) message.
	/// </summary>
	public Header GuildMembershipRequests => Get("GuildMembershipRequests");
		
	/// <summary>
	/// Gets the incoming header for the FlatFavouriteCount (Unity) message.
	/// </summary>
	public Header FlatFavouriteCount => Get("FlatFavouriteCount");
		
	/// <summary>
	/// Gets the incoming header for the StartCreateGuildResponse (Unity) message.
	/// </summary>
	public Header StartCreateGuildResponse => Get("StartCreateGuildResponse");
		
	/// <summary>
	/// Gets the incoming header for the CommitCreateGuildResponse (Unity) message.
	/// </summary>
	public Header CommitCreateGuildResponse => Get("CommitCreateGuildResponse");
		
	/// <summary>
	/// Gets the incoming header for the MeltdownWatchVerify (Unity) message.
	/// </summary>
	public Header MeltdownWatchVerify => Get("MeltdownWatchVerify");
		
	/// <summary>
	/// Gets the incoming header for the EarningStatus (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.IncomeRewardStatus" />.
	/// </summary>
	public Header EarningStatus => Get("EarningStatus");
		
	/// <summary>
	/// Gets the incoming header for the ClaimEarningResult (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.IncomeRewardClaimResponse" />.
	/// </summary>
	public Header ClaimEarningResult => Get("ClaimEarningResult");
		
	/// <summary>
	/// Gets the incoming header for the VaultStatus (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.CreditVaultStatus" />.
	/// </summary>
	public Header VaultStatus => Get("VaultStatus");
		
	/// <summary>
	/// Gets the incoming header for the UpdateAccountPreferencesResult (Unity) message.
	/// </summary>
	public Header UpdateAccountPreferencesResult => Get("UpdateAccountPreferencesResult");
		
	/// <summary>
	/// Gets the incoming header for the LinkIdentificationMethodResult (Unity) message.
	/// </summary>
	public Header LinkIdentificationMethodResult => Get("LinkIdentificationMethodResult");
		
	/// <summary>
	/// Gets the incoming header for the BanInfo (Unity) message.
	/// </summary>
	public Header BanInfo => Get("BanInfo");
		
	/// <summary>
	/// Gets the incoming header for the AntiSpamTriggered (Unity) message.
	/// </summary>
	public Header AntiSpamTriggered => Get("AntiSpamTriggered");
		
	/// <summary>
	/// Gets the incoming header for the ServerDebugPong (Unity) message.
	/// </summary>
	public Header ServerDebugPong => Get("ServerDebugPong");
		
	/// <summary>
	/// Gets the incoming header for the DisconnectionReason (Unity) message.
	/// The Flash equivalent for this message is <see cref="Incoming.DisconnectReason" />.
	/// </summary>
	public Header DisconnectionReason => Get("DisconnectionReason");
		
	/// <summary>
	/// Gets the incoming header for the ProxyId (Unity) message.
	/// </summary>
	public Header ProxyId => Get("ProxyId");
}
