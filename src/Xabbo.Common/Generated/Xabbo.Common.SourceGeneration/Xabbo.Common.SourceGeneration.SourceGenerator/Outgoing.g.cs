namespace Xabbo.Messages;

#nullable disable

public sealed partial class Outgoing : Headers
{
		
	/// <summary>
	/// Gets the outgoing header for the AcceptTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeAccept" />.
	/// </summary>
	public Header AcceptTrading => Get("AcceptTrading");
		
	/// <summary>
	/// Gets the outgoing header for the AddItemsToTrade (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeAddItems" />.
	/// </summary>
	public Header AddItemsToTrade => Get("AddItemsToTrade");
		
	/// <summary>
	/// Gets the outgoing header for the AddItemToTrade (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeAddItem" />.
	/// </summary>
	public Header AddItemToTrade => Get("AddItemToTrade");
		
	/// <summary>
	/// Gets the outgoing header for the CloseTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeClose" />.
	/// </summary>
	public Header CloseTrading => Get("CloseTrading");
		
	/// <summary>
	/// Gets the outgoing header for the ConfirmAcceptTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeConfirmAccept" />.
	/// </summary>
	public Header ConfirmAcceptTrading => Get("ConfirmAcceptTrading");
		
	/// <summary>
	/// Gets the outgoing header for the ConfirmDeclineTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeConfirmDecline" />.
	/// </summary>
	public Header ConfirmDeclineTrading => Get("ConfirmDeclineTrading");
		
	/// <summary>
	/// Gets the outgoing header for the OpenTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeOpen" />.
	/// </summary>
	public Header OpenTrading => Get("OpenTrading");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveItemFromTrade (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeRemoveItem" />.
	/// </summary>
	public Header RemoveItemFromTrade => Get("RemoveItemFromTrade");
		
	/// <summary>
	/// Gets the outgoing header for the UnacceptTrading (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TradeUnaccept" />.
	/// </summary>
	public Header UnacceptTrading => Get("UnacceptTrading");
		
	/// <summary>
	/// Gets the outgoing header for the CloseIssueDefaultAction (Unity/Flash) message.
	/// </summary>
	public Header CloseIssueDefaultAction => Get("CloseIssueDefaultAction");
		
	/// <summary>
	/// Gets the outgoing header for the CloseIssues (Unity/Flash) message.
	/// </summary>
	public Header CloseIssues => Get("CloseIssues");
		
	/// <summary>
	/// Gets the outgoing header for the DefaultSanction (Unity/Flash) message.
	/// </summary>
	public Header DefaultSanction => Get("DefaultSanction");
		
	/// <summary>
	/// Gets the outgoing header for the GetCfhChatLog (Unity/Flash) message.
	/// </summary>
	public Header GetCfhChatLog => Get("GetCfhChatLog");
		
	/// <summary>
	/// Gets the outgoing header for the GetModeratorRoomInfo (Unity/Flash) message.
	/// </summary>
	public Header GetModeratorRoomInfo => Get("GetModeratorRoomInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetModeratorUserInfo (Unity/Flash) message.
	/// </summary>
	public Header GetModeratorUserInfo => Get("GetModeratorUserInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomChatLog (Unity/Flash) message.
	/// </summary>
	public Header GetRoomChatLog => Get("GetRoomChatLog");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomVisits (Unity/Flash) message.
	/// </summary>
	public Header GetRoomVisits => Get("GetRoomVisits");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserChatLog (Unity/Flash) message.
	/// </summary>
	public Header GetUserChatLog => Get("GetUserChatLog");
		
	/// <summary>
	/// Gets the outgoing header for the ModAlert (Unity/Flash) message.
	/// </summary>
	public Header ModAlert => Get("ModAlert");
		
	/// <summary>
	/// Gets the outgoing header for the ModBan (Unity/Flash) message.
	/// </summary>
	public Header ModBan => Get("ModBan");
		
	/// <summary>
	/// Gets the outgoing header for the ModerateRoom (Unity/Flash) message.
	/// </summary>
	public Header ModerateRoom => Get("ModerateRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ModeratorAction (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ModerationAction" />.
	/// </summary>
	public Header ModeratorAction => Get("ModeratorAction");
		
	/// <summary>
	/// Gets the outgoing header for the ModKick (Unity/Flash) message.
	/// </summary>
	public Header ModKick => Get("ModKick");
		
	/// <summary>
	/// Gets the outgoing header for the ModMessage (Unity/Flash) message.
	/// </summary>
	public Header ModMessage => Get("ModMessage");
		
	/// <summary>
	/// Gets the outgoing header for the ModMute (Unity/Flash) message.
	/// </summary>
	public Header ModMute => Get("ModMute");
		
	/// <summary>
	/// Gets the outgoing header for the ModToolPreferences (Unity/Flash) message.
	/// </summary>
	public Header ModToolPreferences => Get("ModToolPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the ModToolSanction (Flash) message.
	/// </summary>
	public Header ModToolSanction => Get("ModToolSanction");
		
	/// <summary>
	/// Gets the outgoing header for the ModTradingLock (Unity/Flash) message.
	/// </summary>
	public Header ModTradingLock => Get("ModTradingLock");
		
	/// <summary>
	/// Gets the outgoing header for the PickIssues (Unity/Flash) message.
	/// </summary>
	public Header PickIssues => Get("PickIssues");
		
	/// <summary>
	/// Gets the outgoing header for the ReleaseIssues (Unity/Flash) message.
	/// </summary>
	public Header ReleaseIssues => Get("ReleaseIssues");
		
	/// <summary>
	/// Gets the outgoing header for the ClickFurni (Flash) message.
	/// </summary>
	public Header ClickFurni => Get("ClickFurni");
		
	/// <summary>
	/// Gets the outgoing header for the CompostPlant (Unity/Flash) message.
	/// </summary>
	public Header CompostPlant => Get("CompostPlant");
		
	/// <summary>
	/// Gets the outgoing header for the GetFurnitureAliases (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetFurniAliases" />.
	/// </summary>
	public Header GetFurnitureAliases => Get("GetFurnitureAliases");
		
	/// <summary>
	/// Gets the outgoing header for the GetHeightMap (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetRoomEntryData" />.
	/// </summary>
	public Header GetHeightMap => Get("GetHeightMap");
		
	/// <summary>
	/// Gets the outgoing header for the GetItemData (Unity/Flash) message.
	/// </summary>
	public Header GetItemData => Get("GetItemData");
		
	/// <summary>
	/// Gets the outgoing header for the GetPetCommands (Flash) message.
	/// </summary>
	public Header GetPetCommands => Get("GetPetCommands");
		
	/// <summary>
	/// Gets the outgoing header for the GiveSupplementToPet (Unity/Flash) message.
	/// </summary>
	public Header GiveSupplementToPet => Get("GiveSupplementToPet");
		
	/// <summary>
	/// Gets the outgoing header for the HarvestPet (Unity/Flash) message.
	/// </summary>
	public Header HarvestPet => Get("HarvestPet");
		
	/// <summary>
	/// Gets the outgoing header for the MountPet (Unity/Flash) message.
	/// </summary>
	public Header MountPet => Get("MountPet");
		
	/// <summary>
	/// Gets the outgoing header for the MoveAvatar (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Move" />.
	/// </summary>
	public Header MoveAvatar => Get("MoveAvatar");
		
	/// <summary>
	/// Gets the outgoing header for the MoveObject (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MoveRoomItem" />.
	/// </summary>
	public Header MoveObject => Get("MoveObject");
		
	/// <summary>
	/// Gets the outgoing header for the MovePet (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MovePetInFlat" />.
	/// </summary>
	public Header MovePet => Get("MovePet");
		
	/// <summary>
	/// Gets the outgoing header for the MoveWallItem (Unity/Flash) message.
	/// </summary>
	public Header MoveWallItem => Get("MoveWallItem");
		
	/// <summary>
	/// Gets the outgoing header for the PickupObject (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.PickItemUpFromRoom" />.
	/// </summary>
	public Header PickupObject => Get("PickupObject");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceBot (Flash) message.
	/// </summary>
	public Header PlaceBot => Get("PlaceBot");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceObject (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.PlaceRoomItem" />.
	/// </summary>
	public Header PlaceObject => Get("PlaceObject");
		
	/// <summary>
	/// Gets the outgoing header for the PlacePet (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.PlacePetToFlat" />.
	/// </summary>
	public Header PlacePet => Get("PlacePet");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveBotFromFlat (Unity/Flash) message.
	/// </summary>
	public Header RemoveBotFromFlat => Get("RemoveBotFromFlat");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveItem (Unity/Flash) message.
	/// </summary>
	public Header RemoveItem => Get("RemoveItem");
		
	/// <summary>
	/// Gets the outgoing header for the RemovePetFromFlat (Unity/Flash) message.
	/// </summary>
	public Header RemovePetFromFlat => Get("RemovePetFromFlat");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveSaddleFromPet (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RemoveSaddle" />.
	/// </summary>
	public Header RemoveSaddleFromPet => Get("RemoveSaddleFromPet");
		
	/// <summary>
	/// Gets the outgoing header for the SetClothingChangeData (Flash) message.
	/// </summary>
	public Header SetClothingChangeData => Get("SetClothingChangeData");
		
	/// <summary>
	/// Gets the outgoing header for the SetItemData (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SetStickyData" />.
	/// </summary>
	public Header SetItemData => Get("SetItemData");
		
	/// <summary>
	/// Gets the outgoing header for the SetObjectData (Flash) message.
	/// </summary>
	public Header SetObjectData => Get("SetObjectData");
		
	/// <summary>
	/// Gets the outgoing header for the TogglePetBreedingPermission (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TogglePetBreedingRights" />.
	/// </summary>
	public Header TogglePetBreedingPermission => Get("TogglePetBreedingPermission");
		
	/// <summary>
	/// Gets the outgoing header for the TogglePetRidingPermission (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.TogglePetRidingAccessRights" />.
	/// </summary>
	public Header TogglePetRidingPermission => Get("TogglePetRidingPermission");
		
	/// <summary>
	/// Gets the outgoing header for the UseFurniture (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UseStuff" />.
	/// </summary>
	public Header UseFurniture => Get("UseFurniture");
		
	/// <summary>
	/// Gets the outgoing header for the UseWallItem (Unity/Flash) message.
	/// </summary>
	public Header UseWallItem => Get("UseWallItem");
		
	/// <summary>
	/// Gets the outgoing header for the NavigatorAddCollapsedCategory (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2AddCollapsedCategory" />.
	/// </summary>
	public Header NavigatorAddCollapsedCategory => Get("NavigatorAddCollapsedCategory");
		
	/// <summary>
	/// Gets the outgoing header for the NavigatorAddSavedSearch (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2AddSavedSearch" />.
	/// </summary>
	public Header NavigatorAddSavedSearch => Get("NavigatorAddSavedSearch");
		
	/// <summary>
	/// Gets the outgoing header for the NavigatorDeleteSavedSearch (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2DeleteSavedSearch" />.
	/// </summary>
	public Header NavigatorDeleteSavedSearch => Get("NavigatorDeleteSavedSearch");
		
	/// <summary>
	/// Gets the outgoing header for the NavigatorRemoveCollapsedCategory (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2RemoveCollapsedCategory" />.
	/// </summary>
	public Header NavigatorRemoveCollapsedCategory => Get("NavigatorRemoveCollapsedCategory");
		
	/// <summary>
	/// Gets the outgoing header for the NavigatorSetSearchCodeViewMode (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2SetSearchCodeViewMode" />.
	/// </summary>
	public Header NavigatorSetSearchCodeViewMode => Get("NavigatorSetSearchCodeViewMode");
		
	/// <summary>
	/// Gets the outgoing header for the NewNavigatorInit (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2Init" />.
	/// </summary>
	public Header NewNavigatorInit => Get("NewNavigatorInit");
		
	/// <summary>
	/// Gets the outgoing header for the NewNavigatorSearch (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Navigator2Search" />.
	/// </summary>
	public Header NewNavigatorSearch => Get("NewNavigatorSearch");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelpFromForumMessage (Flash) message.
	/// </summary>
	public Header CallForHelpFromForumMessage => Get("CallForHelpFromForumMessage");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelpFromForumThread (Flash) message.
	/// </summary>
	public Header CallForHelpFromForumThread => Get("CallForHelpFromForumThread");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelpFromIM (Flash) message.
	/// </summary>
	public Header CallForHelpFromIM => Get("CallForHelpFromIM");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelpFromPhoto (Flash) message.
	/// </summary>
	public Header CallForHelpFromPhoto => Get("CallForHelpFromPhoto");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelpFromSelfie (Flash) message.
	/// </summary>
	public Header CallForHelpFromSelfie => Get("CallForHelpFromSelfie");
		
	/// <summary>
	/// Gets the outgoing header for the CallForHelp (Flash) message.
	/// </summary>
	public Header CallForHelp => Get("CallForHelp");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewGuideDecidesOnOffer (Flash) message.
	/// </summary>
	public Header ChatReviewGuideDecidesOnOffer => Get("ChatReviewGuideDecidesOnOffer");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewGuideDetached (Flash) message.
	/// </summary>
	public Header ChatReviewGuideDetached => Get("ChatReviewGuideDetached");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewGuideVote (Flash) message.
	/// </summary>
	public Header ChatReviewGuideVote => Get("ChatReviewGuideVote");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewSessionCreate (Unity/Flash) message.
	/// </summary>
	public Header ChatReviewSessionCreate => Get("ChatReviewSessionCreate");
		
	/// <summary>
	/// Gets the outgoing header for the DeletePendingCallsForHelp (Unity/Flash) message.
	/// </summary>
	public Header DeletePendingCallsForHelp => Get("DeletePendingCallsForHelp");
		
	/// <summary>
	/// Gets the outgoing header for the GetCfhStatus (Unity/Flash) message.
	/// </summary>
	public Header GetCfhStatus => Get("GetCfhStatus");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuideReportingStatus (Unity/Flash) message.
	/// </summary>
	public Header GetGuideReportingStatus => Get("GetGuideReportingStatus");
		
	/// <summary>
	/// Gets the outgoing header for the GetPendingCallsForHelp (Unity/Flash) message.
	/// </summary>
	public Header GetPendingCallsForHelp => Get("GetPendingCallsForHelp");
		
	/// <summary>
	/// Gets the outgoing header for the GetQuizQuestions (Flash) message.
	/// </summary>
	public Header GetQuizQuestions => Get("GetQuizQuestions");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionCreate (Flash) message.
	/// </summary>
	public Header GuideSessionCreate => Get("GuideSessionCreate");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionFeedback (Flash) message.
	/// </summary>
	public Header GuideSessionFeedback => Get("GuideSessionFeedback");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionGetRequesterRoom (Flash) message.
	/// </summary>
	public Header GuideSessionGetRequesterRoom => Get("GuideSessionGetRequesterRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionGuideDecides (Flash) message.
	/// </summary>
	public Header GuideSessionGuideDecides => Get("GuideSessionGuideDecides");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionInviteRequester (Flash) message.
	/// </summary>
	public Header GuideSessionInviteRequester => Get("GuideSessionInviteRequester");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionIsTyping (Flash) message.
	/// </summary>
	public Header GuideSessionIsTyping => Get("GuideSessionIsTyping");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionMessage (Flash) message.
	/// </summary>
	public Header GuideSessionMessage => Get("GuideSessionMessage");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionOnDutyUpdate (Flash) message.
	/// </summary>
	public Header GuideSessionOnDutyUpdate => Get("GuideSessionOnDutyUpdate");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionReport (Flash) message.
	/// </summary>
	public Header GuideSessionReport => Get("GuideSessionReport");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionRequesterCancels (Flash) message.
	/// </summary>
	public Header GuideSessionRequesterCancels => Get("GuideSessionRequesterCancels");
		
	/// <summary>
	/// Gets the outgoing header for the GuideSessionResolved (Flash) message.
	/// </summary>
	public Header GuideSessionResolved => Get("GuideSessionResolved");
		
	/// <summary>
	/// Gets the outgoing header for the PostQuizAnswers (Flash) message.
	/// </summary>
	public Header PostQuizAnswers => Get("PostQuizAnswers");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateFigureData (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UpdateAvatar" />.
	/// </summary>
	public Header UpdateFigureData => Get("UpdateFigureData");
		
	/// <summary>
	/// Gets the outgoing header for the ResetUnseenItemIds (Flash) message.
	/// </summary>
	public Header ResetUnseenItemIds => Get("ResetUnseenItemIds");
		
	/// <summary>
	/// Gets the outgoing header for the ResetUnseenItems (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ResetUnseenCounter" />.
	/// </summary>
	public Header ResetUnseenItems => Get("ResetUnseenItems");
		
	/// <summary>
	/// Gets the outgoing header for the Craft (Unity/Flash) message.
	/// </summary>
	public Header Craft => Get("Craft");
		
	/// <summary>
	/// Gets the outgoing header for the CraftSecret (Unity/Flash) message.
	/// </summary>
	public Header CraftSecret => Get("CraftSecret");
		
	/// <summary>
	/// Gets the outgoing header for the GetCraftableProducts (Unity/Flash) message.
	/// </summary>
	public Header GetCraftableProducts => Get("GetCraftableProducts");
		
	/// <summary>
	/// Gets the outgoing header for the GetCraftingRecipe (Unity/Flash) message.
	/// </summary>
	public Header GetCraftingRecipe => Get("GetCraftingRecipe");
		
	/// <summary>
	/// Gets the outgoing header for the GetCraftingRecipesAvailable (Unity/Flash) message.
	/// </summary>
	public Header GetCraftingRecipesAvailable => Get("GetCraftingRecipesAvailable");
		
	/// <summary>
	/// Gets the outgoing header for the ApplySnapshot (Flash) message.
	/// </summary>
	public Header ApplySnapshot => Get("ApplySnapshot");
		
	/// <summary>
	/// Gets the outgoing header for the Open (Flash) message.
	/// </summary>
	public Header Open => Get("Open");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateAction (Flash) message.
	/// </summary>
	public Header UpdateAction => Get("UpdateAction");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateAddon (Flash) message.
	/// </summary>
	public Header UpdateAddon => Get("UpdateAddon");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateCondition (Flash) message.
	/// </summary>
	public Header UpdateCondition => Get("UpdateCondition");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateSelector (Flash) message.
	/// </summary>
	public Header UpdateSelector => Get("UpdateSelector");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateTrigger (Flash) message.
	/// </summary>
	public Header UpdateTrigger => Get("UpdateTrigger");
		
	/// <summary>
	/// Gets the outgoing header for the PollAnswer (Unity/Flash) message.
	/// </summary>
	public Header PollAnswer => Get("PollAnswer");
		
	/// <summary>
	/// Gets the outgoing header for the PollReject (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RejectPoll" />.
	/// </summary>
	public Header PollReject => Get("PollReject");
		
	/// <summary>
	/// Gets the outgoing header for the PollStart (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.StartPoll" />.
	/// </summary>
	public Header PollStart => Get("PollStart");
		
	/// <summary>
	/// Gets the outgoing header for the AddAdminRightsToMember (Unity/Flash) message.
	/// </summary>
	public Header AddAdminRightsToMember => Get("AddAdminRightsToMember");
		
	/// <summary>
	/// Gets the outgoing header for the ApproveAllMembershipRequests (Unity/Flash) message.
	/// </summary>
	public Header ApproveAllMembershipRequests => Get("ApproveAllMembershipRequests");
		
	/// <summary>
	/// Gets the outgoing header for the ApproveMembershipRequest (Unity/Flash) message.
	/// </summary>
	public Header ApproveMembershipRequest => Get("ApproveMembershipRequest");
		
	/// <summary>
	/// Gets the outgoing header for the ApproveName (Flash) message.
	/// </summary>
	public Header ApproveName => Get("ApproveName");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeEmail (Unity/Flash) message.
	/// </summary>
	public Header ChangeEmail => Get("ChangeEmail");
		
	/// <summary>
	/// Gets the outgoing header for the CreateGuild (Unity/Flash) message.
	/// </summary>
	public Header CreateGuild => Get("CreateGuild");
		
	/// <summary>
	/// Gets the outgoing header for the DeactivateGuild (Unity/Flash) message.
	/// </summary>
	public Header DeactivateGuild => Get("DeactivateGuild");
		
	/// <summary>
	/// Gets the outgoing header for the DeselectFavouriteHabboGroup (Unity/Flash) message.
	/// </summary>
	public Header DeselectFavouriteHabboGroup => Get("DeselectFavouriteHabboGroup");
		
	/// <summary>
	/// Gets the outgoing header for the GetEmailStatus (Unity/Flash) message.
	/// </summary>
	public Header GetEmailStatus => Get("GetEmailStatus");
		
	/// <summary>
	/// Gets the outgoing header for the GetExtendedProfileByName (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetExtendedProfileByUsername" />.
	/// </summary>
	public Header GetExtendedProfileByName => Get("GetExtendedProfileByName");
		
	/// <summary>
	/// Gets the outgoing header for the GetExtendedProfile (Unity/Flash) message.
	/// </summary>
	public Header GetExtendedProfile => Get("GetExtendedProfile");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildCreationInfo (Unity/Flash) message.
	/// </summary>
	public Header GetGuildCreationInfo => Get("GetGuildCreationInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildEditInfo (Unity/Flash) message.
	/// </summary>
	public Header GetGuildEditInfo => Get("GetGuildEditInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildEditorData (Unity/Flash) message.
	/// </summary>
	public Header GetGuildEditorData => Get("GetGuildEditorData");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildMemberships (Unity/Flash) message.
	/// </summary>
	public Header GetGuildMemberships => Get("GetGuildMemberships");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildMembers (Unity/Flash) message.
	/// </summary>
	public Header GetGuildMembers => Get("GetGuildMembers");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboGroupBadges (Unity/Flash) message.
	/// </summary>
	public Header GetHabboGroupBadges => Get("GetHabboGroupBadges");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboGroupDetails (Unity/Flash) message.
	/// </summary>
	public Header GetHabboGroupDetails => Get("GetHabboGroupDetails");
		
	/// <summary>
	/// Gets the outgoing header for the GetIgnoredUsers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetIgnoreList" />.
	/// </summary>
	public Header GetIgnoredUsers => Get("GetIgnoredUsers");
		
	/// <summary>
	/// Gets the outgoing header for the GetMemberGuildItemCount (Flash) message.
	/// </summary>
	public Header GetMemberGuildItemCount => Get("GetMemberGuildItemCount");
		
	/// <summary>
	/// Gets the outgoing header for the GetMOTD (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetMessageOfTheDay" />.
	/// </summary>
	public Header GetMOTD => Get("GetMOTD");
		
	/// <summary>
	/// Gets the outgoing header for the GetRelationshipStatusInfo (Unity/Flash) message.
	/// </summary>
	public Header GetRelationshipStatusInfo => Get("GetRelationshipStatusInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetSelectedBadges (Unity/Flash) message.
	/// </summary>
	public Header GetSelectedBadges => Get("GetSelectedBadges");
		
	/// <summary>
	/// Gets the outgoing header for the GiveStarGemToUser (Flash) message.
	/// </summary>
	public Header GiveStarGemToUser => Get("GiveStarGemToUser");
		
	/// <summary>
	/// Gets the outgoing header for the IgnoreUserId (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.IgnoreAvatarId" />.
	/// </summary>
	public Header IgnoreUserId => Get("IgnoreUserId");
		
	/// <summary>
	/// Gets the outgoing header for the IgnoreUser (Unity/Flash) message.
	/// </summary>
	public Header IgnoreUser => Get("IgnoreUser");
		
	/// <summary>
	/// Gets the outgoing header for the JoinHabboGroup (Unity/Flash) message.
	/// </summary>
	public Header JoinHabboGroup => Get("JoinHabboGroup");
		
	/// <summary>
	/// Gets the outgoing header for the KickMember (Unity/Flash) message.
	/// </summary>
	public Header KickMember => Get("KickMember");
		
	/// <summary>
	/// Gets the outgoing header for the RejectMembershipRequest (Unity/Flash) message.
	/// </summary>
	public Header RejectMembershipRequest => Get("RejectMembershipRequest");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveAdminRightsFromMember (Unity/Flash) message.
	/// </summary>
	public Header RemoveAdminRightsFromMember => Get("RemoveAdminRightsFromMember");
		
	/// <summary>
	/// Gets the outgoing header for the ScrGetKickbackInfo (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SubscriptionGetKickbackInfo" />.
	/// </summary>
	public Header ScrGetKickbackInfo => Get("ScrGetKickbackInfo");
		
	/// <summary>
	/// Gets the outgoing header for the ScrGetUserInfo (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SubscriptionGetUserInfo" />.
	/// </summary>
	public Header ScrGetUserInfo => Get("ScrGetUserInfo");
		
	/// <summary>
	/// Gets the outgoing header for the SelectFavouriteHabboGroup (Unity/Flash) message.
	/// </summary>
	public Header SelectFavouriteHabboGroup => Get("SelectFavouriteHabboGroup");
		
	/// <summary>
	/// Gets the outgoing header for the UnblockGroupMember (Flash) message.
	/// </summary>
	public Header UnblockGroupMember => Get("UnblockGroupMember");
		
	/// <summary>
	/// Gets the outgoing header for the UnignoreUser (Unity/Flash) message.
	/// </summary>
	public Header UnignoreUser => Get("UnignoreUser");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateGuildBadge (Unity/Flash) message.
	/// </summary>
	public Header UpdateGuildBadge => Get("UpdateGuildBadge");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateGuildColors (Unity/Flash) message.
	/// </summary>
	public Header UpdateGuildColors => Get("UpdateGuildColors");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateGuildIdentity (Unity/Flash) message.
	/// </summary>
	public Header UpdateGuildIdentity => Get("UpdateGuildIdentity");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateGuildSettings (Unity/Flash) message.
	/// </summary>
	public Header UpdateGuildSettings => Get("UpdateGuildSettings");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetFriendsLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetFriendsLeaderboard => Get("Game2GetFriendsLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetTotalGroupLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetTotalGroupLeaderboard => Get("Game2GetTotalGroupLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetTotalLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetTotalLeaderboard => Get("Game2GetTotalLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetWeeklyFriendsLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetWeeklyFriendsLeaderboard => Get("Game2GetWeeklyFriendsLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetWeeklyGroupLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetWeeklyGroupLeaderboard => Get("Game2GetWeeklyGroupLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetWeeklyLeaderboard (Flash) message.
	/// </summary>
	public Header Game2GetWeeklyLeaderboard => Get("Game2GetWeeklyLeaderboard");
		
	/// <summary>
	/// Gets the outgoing header for the Game2ExitGame (Flash) message.
	/// </summary>
	public Header Game2ExitGame => Get("Game2ExitGame");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GameChat (Flash) message.
	/// </summary>
	public Header Game2GameChat => Get("Game2GameChat");
		
	/// <summary>
	/// Gets the outgoing header for the Game2LoadStageReady (Flash) message.
	/// </summary>
	public Header Game2LoadStageReady => Get("Game2LoadStageReady");
		
	/// <summary>
	/// Gets the outgoing header for the Game2PlayAgain (Flash) message.
	/// </summary>
	public Header Game2PlayAgain => Get("Game2PlayAgain");
		
	/// <summary>
	/// Gets the outgoing header for the GetSelectedNftWardrobeOutfit (Flash) message.
	/// </summary>
	public Header GetSelectedNftWardrobeOutfit => Get("GetSelectedNftWardrobeOutfit");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserNftWardrobe (Flash) message.
	/// </summary>
	public Header GetUserNftWardrobe => Get("GetUserNftWardrobe");
		
	/// <summary>
	/// Gets the outgoing header for the SaveUserNftWardrobe (Flash) message.
	/// </summary>
	public Header SaveUserNftWardrobe => Get("SaveUserNftWardrobe");
		
	/// <summary>
	/// Gets the outgoing header for the Game2CheckGameDirectoryStatus (Flash) message.
	/// </summary>
	public Header Game2CheckGameDirectoryStatus => Get("Game2CheckGameDirectoryStatus");
		
	/// <summary>
	/// Gets the outgoing header for the Game2GetAccountGameStatus (Flash) message.
	/// </summary>
	public Header Game2GetAccountGameStatus => Get("Game2GetAccountGameStatus");
		
	/// <summary>
	/// Gets the outgoing header for the Game2LeaveGame (Flash) message.
	/// </summary>
	public Header Game2LeaveGame => Get("Game2LeaveGame");
		
	/// <summary>
	/// Gets the outgoing header for the Game2QuickJoinGame (Flash) message.
	/// </summary>
	public Header Game2QuickJoinGame => Get("Game2QuickJoinGame");
		
	/// <summary>
	/// Gets the outgoing header for the Game2StartSnowWar (Flash) message.
	/// </summary>
	public Header Game2StartSnowWar => Get("Game2StartSnowWar");
		
	/// <summary>
	/// Gets the outgoing header for the ClientHello (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Hello" />.
	/// </summary>
	public Header ClientHello => Get("ClientHello");
		
	/// <summary>
	/// Gets the outgoing header for the CompleteDiffieHandshake (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.CompleteDhHandshake" />.
	/// </summary>
	public Header CompleteDiffieHandshake => Get("CompleteDiffieHandshake");
		
	/// <summary>
	/// Gets the outgoing header for the Disconnect (Unity/Flash) message.
	/// </summary>
	public Header Disconnect => Get("Disconnect");
		
	/// <summary>
	/// Gets the outgoing header for the InfoRetrieve (Unity/Flash) message.
	/// </summary>
	public Header InfoRetrieve => Get("InfoRetrieve");
		
	/// <summary>
	/// Gets the outgoing header for the InitDiffieHandshake (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.InitDhHandshake" />.
	/// </summary>
	public Header InitDiffieHandshake => Get("InitDiffieHandshake");
		
	/// <summary>
	/// Gets the outgoing header for the Pong (Unity/Flash) message.
	/// </summary>
	public Header Pong => Get("Pong");
		
	/// <summary>
	/// Gets the outgoing header for the SSOTicket (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.LoginWithTicket" />.
	/// </summary>
	public Header SSOTicket => Get("SSOTicket");
		
	/// <summary>
	/// Gets the outgoing header for the UniqueID (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UniqueMachineId" />.
	/// </summary>
	public Header UniqueID => Get("UniqueID");
		
	/// <summary>
	/// Gets the outgoing header for the VersionCheck (Unity/Flash) message.
	/// </summary>
	public Header VersionCheck => Get("VersionCheck");
		
	/// <summary>
	/// Gets the outgoing header for the AmbassadorAlert (Unity/Flash) message.
	/// </summary>
	public Header AmbassadorAlert => Get("AmbassadorAlert");
		
	/// <summary>
	/// Gets the outgoing header for the AssignRights (Unity/Flash) message.
	/// </summary>
	public Header AssignRights => Get("AssignRights");
		
	/// <summary>
	/// Gets the outgoing header for the BanUserWithDuration (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomBanWithDuration" />.
	/// </summary>
	public Header BanUserWithDuration => Get("BanUserWithDuration");
		
	/// <summary>
	/// Gets the outgoing header for the KickUser (Unity/Flash) message.
	/// </summary>
	public Header KickUser => Get("KickUser");
		
	/// <summary>
	/// Gets the outgoing header for the LetUserIn (Flash) message.
	/// </summary>
	public Header LetUserIn => Get("LetUserIn");
		
	/// <summary>
	/// Gets the outgoing header for the MuteAllInRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomMuteAll" />.
	/// </summary>
	public Header MuteAllInRoom => Get("MuteAllInRoom");
		
	/// <summary>
	/// Gets the outgoing header for the MuteUser (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomMuteUser" />.
	/// </summary>
	public Header MuteUser => Get("MuteUser");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveAllRights (Unity/Flash) message.
	/// </summary>
	public Header RemoveAllRights => Get("RemoveAllRights");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveRights (Unity/Flash) message.
	/// </summary>
	public Header RemoveRights => Get("RemoveRights");
		
	/// <summary>
	/// Gets the outgoing header for the UnbanUserFromRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomUnbanUser" />.
	/// </summary>
	public Header UnbanUserFromRoom => Get("UnbanUserFromRoom");
		
	/// <summary>
	/// Gets the outgoing header for the UnmuteUser (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomUnmuteUser" />.
	/// </summary>
	public Header UnmuteUser => Get("UnmuteUser");
		
	/// <summary>
	/// Gets the outgoing header for the MysteryBoxWaitingCanceled (Unity/Flash) message.
	/// </summary>
	public Header MysteryBoxWaitingCanceled => Get("MysteryBoxWaitingCanceled");
		
	/// <summary>
	/// Gets the outgoing header for the GetCreditsInfo (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetCredits" />.
	/// </summary>
	public Header GetCreditsInfo => Get("GetCreditsInfo");
		
	/// <summary>
	/// Gets the outgoing header for the AddFavouriteRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.AddYourFavouriteRoom" />.
	/// </summary>
	public Header AddFavouriteRoom => Get("AddFavouriteRoom");
		
	/// <summary>
	/// Gets the outgoing header for the CancelEvent (Flash) message.
	/// </summary>
	public Header CancelEvent => Get("CancelEvent");
		
	/// <summary>
	/// Gets the outgoing header for the CanCreateRoom (Unity/Flash) message.
	/// </summary>
	public Header CanCreateRoom => Get("CanCreateRoom");
		
	/// <summary>
	/// Gets the outgoing header for the CompetitionRoomsSearch (Unity/Flash) message.
	/// </summary>
	public Header CompetitionRoomsSearch => Get("CompetitionRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the ConvertGlobalRoomId (Unity/Flash) message.
	/// </summary>
	public Header ConvertGlobalRoomId => Get("ConvertGlobalRoomId");
		
	/// <summary>
	/// Gets the outgoing header for the CreateFlat (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.CreateNewFlat" />.
	/// </summary>
	public Header CreateFlat => Get("CreateFlat");
		
	/// <summary>
	/// Gets the outgoing header for the DeleteFavouriteRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.DeleteYourFavouriteRoom" />.
	/// </summary>
	public Header DeleteFavouriteRoom => Get("DeleteFavouriteRoom");
		
	/// <summary>
	/// Gets the outgoing header for the EditEvent (Flash) message.
	/// </summary>
	public Header EditEvent => Get("EditEvent");
		
	/// <summary>
	/// Gets the outgoing header for the ForwardToARandomPromotedRoom (Unity/Flash) message.
	/// </summary>
	public Header ForwardToARandomPromotedRoom => Get("ForwardToARandomPromotedRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ForwardToSomeRoom (Unity/Flash) message.
	/// </summary>
	public Header ForwardToSomeRoom => Get("ForwardToSomeRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuestRoom (Unity/Flash) message.
	/// </summary>
	public Header GetGuestRoom => Get("GetGuestRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GetOfficialRooms (Unity/Flash) message.
	/// </summary>
	public Header GetOfficialRooms => Get("GetOfficialRooms");
		
	/// <summary>
	/// Gets the outgoing header for the GetPopularRoomTags (Unity/Flash) message.
	/// </summary>
	public Header GetPopularRoomTags => Get("GetPopularRoomTags");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserEventCats (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetEventFlatCats" />.
	/// </summary>
	public Header GetUserEventCats => Get("GetUserEventCats");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserFlatCats (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetUserFlatCategories" />.
	/// </summary>
	public Header GetUserFlatCats => Get("GetUserFlatCats");
		
	/// <summary>
	/// Gets the outgoing header for the GuildBaseSearch (Unity/Flash) message.
	/// </summary>
	public Header GuildBaseSearch => Get("GuildBaseSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyFavouriteRoomsSearch (Unity/Flash) message.
	/// </summary>
	public Header MyFavouriteRoomsSearch => Get("MyFavouriteRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyFrequentRoomHistorySearch (Flash) message.
	/// </summary>
	public Header MyFrequentRoomHistorySearch => Get("MyFrequentRoomHistorySearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyFriendsRoomsSearch (Unity/Flash) message.
	/// </summary>
	public Header MyFriendsRoomsSearch => Get("MyFriendsRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyGuildBasesSearch (Unity/Flash) message.
	/// </summary>
	public Header MyGuildBasesSearch => Get("MyGuildBasesSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyRecommendedRooms (Flash) message.
	/// </summary>
	public Header MyRecommendedRooms => Get("MyRecommendedRooms");
		
	/// <summary>
	/// Gets the outgoing header for the MyRoomHistorySearch (Unity/Flash) message.
	/// </summary>
	public Header MyRoomHistorySearch => Get("MyRoomHistorySearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyRoomRightsSearch (Unity/Flash) message.
	/// </summary>
	public Header MyRoomRightsSearch => Get("MyRoomRightsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyRoomsSearch (Unity/Flash) message.
	/// </summary>
	public Header MyRoomsSearch => Get("MyRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the PopularRoomsSearch (Unity/Flash) message.
	/// </summary>
	public Header PopularRoomsSearch => Get("PopularRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the RateFlat (Unity/Flash) message.
	/// </summary>
	public Header RateFlat => Get("RateFlat");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveOwnRoomRightsRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RemoveOwnRights" />.
	/// </summary>
	public Header RemoveOwnRoomRightsRoom => Get("RemoveOwnRoomRightsRoom");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdEventTabAdClicked (Unity/Flash) message.
	/// </summary>
	public Header RoomAdEventTabAdClicked => Get("RoomAdEventTabAdClicked");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdEventTabViewed (Unity/Flash) message.
	/// </summary>
	public Header RoomAdEventTabViewed => Get("RoomAdEventTabViewed");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdSearch (Unity/Flash) message.
	/// </summary>
	public Header RoomAdSearch => Get("RoomAdSearch");
		
	/// <summary>
	/// Gets the outgoing header for the RoomsWhereMyFriendsAreSearch (Unity/Flash) message.
	/// </summary>
	public Header RoomsWhereMyFriendsAreSearch => Get("RoomsWhereMyFriendsAreSearch");
		
	/// <summary>
	/// Gets the outgoing header for the RoomsWithHighestScoreSearch (Unity/Flash) message.
	/// </summary>
	public Header RoomsWithHighestScoreSearch => Get("RoomsWithHighestScoreSearch");
		
	/// <summary>
	/// Gets the outgoing header for the RoomTextSearch (Unity/Flash) message.
	/// </summary>
	public Header RoomTextSearch => Get("RoomTextSearch");
		
	/// <summary>
	/// Gets the outgoing header for the SetRoomSessionTags (Unity/Flash) message.
	/// </summary>
	public Header SetRoomSessionTags => Get("SetRoomSessionTags");
		
	/// <summary>
	/// Gets the outgoing header for the ToggleStaffPick (Flash) message.
	/// </summary>
	public Header ToggleStaffPick => Get("ToggleStaffPick");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateHomeRoom (Flash) message.
	/// </summary>
	public Header UpdateHomeRoom => Get("UpdateHomeRoom");
		
	/// <summary>
	/// Gets the outgoing header for the CommunityGoalVote (Unity/Flash) message.
	/// </summary>
	public Header CommunityGoalVote => Get("CommunityGoalVote");
		
	/// <summary>
	/// Gets the outgoing header for the GetHotlooks (Unity/Flash) message.
	/// </summary>
	public Header GetHotlooks => Get("GetHotlooks");
		
	/// <summary>
	/// Gets the outgoing header for the CreditVaultStatus (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.VaultStatus" />.
	/// </summary>
	public Header CreditVaultStatus => Get("CreditVaultStatus");
		
	/// <summary>
	/// Gets the outgoing header for the IncomeRewardClaim (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ClaimEarning" />.
	/// </summary>
	public Header IncomeRewardClaim => Get("IncomeRewardClaim");
		
	/// <summary>
	/// Gets the outgoing header for the IncomeRewardStatus (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.EarningStatus" />.
	/// </summary>
	public Header IncomeRewardStatus => Get("IncomeRewardStatus");
		
	/// <summary>
	/// Gets the outgoing header for the WithdrawCreditVault (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.WithdrawVault" />.
	/// </summary>
	public Header WithdrawCreditVault => Get("WithdrawCreditVault");
		
	/// <summary>
	/// Gets the outgoing header for the SetChatPreferences (Unity/Flash) message.
	/// </summary>
	public Header SetChatPreferences => Get("SetChatPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the SetChatStylePreference (Unity/Flash) message.
	/// </summary>
	public Header SetChatStylePreference => Get("SetChatStylePreference");
		
	/// <summary>
	/// Gets the outgoing header for the SetIgnoreRoomInvites (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SetRoomInvitePreferences" />.
	/// </summary>
	public Header SetIgnoreRoomInvites => Get("SetIgnoreRoomInvites");
		
	/// <summary>
	/// Gets the outgoing header for the SetNewNavigatorWindowPreferences (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SetNewNavigatorPreferences" />.
	/// </summary>
	public Header SetNewNavigatorWindowPreferences => Get("SetNewNavigatorWindowPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the SetRoomCameraPreferences (Unity/Flash) message.
	/// </summary>
	public Header SetRoomCameraPreferences => Get("SetRoomCameraPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the SetSoundSettings (Unity/Flash) message.
	/// </summary>
	public Header SetSoundSettings => Get("SetSoundSettings");
		
	/// <summary>
	/// Gets the outgoing header for the SetUiFlags (Unity/Flash) message.
	/// </summary>
	public Header SetUiFlags => Get("SetUiFlags");
		
	/// <summary>
	/// Gets the outgoing header for the Game2MakeSnowball (Flash) message.
	/// </summary>
	public Header Game2MakeSnowball => Get("Game2MakeSnowball");
		
	/// <summary>
	/// Gets the outgoing header for the Game2RequestFullStatusUpdate (Flash) message.
	/// </summary>
	public Header Game2RequestFullStatusUpdate => Get("Game2RequestFullStatusUpdate");
		
	/// <summary>
	/// Gets the outgoing header for the Game2SetUserMoveTarget (Flash) message.
	/// </summary>
	public Header Game2SetUserMoveTarget => Get("Game2SetUserMoveTarget");
		
	/// <summary>
	/// Gets the outgoing header for the Game2ThrowSnowballAtHuman (Flash) message.
	/// </summary>
	public Header Game2ThrowSnowballAtHuman => Get("Game2ThrowSnowballAtHuman");
		
	/// <summary>
	/// Gets the outgoing header for the Game2ThrowSnowballAtPosition (Flash) message.
	/// </summary>
	public Header Game2ThrowSnowballAtPosition => Get("Game2ThrowSnowballAtPosition");
		
	/// <summary>
	/// Gets the outgoing header for the AddSpamWallPostIt (Unity/Flash) message.
	/// </summary>
	public Header AddSpamWallPostIt => Get("AddSpamWallPostIt");
		
	/// <summary>
	/// Gets the outgoing header for the ControlYoutubeDisplayPlayback (Flash) message.
	/// </summary>
	public Header ControlYoutubeDisplayPlayback => Get("ControlYoutubeDisplayPlayback");
		
	/// <summary>
	/// Gets the outgoing header for the CreditFurniRedeem (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ConvertFurniToCredits" />.
	/// </summary>
	public Header CreditFurniRedeem => Get("CreditFurniRedeem");
		
	/// <summary>
	/// Gets the outgoing header for the DiceOff (Unity/Flash) message.
	/// </summary>
	public Header DiceOff => Get("DiceOff");
		
	/// <summary>
	/// Gets the outgoing header for the EnterOneWayDoor (Unity/Flash) message.
	/// </summary>
	public Header EnterOneWayDoor => Get("EnterOneWayDoor");
		
	/// <summary>
	/// Gets the outgoing header for the ExtendRentOrBuyoutFurni (Flash) message.
	/// </summary>
	public Header ExtendRentOrBuyoutFurni => Get("ExtendRentOrBuyoutFurni");
		
	/// <summary>
	/// Gets the outgoing header for the ExtendRentOrBuyoutStripItem (Flash) message.
	/// </summary>
	public Header ExtendRentOrBuyoutStripItem => Get("ExtendRentOrBuyoutStripItem");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildFurniContextMenuInfo (Unity/Flash) message.
	/// </summary>
	public Header GetGuildFurniContextMenuInfo => Get("GetGuildFurniContextMenuInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetRentOrBuyoutOffer (Unity/Flash) message.
	/// </summary>
	public Header GetRentOrBuyoutOffer => Get("GetRentOrBuyoutOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetYoutubeDisplayStatus (Flash) message.
	/// </summary>
	public Header GetYoutubeDisplayStatus => Get("GetYoutubeDisplayStatus");
		
	/// <summary>
	/// Gets the outgoing header for the OpenMysteryTrophy (Unity/Flash) message.
	/// </summary>
	public Header OpenMysteryTrophy => Get("OpenMysteryTrophy");
		
	/// <summary>
	/// Gets the outgoing header for the OpenPetPackage (Unity/Flash) message.
	/// </summary>
	public Header OpenPetPackage => Get("OpenPetPackage");
		
	/// <summary>
	/// Gets the outgoing header for the PlacePostIt (Unity/Flash) message.
	/// </summary>
	public Header PlacePostIt => Get("PlacePostIt");
		
	/// <summary>
	/// Gets the outgoing header for the PresentOpen (Unity/Flash) message.
	/// </summary>
	public Header PresentOpen => Get("PresentOpen");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceCancelRent (Flash) message.
	/// </summary>
	public Header RentableSpaceCancelRent => Get("RentableSpaceCancelRent");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceRent (Flash) message.
	/// </summary>
	public Header RentableSpaceRent => Get("RentableSpaceRent");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceStatus (Flash) message.
	/// </summary>
	public Header RentableSpaceStatus => Get("RentableSpaceStatus");
		
	/// <summary>
	/// Gets the outgoing header for the RoomDimmerChangeState (Unity/Flash) message.
	/// </summary>
	public Header RoomDimmerChangeState => Get("RoomDimmerChangeState");
		
	/// <summary>
	/// Gets the outgoing header for the RoomDimmerGetPresets (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomDimmerEditPresets" />.
	/// </summary>
	public Header RoomDimmerGetPresets => Get("RoomDimmerGetPresets");
		
	/// <summary>
	/// Gets the outgoing header for the RoomDimmerSavePreset (Unity/Flash) message.
	/// </summary>
	public Header RoomDimmerSavePreset => Get("RoomDimmerSavePreset");
		
	/// <summary>
	/// Gets the outgoing header for the SetCustomStackingHeight (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.StackingHelperSetCaretHeight" />.
	/// </summary>
	public Header SetCustomStackingHeight => Get("SetCustomStackingHeight");
		
	/// <summary>
	/// Gets the outgoing header for the SetMannequinFigure (Unity/Flash) message.
	/// </summary>
	public Header SetMannequinFigure => Get("SetMannequinFigure");
		
	/// <summary>
	/// Gets the outgoing header for the SetMannequinName (Unity/Flash) message.
	/// </summary>
	public Header SetMannequinName => Get("SetMannequinName");
		
	/// <summary>
	/// Gets the outgoing header for the SetRandomState (Flash) message.
	/// </summary>
	public Header SetRandomState => Get("SetRandomState");
		
	/// <summary>
	/// Gets the outgoing header for the SetRoomBackgroundColorData (Unity/Flash) message.
	/// </summary>
	public Header SetRoomBackgroundColorData => Get("SetRoomBackgroundColorData");
		
	/// <summary>
	/// Gets the outgoing header for the SetYoutubeDisplayPlaylist (Flash) message.
	/// </summary>
	public Header SetYoutubeDisplayPlaylist => Get("SetYoutubeDisplayPlaylist");
		
	/// <summary>
	/// Gets the outgoing header for the SpinWheelOfFortune (Unity/Flash) message.
	/// </summary>
	public Header SpinWheelOfFortune => Get("SpinWheelOfFortune");
		
	/// <summary>
	/// Gets the outgoing header for the ThrowDice (Unity/Flash) message.
	/// </summary>
	public Header ThrowDice => Get("ThrowDice");
		
	/// <summary>
	/// Gets the outgoing header for the GetAchievements (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetUserAchievements" />.
	/// </summary>
	public Header GetAchievements => Get("GetAchievements");
		
	/// <summary>
	/// Gets the outgoing header for the RequestFurniInventory (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetInventory" />.
	/// </summary>
	public Header RequestFurniInventory => Get("RequestFurniInventory");
		
	/// <summary>
	/// Gets the outgoing header for the RequestFurniInventoryWhenNotInRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetInventoryPeer" />.
	/// </summary>
	public Header RequestFurniInventoryWhenNotInRoom => Get("RequestFurniInventoryWhenNotInRoom");
		
	/// <summary>
	/// Gets the outgoing header for the RequestRoomPropertySet (Flash) message.
	/// </summary>
	public Header RequestRoomPropertySet => Get("RequestRoomPropertySet");
		
	/// <summary>
	/// Gets the outgoing header for the AcceptFriend (Unity/Flash) message.
	/// </summary>
	public Header AcceptFriend => Get("AcceptFriend");
		
	/// <summary>
	/// Gets the outgoing header for the DeclineFriend (Unity/Flash) message.
	/// </summary>
	public Header DeclineFriend => Get("DeclineFriend");
		
	/// <summary>
	/// Gets the outgoing header for the FindNewFriends (Flash) message.
	/// </summary>
	public Header FindNewFriends => Get("FindNewFriends");
		
	/// <summary>
	/// Gets the outgoing header for the FollowFriend (Unity/Flash) message.
	/// </summary>
	public Header FollowFriend => Get("FollowFriend");
		
	/// <summary>
	/// Gets the outgoing header for the FriendListUpdate (Unity/Flash) message.
	/// </summary>
	public Header FriendListUpdate => Get("FriendListUpdate");
		
	/// <summary>
	/// Gets the outgoing header for the GetFriendRequests (Unity/Flash) message.
	/// </summary>
	public Header GetFriendRequests => Get("GetFriendRequests");
		
	/// <summary>
	/// Gets the outgoing header for the HabboSearch (Unity/Flash) message.
	/// </summary>
	public Header HabboSearch => Get("HabboSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MessengerInit (Unity/Flash) message.
	/// </summary>
	public Header MessengerInit => Get("MessengerInit");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveFriend (Unity/Flash) message.
	/// </summary>
	public Header RemoveFriend => Get("RemoveFriend");
		
	/// <summary>
	/// Gets the outgoing header for the RequestFriend (Unity/Flash) message.
	/// </summary>
	public Header RequestFriend => Get("RequestFriend");
		
	/// <summary>
	/// Gets the outgoing header for the SendMsg (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SendMessage" />.
	/// </summary>
	public Header SendMsg => Get("SendMsg");
		
	/// <summary>
	/// Gets the outgoing header for the SendRoomInvite (Unity/Flash) message.
	/// </summary>
	public Header SendRoomInvite => Get("SendRoomInvite");
		
	/// <summary>
	/// Gets the outgoing header for the SetRelationshipStatus (Unity/Flash) message.
	/// </summary>
	public Header SetRelationshipStatus => Get("SetRelationshipStatus");
		
	/// <summary>
	/// Gets the outgoing header for the VisitUser (Unity/Flash) message.
	/// </summary>
	public Header VisitUser => Get("VisitUser");
		
	/// <summary>
	/// Gets the outgoing header for the BuyMarketplaceOffer (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceBuyOffer" />.
	/// </summary>
	public Header BuyMarketplaceOffer => Get("BuyMarketplaceOffer");
		
	/// <summary>
	/// Gets the outgoing header for the BuyMarketplaceTokens (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceBuyTokens" />.
	/// </summary>
	public Header BuyMarketplaceTokens => Get("BuyMarketplaceTokens");
		
	/// <summary>
	/// Gets the outgoing header for the CancelMarketplaceOffer (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceCancelOffer" />.
	/// </summary>
	public Header CancelMarketplaceOffer => Get("CancelMarketplaceOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetMarketplaceCanMakeOffer (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceCanMakeOffer" />.
	/// </summary>
	public Header GetMarketplaceCanMakeOffer => Get("GetMarketplaceCanMakeOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetMarketplaceConfiguration (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceGetConfiguration" />.
	/// </summary>
	public Header GetMarketplaceConfiguration => Get("GetMarketplaceConfiguration");
		
	/// <summary>
	/// Gets the outgoing header for the GetMarketplaceItemStats (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceGetItemStats" />.
	/// </summary>
	public Header GetMarketplaceItemStats => Get("GetMarketplaceItemStats");
		
	/// <summary>
	/// Gets the outgoing header for the GetMarketplaceOffers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceSearchOffers" />.
	/// </summary>
	public Header GetMarketplaceOffers => Get("GetMarketplaceOffers");
		
	/// <summary>
	/// Gets the outgoing header for the GetMarketplaceOwnOffers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceListOwnOffers" />.
	/// </summary>
	public Header GetMarketplaceOwnOffers => Get("GetMarketplaceOwnOffers");
		
	/// <summary>
	/// Gets the outgoing header for the MakeOffer (Flash) message.
	/// </summary>
	public Header MakeOffer => Get("MakeOffer");
		
	/// <summary>
	/// Gets the outgoing header for the RedeemMarketplaceOfferCredits (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.MarketplaceRedeemOfferCredits" />.
	/// </summary>
	public Header RedeemMarketplaceOfferCredits => Get("RedeemMarketplaceOfferCredits");
		
	/// <summary>
	/// Gets the outgoing header for the GetTalentTrackLevel (Unity/Flash) message.
	/// </summary>
	public Header GetTalentTrackLevel => Get("GetTalentTrackLevel");
		
	/// <summary>
	/// Gets the outgoing header for the GetTalentTrack (Unity/Flash) message.
	/// </summary>
	public Header GetTalentTrack => Get("GetTalentTrack");
		
	/// <summary>
	/// Gets the outgoing header for the GuideAdvertisementRead (Unity/Flash) message.
	/// </summary>
	public Header GuideAdvertisementRead => Get("GuideAdvertisementRead");
		
	/// <summary>
	/// Gets the outgoing header for the GetForumsList (Unity/Flash) message.
	/// </summary>
	public Header GetForumsList => Get("GetForumsList");
		
	/// <summary>
	/// Gets the outgoing header for the GetForumStats (Unity/Flash) message.
	/// </summary>
	public Header GetForumStats => Get("GetForumStats");
		
	/// <summary>
	/// Gets the outgoing header for the GetMessages (Flash) message.
	/// </summary>
	public Header GetMessages => Get("GetMessages");
		
	/// <summary>
	/// Gets the outgoing header for the GetThread (Flash) message.
	/// </summary>
	public Header GetThread => Get("GetThread");
		
	/// <summary>
	/// Gets the outgoing header for the GetThreads (Flash) message.
	/// </summary>
	public Header GetThreads => Get("GetThreads");
		
	/// <summary>
	/// Gets the outgoing header for the GetUnreadForumsCount (Unity/Flash) message.
	/// </summary>
	public Header GetUnreadForumsCount => Get("GetUnreadForumsCount");
		
	/// <summary>
	/// Gets the outgoing header for the ModerateMessage (Flash) message.
	/// </summary>
	public Header ModerateMessage => Get("ModerateMessage");
		
	/// <summary>
	/// Gets the outgoing header for the ModerateThread (Flash) message.
	/// </summary>
	public Header ModerateThread => Get("ModerateThread");
		
	/// <summary>
	/// Gets the outgoing header for the PostMessage (Flash) message.
	/// </summary>
	public Header PostMessage => Get("PostMessage");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateForumReadMarker (Flash) message.
	/// </summary>
	public Header UpdateForumReadMarker => Get("UpdateForumReadMarker");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateForumSettings (Unity/Flash) message.
	/// </summary>
	public Header UpdateForumSettings => Get("UpdateForumSettings");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateThread (Flash) message.
	/// </summary>
	public Header UpdateThread => Get("UpdateThread");
		
	/// <summary>
	/// Gets the outgoing header for the RoomNetworkOpenConnection (Flash) message.
	/// </summary>
	public Header RoomNetworkOpenConnection => Get("RoomNetworkOpenConnection");
		
	/// <summary>
	/// Gets the outgoing header for the GetResolutionAchievements (Flash) message.
	/// </summary>
	public Header GetResolutionAchievements => Get("GetResolutionAchievements");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeQueue (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RoomQueueChange" />.
	/// </summary>
	public Header ChangeQueue => Get("ChangeQueue");
		
	/// <summary>
	/// Gets the outgoing header for the OpenFlatConnection (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.FlatOpc" />.
	/// </summary>
	public Header OpenFlatConnection => Get("OpenFlatConnection");
		
	/// <summary>
	/// Gets the outgoing header for the Quit (Unity/Flash) message.
	/// </summary>
	public Header Quit => Get("Quit");
		
	/// <summary>
	/// Gets the outgoing header for the PeerUsersClassification (Flash) message.
	/// </summary>
	public Header PeerUsersClassification => Get("PeerUsersClassification");
		
	/// <summary>
	/// Gets the outgoing header for the RoomUsersClassification (Flash) message.
	/// </summary>
	public Header RoomUsersClassification => Get("RoomUsersClassification");
		
	/// <summary>
	/// Gets the outgoing header for the EventLog (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.LogToEventLog" />.
	/// </summary>
	public Header EventLog => Get("EventLog");
		
	/// <summary>
	/// Gets the outgoing header for the LagWarningReport (Flash) message.
	/// </summary>
	public Header LagWarningReport => Get("LagWarningReport");
		
	/// <summary>
	/// Gets the outgoing header for the LatencyPingReport (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ClientLatencyPingReport" />.
	/// </summary>
	public Header LatencyPingReport => Get("LatencyPingReport");
		
	/// <summary>
	/// Gets the outgoing header for the LatencyPingRequest (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ClientLatencyPingRequest" />.
	/// </summary>
	public Header LatencyPingRequest => Get("LatencyPingRequest");
		
	/// <summary>
	/// Gets the outgoing header for the PerformanceLog (Flash) message.
	/// </summary>
	public Header PerformanceLog => Get("PerformanceLog");
		
	/// <summary>
	/// Gets the outgoing header for the AddJukeboxDisk (Flash) message.
	/// </summary>
	public Header AddJukeboxDisk => Get("AddJukeboxDisk");
		
	/// <summary>
	/// Gets the outgoing header for the GetJukeboxPlayList (Flash) message.
	/// </summary>
	public Header GetJukeboxPlayList => Get("GetJukeboxPlayList");
		
	/// <summary>
	/// Gets the outgoing header for the GetNowPlaying (Unity/Flash) message.
	/// </summary>
	public Header GetNowPlaying => Get("GetNowPlaying");
		
	/// <summary>
	/// Gets the outgoing header for the GetOfficialSongId (Flash) message.
	/// </summary>
	public Header GetOfficialSongId => Get("GetOfficialSongId");
		
	/// <summary>
	/// Gets the outgoing header for the GetSongInfo (Unity/Flash) message.
	/// </summary>
	public Header GetSongInfo => Get("GetSongInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetSoundMachinePlayList (Unity/Flash) message.
	/// </summary>
	public Header GetSoundMachinePlayList => Get("GetSoundMachinePlayList");
		
	/// <summary>
	/// Gets the outgoing header for the GetSoundSettings (Flash) message.
	/// </summary>
	public Header GetSoundSettings => Get("GetSoundSettings");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserSongDisks (Flash) message.
	/// </summary>
	public Header GetUserSongDisks => Get("GetUserSongDisks");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveJukeboxDisk (Flash) message.
	/// </summary>
	public Header RemoveJukeboxDisk => Get("RemoveJukeboxDisk");
		
	/// <summary>
	/// Gets the outgoing header for the FriendFurniConfirmLock (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.FriendFurnitureLockConfirm" />.
	/// </summary>
	public Header FriendFurniConfirmLock => Get("FriendFurniConfirmLock");
		
	/// <summary>
	/// Gets the outgoing header for the BreedPets (Unity/Flash) message.
	/// </summary>
	public Header BreedPets => Get("BreedPets");
		
	/// <summary>
	/// Gets the outgoing header for the CustomizePetWithFurni (Unity/Flash) message.
	/// </summary>
	public Header CustomizePetWithFurni => Get("CustomizePetWithFurni");
		
	/// <summary>
	/// Gets the outgoing header for the GetPetInfo (Flash) message.
	/// </summary>
	public Header GetPetInfo => Get("GetPetInfo");
		
	/// <summary>
	/// Gets the outgoing header for the PetSelected (Unity/Flash) message.
	/// </summary>
	public Header PetSelected => Get("PetSelected");
		
	/// <summary>
	/// Gets the outgoing header for the RespectPet (Unity/Flash) message.
	/// </summary>
	public Header RespectPet => Get("RespectPet");
		
	/// <summary>
	/// Gets the outgoing header for the NewUserExperienceGetGifts (Flash) message.
	/// </summary>
	public Header NewUserExperienceGetGifts => Get("NewUserExperienceGetGifts");
		
	/// <summary>
	/// Gets the outgoing header for the NewUserExperienceScriptProceed (Flash) message.
	/// </summary>
	public Header NewUserExperienceScriptProceed => Get("NewUserExperienceScriptProceed");
		
	/// <summary>
	/// Gets the outgoing header for the SelectInitialRoom (Unity/Flash) message.
	/// </summary>
	public Header SelectInitialRoom => Get("SelectInitialRoom");
		
	/// <summary>
	/// Gets the outgoing header for the OpenCampaignCalendarDoorAsStaff (Flash) message.
	/// </summary>
	public Header OpenCampaignCalendarDoorAsStaff => Get("OpenCampaignCalendarDoorAsStaff");
		
	/// <summary>
	/// Gets the outgoing header for the OpenCampaignCalendarDoor (Unity/Flash) message.
	/// </summary>
	public Header OpenCampaignCalendarDoor => Get("OpenCampaignCalendarDoor");
		
	/// <summary>
	/// Gets the outgoing header for the DeleteRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.DeleteFlat" />.
	/// </summary>
	public Header DeleteRoom => Get("DeleteRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GetBannedUsersFromRoom (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetBannedUsers" />.
	/// </summary>
	public Header GetBannedUsersFromRoom => Get("GetBannedUsersFromRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GetCustomRoomFilter (Flash) message.
	/// </summary>
	public Header GetCustomRoomFilter => Get("GetCustomRoomFilter");
		
	/// <summary>
	/// Gets the outgoing header for the GetFlatControllers (Unity/Flash) message.
	/// </summary>
	public Header GetFlatControllers => Get("GetFlatControllers");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomSettings (Unity/Flash) message.
	/// </summary>
	public Header GetRoomSettings => Get("GetRoomSettings");
		
	/// <summary>
	/// Gets the outgoing header for the SaveRoomSettings (Unity/Flash) message.
	/// </summary>
	public Header SaveRoomSettings => Get("SaveRoomSettings");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateRoomCategoryAndTradeSettings (Flash) message.
	/// </summary>
	public Header UpdateRoomCategoryAndTradeSettings => Get("UpdateRoomCategoryAndTradeSettings");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateRoomFilter (Unity/Flash) message.
	/// </summary>
	public Header UpdateRoomFilter => Get("UpdateRoomFilter");
		
	/// <summary>
	/// Gets the outgoing header for the GetInterstitial (Unity/Flash) message.
	/// </summary>
	public Header GetInterstitial => Get("GetInterstitial");
		
	/// <summary>
	/// Gets the outgoing header for the InterstitialShown (Unity/Flash) message.
	/// </summary>
	public Header InterstitialShown => Get("InterstitialShown");
		
	/// <summary>
	/// Gets the outgoing header for the GetBotInventory (Unity/Flash) message.
	/// </summary>
	public Header GetBotInventory => Get("GetBotInventory");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeUserNameInRoom (Flash) message.
	/// </summary>
	public Header ChangeUserNameInRoom => Get("ChangeUserNameInRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeUserName (Flash) message.
	/// </summary>
	public Header ChangeUserName => Get("ChangeUserName");
		
	/// <summary>
	/// Gets the outgoing header for the CheckUserName (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.CheckAvatarName" />.
	/// </summary>
	public Header CheckUserName => Get("CheckUserName");
		
	/// <summary>
	/// Gets the outgoing header for the GetWardrobe (Unity/Flash) message.
	/// </summary>
	public Header GetWardrobe => Get("GetWardrobe");
		
	/// <summary>
	/// Gets the outgoing header for the SaveWardrobeOutfit (Unity/Flash) message.
	/// </summary>
	public Header SaveWardrobeOutfit => Get("SaveWardrobeOutfit");
		
	/// <summary>
	/// Gets the outgoing header for the AvatarExpression (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Expression" />.
	/// </summary>
	public Header AvatarExpression => Get("AvatarExpression");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeMotto (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ChangeAvatarMotto" />.
	/// </summary>
	public Header ChangeMotto => Get("ChangeMotto");
		
	/// <summary>
	/// Gets the outgoing header for the ChangePosture (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.Posture" />.
	/// </summary>
	public Header ChangePosture => Get("ChangePosture");
		
	/// <summary>
	/// Gets the outgoing header for the CustomizeAvatarWithFurni (Unity/Flash) message.
	/// </summary>
	public Header CustomizeAvatarWithFurni => Get("CustomizeAvatarWithFurni");
		
	/// <summary>
	/// Gets the outgoing header for the Dance (Unity/Flash) message.
	/// </summary>
	public Header Dance => Get("Dance");
		
	/// <summary>
	/// Gets the outgoing header for the DropCarryItem (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.DropHandItem" />.
	/// </summary>
	public Header DropCarryItem => Get("DropCarryItem");
		
	/// <summary>
	/// Gets the outgoing header for the LookTo (Unity/Flash) message.
	/// </summary>
	public Header LookTo => Get("LookTo");
		
	/// <summary>
	/// Gets the outgoing header for the PassCarryItem (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.PassHandItem" />.
	/// </summary>
	public Header PassCarryItem => Get("PassCarryItem");
		
	/// <summary>
	/// Gets the outgoing header for the PassCarryItemToPet (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.PassHandItemToPet" />.
	/// </summary>
	public Header PassCarryItemToPet => Get("PassCarryItemToPet");
		
	/// <summary>
	/// Gets the outgoing header for the Sign (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ShowSign" />.
	/// </summary>
	public Header Sign => Get("Sign");
		
	/// <summary>
	/// Gets the outgoing header for the GetOccupiedTiles (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetRoomOccupiedTiles" />.
	/// </summary>
	public Header GetOccupiedTiles => Get("GetOccupiedTiles");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomEntryTile (Unity/Flash) message.
	/// </summary>
	public Header GetRoomEntryTile => Get("GetRoomEntryTile");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateFloorProperties (Flash) message.
	/// </summary>
	public Header UpdateFloorProperties => Get("UpdateFloorProperties");
		
	/// <summary>
	/// Gets the outgoing header for the CommandBot (Unity/Flash) message.
	/// </summary>
	public Header CommandBot => Get("CommandBot");
		
	/// <summary>
	/// Gets the outgoing header for the GetBotCommandConfigurationData (Unity/Flash) message.
	/// </summary>
	public Header GetBotCommandConfigurationData => Get("GetBotCommandConfigurationData");
		
	/// <summary>
	/// Gets the outgoing header for the ResetPhoneNumberState (Flash) message.
	/// </summary>
	public Header ResetPhoneNumberState => Get("ResetPhoneNumberState");
		
	/// <summary>
	/// Gets the outgoing header for the SetPhoneNumberVerificationStatus (Flash) message.
	/// </summary>
	public Header SetPhoneNumberVerificationStatus => Get("SetPhoneNumberVerificationStatus");
		
	/// <summary>
	/// Gets the outgoing header for the TryPhoneNumber (Unity/Flash) message.
	/// </summary>
	public Header TryPhoneNumber => Get("TryPhoneNumber");
		
	/// <summary>
	/// Gets the outgoing header for the VerifyCode (Unity/Flash) message.
	/// </summary>
	public Header VerifyCode => Get("VerifyCode");
		
	/// <summary>
	/// Gets the outgoing header for the AvatarEffectActivated (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.ActivateAvatarEffect" />.
	/// </summary>
	public Header AvatarEffectActivated => Get("AvatarEffectActivated");
		
	/// <summary>
	/// Gets the outgoing header for the AvatarEffectSelected (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UseAvatarEffect" />.
	/// </summary>
	public Header AvatarEffectSelected => Get("AvatarEffectSelected");
		
	/// <summary>
	/// Gets the outgoing header for the CancelTyping (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UserCancelTyping" />.
	/// </summary>
	public Header CancelTyping => Get("CancelTyping");
		
	/// <summary>
	/// Gets the outgoing header for the Chat (Unity/Flash) message.
	/// </summary>
	public Header Chat => Get("Chat");
		
	/// <summary>
	/// Gets the outgoing header for the Shout (Unity/Flash) message.
	/// </summary>
	public Header Shout => Get("Shout");
		
	/// <summary>
	/// Gets the outgoing header for the StartTyping (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.UserStartTyping" />.
	/// </summary>
	public Header StartTyping => Get("StartTyping");
		
	/// <summary>
	/// Gets the outgoing header for the Whisper (Unity/Flash) message.
	/// </summary>
	public Header Whisper => Get("Whisper");
		
	/// <summary>
	/// Gets the outgoing header for the AcceptQuest (Unity/Flash) message.
	/// </summary>
	public Header AcceptQuest => Get("AcceptQuest");
		
	/// <summary>
	/// Gets the outgoing header for the ActivateQuest (Unity/Flash) message.
	/// </summary>
	public Header ActivateQuest => Get("ActivateQuest");
		
	/// <summary>
	/// Gets the outgoing header for the CancelQuest (Unity/Flash) message.
	/// </summary>
	public Header CancelQuest => Get("CancelQuest");
		
	/// <summary>
	/// Gets the outgoing header for the FriendRequestQuestComplete (Unity/Flash) message.
	/// </summary>
	public Header FriendRequestQuestComplete => Get("FriendRequestQuestComplete");
		
	/// <summary>
	/// Gets the outgoing header for the GetCommunityGoalHallOfFame (Unity/Flash) message.
	/// </summary>
	public Header GetCommunityGoalHallOfFame => Get("GetCommunityGoalHallOfFame");
		
	/// <summary>
	/// Gets the outgoing header for the GetCommunityGoalProgress (Unity/Flash) message.
	/// </summary>
	public Header GetCommunityGoalProgress => Get("GetCommunityGoalProgress");
		
	/// <summary>
	/// Gets the outgoing header for the GetConcurrentUsersGoalProgress (Unity/Flash) message.
	/// </summary>
	public Header GetConcurrentUsersGoalProgress => Get("GetConcurrentUsersGoalProgress");
		
	/// <summary>
	/// Gets the outgoing header for the GetConcurrentUsersReward (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RequestConcurrentUsersGoalReward" />.
	/// </summary>
	public Header GetConcurrentUsersReward => Get("GetConcurrentUsersReward");
		
	/// <summary>
	/// Gets the outgoing header for the GetDailyQuest (Unity/Flash) message.
	/// </summary>
	public Header GetDailyQuest => Get("GetDailyQuest");
		
	/// <summary>
	/// Gets the outgoing header for the GetQuests (Unity/Flash) message.
	/// </summary>
	public Header GetQuests => Get("GetQuests");
		
	/// <summary>
	/// Gets the outgoing header for the GetSeasonalQuestsOnly (Flash) message.
	/// </summary>
	public Header GetSeasonalQuestsOnly => Get("GetSeasonalQuestsOnly");
		
	/// <summary>
	/// Gets the outgoing header for the OpenQuestTracker (Unity/Flash) message.
	/// </summary>
	public Header OpenQuestTracker => Get("OpenQuestTracker");
		
	/// <summary>
	/// Gets the outgoing header for the RejectQuest (Unity/Flash) message.
	/// </summary>
	public Header RejectQuest => Get("RejectQuest");
		
	/// <summary>
	/// Gets the outgoing header for the StartCampaign (Unity/Flash) message.
	/// </summary>
	public Header StartCampaign => Get("StartCampaign");
		
	/// <summary>
	/// Gets the outgoing header for the ForwardToACompetitionRoom (Unity/Flash) message.
	/// </summary>
	public Header ForwardToACompetitionRoom => Get("ForwardToACompetitionRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ForwardToASubmittableRoom (Unity/Flash) message.
	/// </summary>
	public Header ForwardToASubmittableRoom => Get("ForwardToASubmittableRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ForwardToRandomCompetitionRoom (Unity/Flash) message.
	/// </summary>
	public Header ForwardToRandomCompetitionRoom => Get("ForwardToRandomCompetitionRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GetCurrentTimingCode (Unity/Flash) message.
	/// </summary>
	public Header GetCurrentTimingCode => Get("GetCurrentTimingCode");
		
	/// <summary>
	/// Gets the outgoing header for the GetIsUserPartOfCompetition (Flash) message.
	/// </summary>
	public Header GetIsUserPartOfCompetition => Get("GetIsUserPartOfCompetition");
		
	/// <summary>
	/// Gets the outgoing header for the GetSecondsUntil (Unity/Flash) message.
	/// </summary>
	public Header GetSecondsUntil => Get("GetSecondsUntil");
		
	/// <summary>
	/// Gets the outgoing header for the RoomCompetitionInit (Unity/Flash) message.
	/// </summary>
	public Header RoomCompetitionInit => Get("RoomCompetitionInit");
		
	/// <summary>
	/// Gets the outgoing header for the SubmitRoomToCompetition (Unity/Flash) message.
	/// </summary>
	public Header SubmitRoomToCompetition => Get("SubmitRoomToCompetition");
		
	/// <summary>
	/// Gets the outgoing header for the VoteForRoom (Unity/Flash) message.
	/// </summary>
	public Header VoteForRoom => Get("VoteForRoom");
		
	/// <summary>
	/// Gets the outgoing header for the CancelPetBreeding (Unity/Flash) message.
	/// </summary>
	public Header CancelPetBreeding => Get("CancelPetBreeding");
		
	/// <summary>
	/// Gets the outgoing header for the ConfirmPetBreeding (Unity/Flash) message.
	/// </summary>
	public Header ConfirmPetBreeding => Get("ConfirmPetBreeding");
		
	/// <summary>
	/// Gets the outgoing header for the GetPetInventory (Unity/Flash) message.
	/// </summary>
	public Header GetPetInventory => Get("GetPetInventory");
		
	/// <summary>
	/// Gets the outgoing header for the GetBadgePointLimits (Unity/Flash) message.
	/// </summary>
	public Header GetBadgePointLimits => Get("GetBadgePointLimits");
		
	/// <summary>
	/// Gets the outgoing header for the GetBadges (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetAvailableBadges" />.
	/// </summary>
	public Header GetBadges => Get("GetBadges");
		
	/// <summary>
	/// Gets the outgoing header for the GetIsBadgeRequestFulfilled (Unity/Flash) message.
	/// </summary>
	public Header GetIsBadgeRequestFulfilled => Get("GetIsBadgeRequestFulfilled");
		
	/// <summary>
	/// Gets the outgoing header for the RequestABadge (Flash) message.
	/// </summary>
	public Header RequestABadge => Get("RequestABadge");
		
	/// <summary>
	/// Gets the outgoing header for the SetActivatedBadges (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.SetSelectedBadges" />.
	/// </summary>
	public Header SetActivatedBadges => Get("SetActivatedBadges");
		
	/// <summary>
	/// Gets the outgoing header for the BuildersClubPlaceRoomItem (Unity/Flash) message.
	/// </summary>
	public Header BuildersClubPlaceRoomItem => Get("BuildersClubPlaceRoomItem");
		
	/// <summary>
	/// Gets the outgoing header for the BuildersClubPlaceWallItem (Unity/Flash) message.
	/// </summary>
	public Header BuildersClubPlaceWallItem => Get("BuildersClubPlaceWallItem");
		
	/// <summary>
	/// Gets the outgoing header for the BuildersClubQueryFurniCount (Unity/Flash) message.
	/// </summary>
	public Header BuildersClubQueryFurniCount => Get("BuildersClubQueryFurniCount");
		
	/// <summary>
	/// Gets the outgoing header for the GetBonusRareInfo (Unity/Flash) message.
	/// </summary>
	public Header GetBonusRareInfo => Get("GetBonusRareInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetBundleDiscountRuleset (Unity/Flash) message.
	/// </summary>
	public Header GetBundleDiscountRuleset => Get("GetBundleDiscountRuleset");
		
	/// <summary>
	/// Gets the outgoing header for the GetCatalogIndex (Unity/Flash) message.
	/// </summary>
	public Header GetCatalogIndex => Get("GetCatalogIndex");
		
	/// <summary>
	/// Gets the outgoing header for the GetCatalogPage (Unity/Flash) message.
	/// </summary>
	public Header GetCatalogPage => Get("GetCatalogPage");
		
	/// <summary>
	/// Gets the outgoing header for the GetCatalogPageWithEarliestExpiry (Unity/Flash) message.
	/// </summary>
	public Header GetCatalogPageWithEarliestExpiry => Get("GetCatalogPageWithEarliestExpiry");
		
	/// <summary>
	/// Gets the outgoing header for the GetClubGift (Flash) message.
	/// </summary>
	public Header GetClubGift => Get("GetClubGift");
		
	/// <summary>
	/// Gets the outgoing header for the GetClubOffers (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.GetHabboClubOffers" />.
	/// </summary>
	public Header GetClubOffers => Get("GetClubOffers");
		
	/// <summary>
	/// Gets the outgoing header for the GetGiftWrappingConfiguration (Unity/Flash) message.
	/// </summary>
	public Header GetGiftWrappingConfiguration => Get("GetGiftWrappingConfiguration");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboClubExtendOffer (Flash) message.
	/// </summary>
	public Header GetHabboClubExtendOffer => Get("GetHabboClubExtendOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetIsOfferGiftable (Unity/Flash) message.
	/// </summary>
	public Header GetIsOfferGiftable => Get("GetIsOfferGiftable");
		
	/// <summary>
	/// Gets the outgoing header for the GetLimitedOfferAppearingNext (Flash) message.
	/// </summary>
	public Header GetLimitedOfferAppearingNext => Get("GetLimitedOfferAppearingNext");
		
	/// <summary>
	/// Gets the outgoing header for the GetNextTargetedOffer (Unity/Flash) message.
	/// </summary>
	public Header GetNextTargetedOffer => Get("GetNextTargetedOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetProductOffer (Unity/Flash) message.
	/// </summary>
	public Header GetProductOffer => Get("GetProductOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomAdPurchaseInfo (Flash) message.
	/// </summary>
	public Header GetRoomAdPurchaseInfo => Get("GetRoomAdPurchaseInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetSeasonalCalendarDaily (Flash) message.
	/// </summary>
	public Header GetSeasonalCalendarDaily => Get("GetSeasonalCalendarDaily");
		
	/// <summary>
	/// Gets the outgoing header for the GetSellablePetPalettes (Unity/Flash) message.
	/// </summary>
	public Header GetSellablePetPalettes => Get("GetSellablePetPalettes");
		
	/// <summary>
	/// Gets the outgoing header for the GetSnowWarGameTokensOffer (Unity/Flash) message.
	/// </summary>
	public Header GetSnowWarGameTokensOffer => Get("GetSnowWarGameTokensOffer");
		
	/// <summary>
	/// Gets the outgoing header for the MarkCatalogNewAdditionsPageOpened (Unity/Flash) message.
	/// </summary>
	public Header MarkCatalogNewAdditionsPageOpened => Get("MarkCatalogNewAdditionsPageOpened");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseBasicMembershipExtension (Flash) message.
	/// </summary>
	public Header PurchaseBasicMembershipExtension => Get("PurchaseBasicMembershipExtension");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseFromCatalogAsGift (Unity/Flash) message.
	/// </summary>
	public Header PurchaseFromCatalogAsGift => Get("PurchaseFromCatalogAsGift");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseFromCatalog (Unity/Flash) message.
	/// </summary>
	public Header PurchaseFromCatalog => Get("PurchaseFromCatalog");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseRoomAd (Unity/Flash) message.
	/// </summary>
	public Header PurchaseRoomAd => Get("PurchaseRoomAd");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseSnowWarGameTokensOffer (Flash) message.
	/// </summary>
	public Header PurchaseSnowWarGameTokensOffer => Get("PurchaseSnowWarGameTokensOffer");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseTargetedOffer (Unity/Flash) message.
	/// </summary>
	public Header PurchaseTargetedOffer => Get("PurchaseTargetedOffer");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseVipMembershipExtension (Flash) message.
	/// </summary>
	public Header PurchaseVipMembershipExtension => Get("PurchaseVipMembershipExtension");
		
	/// <summary>
	/// Gets the outgoing header for the RedeemVoucher (Flash) message.
	/// </summary>
	public Header RedeemVoucher => Get("RedeemVoucher");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdPurchaseInitiated (Unity/Flash) message.
	/// </summary>
	public Header RoomAdPurchaseInitiated => Get("RoomAdPurchaseInitiated");
		
	/// <summary>
	/// Gets the outgoing header for the SelectClubGift (Unity/Flash) message.
	/// </summary>
	public Header SelectClubGift => Get("SelectClubGift");
		
	/// <summary>
	/// Gets the outgoing header for the SetTargetedOfferState (Unity/Flash) message.
	/// </summary>
	public Header SetTargetedOfferState => Get("SetTargetedOfferState");
		
	/// <summary>
	/// Gets the outgoing header for the ShopTargetedOfferViewed (Unity/Flash) message.
	/// </summary>
	public Header ShopTargetedOfferViewed => Get("ShopTargetedOfferViewed");
		
	/// <summary>
	/// Gets the outgoing header for the PhotoCompetition (Flash) message.
	/// </summary>
	public Header PhotoCompetition => Get("PhotoCompetition");
		
	/// <summary>
	/// Gets the outgoing header for the PublishPhoto (Unity/Flash) message.
	/// </summary>
	public Header PublishPhoto => Get("PublishPhoto");
		
	/// <summary>
	/// Gets the outgoing header for the PurchasePhoto (Unity/Flash) message.
	/// </summary>
	public Header PurchasePhoto => Get("PurchasePhoto");
		
	/// <summary>
	/// Gets the outgoing header for the RenderRoom (Unity/Flash) message.
	/// </summary>
	public Header RenderRoom => Get("RenderRoom");
		
	/// <summary>
	/// Gets the outgoing header for the RenderRoomThumbnail (Flash) message.
	/// The Unity equivalent for this message is <see cref="Outgoing.RenderAndSaveRoomThumbnailPhoto" />.
	/// </summary>
	public Header RenderRoomThumbnail => Get("RenderRoomThumbnail");
		
	/// <summary>
	/// Gets the outgoing header for the RequestCameraConfiguration (Flash) message.
	/// </summary>
	public Header RequestCameraConfiguration => Get("RequestCameraConfiguration");
		
	/// <summary>
	/// Gets the outgoing header for the GetPromoArticles (Unity/Flash) message.
	/// </summary>
	public Header GetPromoArticles => Get("GetPromoArticles");
		
	/// <summary>
	/// Gets the outgoing header for the GetCredits (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetCreditsInfo" />.
	/// </summary>
	public Header GetCredits => Get("GetCredits");
		
	/// <summary>
	/// Gets the outgoing header for the AddYourFavouriteRoom (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AddFavouriteRoom" />.
	/// </summary>
	public Header AddYourFavouriteRoom => Get("AddYourFavouriteRoom");
		
	/// <summary>
	/// Gets the outgoing header for the DeleteYourFavouriteRoom (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.DeleteFavouriteRoom" />.
	/// </summary>
	public Header DeleteYourFavouriteRoom => Get("DeleteYourFavouriteRoom");
		
	/// <summary>
	/// Gets the outgoing header for the DeleteFlat (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.DeleteRoom" />.
	/// </summary>
	public Header DeleteFlat => Get("DeleteFlat");
		
	/// <summary>
	/// Gets the outgoing header for the SubscriptionGetUserInfo (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ScrGetUserInfo" />.
	/// </summary>
	public Header SubscriptionGetUserInfo => Get("SubscriptionGetUserInfo");
		
	/// <summary>
	/// Gets the outgoing header for the SubscriptionGetKickbackInfo (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ScrGetKickbackInfo" />.
	/// </summary>
	public Header SubscriptionGetKickbackInfo => Get("SubscriptionGetKickbackInfo");
		
	/// <summary>
	/// Gets the outgoing header for the CreateNewFlat (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CreateFlat" />.
	/// </summary>
	public Header CreateNewFlat => Get("CreateNewFlat");
		
	/// <summary>
	/// Gets the outgoing header for the SendMessage (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SendMsg" />.
	/// </summary>
	public Header SendMessage => Get("SendMessage");
		
	/// <summary>
	/// Gets the outgoing header for the ApprovePetName (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header ApprovePetName => Get("ApprovePetName");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateAvatar (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UpdateFigureData" />.
	/// </summary>
	public Header UpdateAvatar => Get("UpdateAvatar");
		
	/// <summary>
	/// Gets the outgoing header for the PickUpPetFromRoom (Unity) message.
	/// </summary>
	public Header PickUpPetFromRoom => Get("PickUpPetFromRoom");
		
	/// <summary>
	/// Gets the outgoing header for the GoToFlat (Unity) message.
	/// </summary>
	public Header GoToFlat => Get("GoToFlat");
		
	/// <summary>
	/// Gets the outgoing header for the PickUpAllItemsFromRoom (Unity) message.
	/// </summary>
	public Header PickUpAllItemsFromRoom => Get("PickUpAllItemsFromRoom");
		
	/// <summary>
	/// Gets the outgoing header for the FlatPropertyByItem (Unity) message.
	/// </summary>
	public Header FlatPropertyByItem => Get("FlatPropertyByItem");
		
	/// <summary>
	/// Gets the outgoing header for the PickItemUpFromRoom (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PickupObject" />.
	/// </summary>
	public Header PickItemUpFromRoom => Get("PickItemUpFromRoom");
		
	/// <summary>
	/// Gets the outgoing header for the TradeUnaccept (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UnacceptTrading" />.
	/// </summary>
	public Header TradeUnaccept => Get("TradeUnaccept");
		
	/// <summary>
	/// Gets the outgoing header for the TradeAccept (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AcceptTrading" />.
	/// </summary>
	public Header TradeAccept => Get("TradeAccept");
		
	/// <summary>
	/// Gets the outgoing header for the TradeClose (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CloseTrading" />.
	/// </summary>
	public Header TradeClose => Get("TradeClose");
		
	/// <summary>
	/// Gets the outgoing header for the TradeOpen (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.OpenTrading" />.
	/// </summary>
	public Header TradeOpen => Get("TradeOpen");
		
	/// <summary>
	/// Gets the outgoing header for the TradeAddItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AddItemToTrade" />.
	/// </summary>
	public Header TradeAddItem => Get("TradeAddItem");
		
	/// <summary>
	/// Gets the outgoing header for the MoveRoomItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.MoveObject" />.
	/// </summary>
	public Header MoveRoomItem => Get("MoveRoomItem");
		
	/// <summary>
	/// Gets the outgoing header for the SetStuffData (Unity) message.
	/// </summary>
	public Header SetStuffData => Get("SetStuffData");
		
	/// <summary>
	/// Gets the outgoing header for the Move (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.MoveAvatar" />.
	/// </summary>
	public Header Move => Get("Move");
		
	/// <summary>
	/// Gets the outgoing header for the PassHandItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PassCarryItem" />.
	/// </summary>
	public Header PassHandItem => Get("PassHandItem");
		
	/// <summary>
	/// Gets the outgoing header for the DropHandItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.DropCarryItem" />.
	/// </summary>
	public Header DropHandItem => Get("DropHandItem");
		
	/// <summary>
	/// Gets the outgoing header for the PassHandItemToPet (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PassCarryItemToPet" />.
	/// </summary>
	public Header PassHandItemToPet => Get("PassHandItemToPet");
		
	/// <summary>
	/// Gets the outgoing header for the SetStickyData (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SetItemData" />.
	/// </summary>
	public Header SetStickyData => Get("SetStickyData");
		
	/// <summary>
	/// Gets the outgoing header for the Posture (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ChangePosture" />.
	/// </summary>
	public Header Posture => Get("Posture");
		
	/// <summary>
	/// Gets the outgoing header for the TradeAddItems (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AddItemsToTrade" />.
	/// </summary>
	public Header TradeAddItems => Get("TradeAddItems");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceStuffFromStripDEPRECATED (Unity) message.
	/// </summary>
	public Header PlaceStuffFromStripDEPRECATED => Get("PlaceStuffFromStripDEPRECATED");
		
	/// <summary>
	/// Gets the outgoing header for the MoveItemDEPRECATED (Unity) message.
	/// </summary>
	public Header MoveItemDEPRECATED => Get("MoveItemDEPRECATED");
		
	/// <summary>
	/// Gets the outgoing header for the Expression (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AvatarExpression" />.
	/// </summary>
	public Header Expression => Get("Expression");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveOwnRights (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RemoveOwnRoomRightsRoom" />.
	/// </summary>
	public Header RemoveOwnRights => Get("RemoveOwnRights");
		
	/// <summary>
	/// Gets the outgoing header for the ShowSign (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.Sign" />.
	/// </summary>
	public Header ShowSign => Get("ShowSign");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceRoomItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PlaceObject" />.
	/// </summary>
	public Header PlaceRoomItem => Get("PlaceRoomItem");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceWallItem (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header PlaceWallItem => Get("PlaceWallItem");
		
	/// <summary>
	/// Gets the outgoing header for the RelinkTeleports (Unity) message.
	/// </summary>
	public Header RelinkTeleports => Get("RelinkTeleports");
		
	/// <summary>
	/// Gets the outgoing header for the Goaway (Unity) message.
	/// </summary>
	public Header Goaway => Get("Goaway");
		
	/// <summary>
	/// Gets the outgoing header for the GetDonationSettings (Unity) message.
	/// </summary>
	public Header GetDonationSettings => Get("GetDonationSettings");
		
	/// <summary>
	/// Gets the outgoing header for the Donate (Unity) message.
	/// </summary>
	public Header Donate => Get("Donate");
		
	/// <summary>
	/// Gets the outgoing header for the ExtendRentOrBuyoutFurniInRoom (Unity) message.
	/// </summary>
	public Header ExtendRentOrBuyoutFurniInRoom => Get("ExtendRentOrBuyoutFurniInRoom");
		
	/// <summary>
	/// Gets the outgoing header for the ExtendRentOrBuyoutFurniInInventory (Unity) message.
	/// </summary>
	public Header ExtendRentOrBuyoutFurniInInventory => Get("ExtendRentOrBuyoutFurniInInventory");
		
	/// <summary>
	/// Gets the outgoing header for the RedeemVoucherCode (Unity) message.
	/// </summary>
	public Header RedeemVoucherCode => Get("RedeemVoucherCode");
		
	/// <summary>
	/// Gets the outgoing header for the RedeemVoucherCodeWithHc (Unity) message.
	/// </summary>
	public Header RedeemVoucherCodeWithHc => Get("RedeemVoucherCodeWithHc");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserFlatCategories (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetUserFlatCats" />.
	/// </summary>
	public Header GetUserFlatCategories => Get("GetUserFlatCategories");
		
	/// <summary>
	/// Gets the outgoing header for the GetEventFlatCats (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetUserEventCats" />.
	/// </summary>
	public Header GetEventFlatCats => Get("GetEventFlatCats");
		
	/// <summary>
	/// Gets the outgoing header for the GetAvailableBadges (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetBadges" />.
	/// </summary>
	public Header GetAvailableBadges => Get("GetAvailableBadges");
		
	/// <summary>
	/// Gets the outgoing header for the SetSelectedBadges (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SetActivatedBadges" />.
	/// </summary>
	public Header SetSelectedBadges => Get("SetSelectedBadges");
		
	/// <summary>
	/// Gets the outgoing header for the ConvertFurniToCredits (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CreditFurniRedeem" />.
	/// </summary>
	public Header ConvertFurniToCredits => Get("ConvertFurniToCredits");
		
	/// <summary>
	/// Gets the outgoing header for the ClientSuspended (Unity) message.
	/// </summary>
	public Header ClientSuspended => Get("ClientSuspended");
		
	/// <summary>
	/// Gets the outgoing header for the ClientResumed (Unity) message.
	/// </summary>
	public Header ClientResumed => Get("ClientResumed");
		
	/// <summary>
	/// Gets the outgoing header for the ModerationAction (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ModeratorAction" />.
	/// </summary>
	public Header ModerationAction => Get("ModerationAction");
		
	/// <summary>
	/// Gets the outgoing header for the InitDhHandshake (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.InitDiffieHandshake" />.
	/// </summary>
	public Header InitDhHandshake => Get("InitDhHandshake");
		
	/// <summary>
	/// Gets the outgoing header for the CompleteDhHandshake (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CompleteDiffieHandshake" />.
	/// </summary>
	public Header CompleteDhHandshake => Get("CompleteDhHandshake");
		
	/// <summary>
	/// Gets the outgoing header for the RoomQueueChange (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ChangeQueue" />.
	/// </summary>
	public Header RoomQueueChange => Get("RoomQueueChange");
		
	/// <summary>
	/// Gets the outgoing header for the GetOpeningHours (Unity) message.
	/// </summary>
	public Header GetOpeningHours => Get("GetOpeningHours");
		
	/// <summary>
	/// Gets the outgoing header for the SetWallItemAnimationState (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header SetWallItemAnimationState => Get("SetWallItemAnimationState");
		
	/// <summary>
	/// Gets the outgoing header for the GetFurniAliases (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetFurnitureAliases" />.
	/// </summary>
	public Header GetFurniAliases => Get("GetFurniAliases");
		
	/// <summary>
	/// Gets the outgoing header for the GetSpectatorAmount (Unity) message.
	/// </summary>
	public Header GetSpectatorAmount => Get("GetSpectatorAmount");
		
	/// <summary>
	/// Gets the outgoing header for the GetSongId (Unity) message.
	/// </summary>
	public Header GetSongId => Get("GetSongId");
		
	/// <summary>
	/// Gets the outgoing header for the GetAccountPreferences (Unity) message.
	/// This message is not used in the Flash client.
	/// </summary>
	public Header GetAccountPreferences => Get("GetAccountPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the StartPoll (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PollStart" />.
	/// </summary>
	public Header StartPoll => Get("StartPoll");
		
	/// <summary>
	/// Gets the outgoing header for the RejectPoll (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PollReject" />.
	/// </summary>
	public Header RejectPoll => Get("RejectPoll");
		
	/// <summary>
	/// Gets the outgoing header for the SetRoomInvitePreferences (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SetIgnoreRoomInvites" />.
	/// </summary>
	public Header SetRoomInvitePreferences => Get("SetRoomInvitePreferences");
		
	/// <summary>
	/// Gets the outgoing header for the SetNewNavigatorPreferences (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SetNewNavigatorWindowPreferences" />.
	/// </summary>
	public Header SetNewNavigatorPreferences => Get("SetNewNavigatorPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the AddJukeboxDisc (Unity) message.
	/// </summary>
	public Header AddJukeboxDisc => Get("AddJukeboxDisc");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveJukeboxDisc (Unity) message.
	/// </summary>
	public Header RemoveJukeboxDisc => Get("RemoveJukeboxDisc");
		
	/// <summary>
	/// Gets the outgoing header for the GetJukeboxDiscs (Unity) message.
	/// </summary>
	public Header GetJukeboxDiscs => Get("GetJukeboxDiscs");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserSongDiscs (Unity) message.
	/// </summary>
	public Header GetUserSongDiscs => Get("GetUserSongDiscs");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveAllJukeboxDiscs (Unity) message.
	/// </summary>
	public Header RemoveAllJukeboxDiscs => Get("RemoveAllJukeboxDiscs");
		
	/// <summary>
	/// Gets the outgoing header for the CancelFriendRequest (Unity) message.
	/// </summary>
	public Header CancelFriendRequest => Get("CancelFriendRequest");
		
	/// <summary>
	/// Gets the outgoing header for the SetFurniRandomState (Unity) message.
	/// </summary>
	public Header SetFurniRandomState => Get("SetFurniRandomState");
		
	/// <summary>
	/// Gets the outgoing header for the ClientLatencyPingRequest (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.LatencyPingRequest" />.
	/// </summary>
	public Header ClientLatencyPingRequest => Get("ClientLatencyPingRequest");
		
	/// <summary>
	/// Gets the outgoing header for the ClientLatencyPingReport (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.LatencyPingReport" />.
	/// </summary>
	public Header ClientLatencyPingReport => Get("ClientLatencyPingReport");
		
	/// <summary>
	/// Gets the outgoing header for the UserStartTyping (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.StartTyping" />.
	/// </summary>
	public Header UserStartTyping => Get("UserStartTyping");
		
	/// <summary>
	/// Gets the outgoing header for the UserCancelTyping (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CancelTyping" />.
	/// </summary>
	public Header UserCancelTyping => Get("UserCancelTyping");
		
	/// <summary>
	/// Gets the outgoing header for the IgnoreAvatarId (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.IgnoreUserId" />.
	/// </summary>
	public Header IgnoreAvatarId => Get("IgnoreAvatarId");
		
	/// <summary>
	/// Gets the outgoing header for the GetIgnoreList (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetIgnoredUsers" />.
	/// </summary>
	public Header GetIgnoreList => Get("GetIgnoreList");
		
	/// <summary>
	/// Gets the outgoing header for the RoomBanWithDuration (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.BanUserWithDuration" />.
	/// </summary>
	public Header RoomBanWithDuration => Get("RoomBanWithDuration");
		
	/// <summary>
	/// Gets the outgoing header for the RoomMuteUser (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.MuteUser" />.
	/// </summary>
	public Header RoomMuteUser => Get("RoomMuteUser");
		
	/// <summary>
	/// Gets the outgoing header for the RoomMuteUnmuteAll (Unity) message.
	/// </summary>
	public Header RoomMuteUnmuteAll => Get("RoomMuteUnmuteAll");
		
	/// <summary>
	/// Gets the outgoing header for the RoomUnmuteUser (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UnmuteUser" />.
	/// </summary>
	public Header RoomUnmuteUser => Get("RoomUnmuteUser");
		
	/// <summary>
	/// Gets the outgoing header for the RoomDimmerEditPresets (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RoomDimmerGetPresets" />.
	/// </summary>
	public Header RoomDimmerEditPresets => Get("RoomDimmerEditPresets");
		
	/// <summary>
	/// Gets the outgoing header for the RoomMuteAll (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.MuteAllInRoom" />.
	/// </summary>
	public Header RoomMuteAll => Get("RoomMuteAll");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdCancelAd (Unity) message.
	/// </summary>
	public Header RoomAdCancelAd => Get("RoomAdCancelAd");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdEditAd (Unity) message.
	/// </summary>
	public Header RoomAdEditAd => Get("RoomAdEditAd");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdGetRooms (Unity) message.
	/// </summary>
	public Header RoomAdGetRooms => Get("RoomAdGetRooms");
		
	/// <summary>
	/// Gets the outgoing header for the RoomAdListAds (Unity) message.
	/// </summary>
	public Header RoomAdListAds => Get("RoomAdListAds");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserAchievements (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetAchievements" />.
	/// </summary>
	public Header GetUserAchievements => Get("GetUserAchievements");
		
	/// <summary>
	/// Gets the outgoing header for the RespectUser (Unity) message.
	/// </summary>
	public Header RespectUser => Get("RespectUser");
		
	/// <summary>
	/// Gets the outgoing header for the UseAvatarEffect (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AvatarEffectSelected" />.
	/// </summary>
	public Header UseAvatarEffect => Get("UseAvatarEffect");
		
	/// <summary>
	/// Gets the outgoing header for the ActivateAvatarEffect (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.AvatarEffectActivated" />.
	/// </summary>
	public Header ActivateAvatarEffect => Get("ActivateAvatarEffect");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseAndActivateAvatarEffect (Unity) message.
	/// </summary>
	public Header PurchaseAndActivateAvatarEffect => Get("PurchaseAndActivateAvatarEffect");
		
	/// <summary>
	/// Gets the outgoing header for the ResetResolutionAchievement (Unity) message.
	/// </summary>
	public Header ResetResolutionAchievement => Get("ResetResolutionAchievement");
		
	/// <summary>
	/// Gets the outgoing header for the GetUserAchievementsForAResolution (Unity) message.
	/// </summary>
	public Header GetUserAchievementsForAResolution => Get("GetUserAchievementsForAResolution");
		
	/// <summary>
	/// Gets the outgoing header for the FriendFurnitureLockConfirm (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.FriendFurniConfirmLock" />.
	/// </summary>
	public Header FriendFurnitureLockConfirm => Get("FriendFurnitureLockConfirm");
		
	/// <summary>
	/// Gets the outgoing header for the GetCategoriesWithVisitorCount (Unity) message.
	/// </summary>
	public Header GetCategoriesWithVisitorCount => Get("GetCategoriesWithVisitorCount");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateNavigatorSettings (Unity) message.
	/// </summary>
	public Header UpdateNavigatorSettings => Get("UpdateNavigatorSettings");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateRoomThumbnail (Unity) message.
	/// </summary>
	public Header UpdateRoomThumbnail => Get("UpdateRoomThumbnail");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomEntryData (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetHeightMap" />.
	/// </summary>
	public Header GetRoomEntryData => Get("GetRoomEntryData");
		
	/// <summary>
	/// Gets the outgoing header for the FlatOpc (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.OpenFlatConnection" />.
	/// </summary>
	public Header FlatOpc => Get("FlatOpc");
		
	/// <summary>
	/// Gets the outgoing header for the UseStuff (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UseFurniture" />.
	/// </summary>
	public Header UseStuff => Get("UseStuff");
		
	/// <summary>
	/// Gets the outgoing header for the TradeConfirmAccept (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ConfirmAcceptTrading" />.
	/// </summary>
	public Header TradeConfirmAccept => Get("TradeConfirmAccept");
		
	/// <summary>
	/// Gets the outgoing header for the TradeConfirmDecline (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ConfirmDeclineTrading" />.
	/// </summary>
	public Header TradeConfirmDecline => Get("TradeConfirmDecline");
		
	/// <summary>
	/// Gets the outgoing header for the GetInventory (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RequestFurniInventory" />.
	/// </summary>
	public Header GetInventory => Get("GetInventory");
		
	/// <summary>
	/// Gets the outgoing header for the TradeRemoveItem (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RemoveItemFromTrade" />.
	/// </summary>
	public Header TradeRemoveItem => Get("TradeRemoveItem");
		
	/// <summary>
	/// Gets the outgoing header for the GetInventoryPeer (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RequestFurniInventoryWhenNotInRoom" />.
	/// </summary>
	public Header GetInventoryPeer => Get("GetInventoryPeer");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomFilter (Unity) message.
	/// </summary>
	public Header GetRoomFilter => Get("GetRoomFilter");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateRoomCategoryAndTrade (Unity) message.
	/// </summary>
	public Header UpdateRoomCategoryAndTrade => Get("UpdateRoomCategoryAndTrade");
		
	/// <summary>
	/// Gets the outgoing header for the StageStartPerformance (Unity) message.
	/// </summary>
	public Header StageStartPerformance => Get("StageStartPerformance");
		
	/// <summary>
	/// Gets the outgoing header for the StageVotePerformance (Unity) message.
	/// </summary>
	public Header StageVotePerformance => Get("StageVotePerformance");
		
	/// <summary>
	/// Gets the outgoing header for the LoginWithTicket (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SSOTicket" />.
	/// </summary>
	public Header LoginWithTicket => Get("LoginWithTicket");
		
	/// <summary>
	/// Gets the outgoing header for the GetClientFaqs (Unity) message.
	/// </summary>
	public Header GetClientFaqs => Get("GetClientFaqs");
		
	/// <summary>
	/// Gets the outgoing header for the GetFaqCategories (Unity) message.
	/// </summary>
	public Header GetFaqCategories => Get("GetFaqCategories");
		
	/// <summary>
	/// Gets the outgoing header for the GetFaqText (Unity) message.
	/// </summary>
	public Header GetFaqText => Get("GetFaqText");
		
	/// <summary>
	/// Gets the outgoing header for the SearchFaqs (Unity) message.
	/// </summary>
	public Header SearchFaqs => Get("SearchFaqs");
		
	/// <summary>
	/// Gets the outgoing header for the GetFaqCategory (Unity) message.
	/// </summary>
	public Header GetFaqCategory => Get("GetFaqCategory");
		
	/// <summary>
	/// Gets the outgoing header for the LogFlashPerformance (Unity) message.
	/// </summary>
	public Header LogFlashPerformance => Get("LogFlashPerformance");
		
	/// <summary>
	/// Gets the outgoing header for the LogLagWarning (Unity) message.
	/// </summary>
	public Header LogLagWarning => Get("LogLagWarning");
		
	/// <summary>
	/// Gets the outgoing header for the LogAirPerformance (Unity) message.
	/// </summary>
	public Header LogAirPerformance => Get("LogAirPerformance");
		
	/// <summary>
	/// Gets the outgoing header for the RoomTagSearch (Unity) message.
	/// </summary>
	public Header RoomTagSearch => Get("RoomTagSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyFrequentlyVisitedRoomsSearch (Unity) message.
	/// </summary>
	public Header MyFrequentlyVisitedRoomsSearch => Get("MyFrequentlyVisitedRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the MyRecommendedRoomsSearch (Unity) message.
	/// </summary>
	public Header MyRecommendedRoomsSearch => Get("MyRecommendedRoomsSearch");
		
	/// <summary>
	/// Gets the outgoing header for the CreateIssue (Unity) message.
	/// </summary>
	public Header CreateIssue => Get("CreateIssue");
		
	/// <summary>
	/// Gets the outgoing header for the ModToolNextSanction (Unity) message.
	/// </summary>
	public Header ModToolNextSanction => Get("ModToolNextSanction");
		
	/// <summary>
	/// Gets the outgoing header for the CreateImIssue (Unity) message.
	/// </summary>
	public Header CreateImIssue => Get("CreateImIssue");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeAvatarNameInRoom (Unity) message.
	/// </summary>
	public Header ChangeAvatarNameInRoom => Get("ChangeAvatarNameInRoom");
		
	/// <summary>
	/// Gets the outgoing header for the CheckAvatarName (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CheckUserName" />.
	/// </summary>
	public Header CheckAvatarName => Get("CheckAvatarName");
		
	/// <summary>
	/// Gets the outgoing header for the GetSelectableClubGiftInfo (Unity) message.
	/// </summary>
	public Header GetSelectableClubGiftInfo => Get("GetSelectableClubGiftInfo");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeAvatarName (Unity) message.
	/// </summary>
	public Header ChangeAvatarName => Get("ChangeAvatarName");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomTypes (Unity) message.
	/// </summary>
	public Header GetRoomTypes => Get("GetRoomTypes");
		
	/// <summary>
	/// Gets the outgoing header for the SetClothingChangeFurnitureData (Unity) message.
	/// </summary>
	public Header SetClothingChangeFurnitureData => Get("SetClothingChangeFurnitureData");
		
	/// <summary>
	/// Gets the outgoing header for the LogToEventLog (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.EventLog" />.
	/// </summary>
	public Header LogToEventLog => Get("LogToEventLog");
		
	/// <summary>
	/// Gets the outgoing header for the ToggleRoomStaffPick (Unity) message.
	/// </summary>
	public Header ToggleRoomStaffPick => Get("ToggleRoomStaffPick");
		
	/// <summary>
	/// Gets the outgoing header for the ChangeAvatarMotto (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ChangeMotto" />.
	/// </summary>
	public Header ChangeAvatarMotto => Get("ChangeAvatarMotto");
		
	/// <summary>
	/// Gets the outgoing header for the FriendBarHelperFindFriends (Unity) message.
	/// </summary>
	public Header FriendBarHelperFindFriends => Get("FriendBarHelperFindFriends");
		
	/// <summary>
	/// Gets the outgoing header for the GetEventStream (Unity) message.
	/// </summary>
	public Header GetEventStream => Get("GetEventStream");
		
	/// <summary>
	/// Gets the outgoing header for the SetEventStreamPublishingAllowed (Unity) message.
	/// </summary>
	public Header SetEventStreamPublishingAllowed => Get("SetEventStreamPublishingAllowed");
		
	/// <summary>
	/// Gets the outgoing header for the StreamLike (Unity) message.
	/// </summary>
	public Header StreamLike => Get("StreamLike");
		
	/// <summary>
	/// Gets the outgoing header for the StreamStatus (Unity) message.
	/// </summary>
	public Header StreamStatus => Get("StreamStatus");
		
	/// <summary>
	/// Gets the outgoing header for the GetEventStreamForAccount (Unity) message.
	/// </summary>
	public Header GetEventStreamForAccount => Get("GetEventStreamForAccount");
		
	/// <summary>
	/// Gets the outgoing header for the StreamComment (Unity) message.
	/// </summary>
	public Header StreamComment => Get("StreamComment");
		
	/// <summary>
	/// Gets the outgoing header for the GetStreamNotificationCount (Unity) message.
	/// </summary>
	public Header GetStreamNotificationCount => Get("GetStreamNotificationCount");
		
	/// <summary>
	/// Gets the outgoing header for the GetStreamNotifications (Unity) message.
	/// </summary>
	public Header GetStreamNotifications => Get("GetStreamNotifications");
		
	/// <summary>
	/// Gets the outgoing header for the IsUserPartOfCompetition (Unity) message.
	/// </summary>
	public Header IsUserPartOfCompetition => Get("IsUserPartOfCompetition");
		
	/// <summary>
	/// Gets the outgoing header for the RequestConcurrentUsersGoalReward (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetConcurrentUsersReward" />.
	/// </summary>
	public Header RequestConcurrentUsersGoalReward => Get("RequestConcurrentUsersGoalReward");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionCreate (Unity) message.
	/// </summary>
	public Header HelpRequestSessionCreate => Get("HelpRequestSessionCreate");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionGuideDecides (Unity) message.
	/// </summary>
	public Header HelpRequestSessionGuideDecides => Get("HelpRequestSessionGuideDecides");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionRequesterCancels (Unity) message.
	/// </summary>
	public Header HelpRequestSessionRequesterCancels => Get("HelpRequestSessionRequesterCancels");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionResolved (Unity) message.
	/// </summary>
	public Header HelpRequestSessionResolved => Get("HelpRequestSessionResolved");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionFeedback (Unity) message.
	/// </summary>
	public Header HelpRequestSessionFeedback => Get("HelpRequestSessionFeedback");
		
	/// <summary>
	/// Gets the outgoing header for the GuideOnDutyUpdate (Unity) message.
	/// </summary>
	public Header GuideOnDutyUpdate => Get("GuideOnDutyUpdate");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionMessage (Unity) message.
	/// </summary>
	public Header HelpRequestSessionMessage => Get("HelpRequestSessionMessage");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionGetRequesterRoom (Unity) message.
	/// </summary>
	public Header HelpRequestSessionGetRequesterRoom => Get("HelpRequestSessionGetRequesterRoom");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionReported (Unity) message.
	/// </summary>
	public Header HelpRequestSessionReported => Get("HelpRequestSessionReported");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionInviteRequester (Unity) message.
	/// </summary>
	public Header HelpRequestSessionInviteRequester => Get("HelpRequestSessionInviteRequester");
		
	/// <summary>
	/// Gets the outgoing header for the HelpRequestSessionTyping (Unity) message.
	/// </summary>
	public Header HelpRequestSessionTyping => Get("HelpRequestSessionTyping");
		
	/// <summary>
	/// Gets the outgoing header for the QuizGetQuestions (Unity) message.
	/// </summary>
	public Header QuizGetQuestions => Get("QuizGetQuestions");
		
	/// <summary>
	/// Gets the outgoing header for the QuizPostAnswers (Unity) message.
	/// </summary>
	public Header QuizPostAnswers => Get("QuizPostAnswers");
		
	/// <summary>
	/// Gets the outgoing header for the ChangePetName (Unity) message.
	/// </summary>
	public Header ChangePetName => Get("ChangePetName");
		
	/// <summary>
	/// Gets the outgoing header for the GetPetConfigurations (Unity) message.
	/// </summary>
	public Header GetPetConfigurations => Get("GetPetConfigurations");
		
	/// <summary>
	/// Gets the outgoing header for the ChangePassword (Unity) message.
	/// </summary>
	public Header ChangePassword => Get("ChangePassword");
		
	/// <summary>
	/// Gets the outgoing header for the LoginWithPassword (Unity) message.
	/// </summary>
	public Header LoginWithPassword => Get("LoginWithPassword");
		
	/// <summary>
	/// Gets the outgoing header for the LoginWithPasswordDEPRECATED (Unity) message.
	/// </summary>
	public Header LoginWithPasswordDEPRECATED => Get("LoginWithPasswordDEPRECATED");
		
	/// <summary>
	/// Gets the outgoing header for the LoginWithFacebookToken (Unity) message.
	/// </summary>
	public Header LoginWithFacebookToken => Get("LoginWithFacebookToken");
		
	/// <summary>
	/// Gets the outgoing header for the LoginWithToken (Unity) message.
	/// </summary>
	public Header LoginWithToken => Get("LoginWithToken");
		
	/// <summary>
	/// Gets the outgoing header for the CreateAccount (Unity) message.
	/// </summary>
	public Header CreateAccount => Get("CreateAccount");
		
	/// <summary>
	/// Gets the outgoing header for the CaptchaRequest (Unity) message.
	/// </summary>
	public Header CaptchaRequest => Get("CaptchaRequest");
		
	/// <summary>
	/// Gets the outgoing header for the ClearDeviceLoginToken (Unity) message.
	/// </summary>
	public Header ClearDeviceLoginToken => Get("ClearDeviceLoginToken");
		
	/// <summary>
	/// Gets the outgoing header for the GetDeviceLoginTokens (Unity) message.
	/// </summary>
	public Header GetDeviceLoginTokens => Get("GetDeviceLoginTokens");
		
	/// <summary>
	/// Gets the outgoing header for the UniqueMachineId (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UniqueID" />.
	/// </summary>
	public Header UniqueMachineId => Get("UniqueMachineId");
		
	/// <summary>
	/// Gets the outgoing header for the ClientStatistics (Unity) message.
	/// </summary>
	public Header ClientStatistics => Get("ClientStatistics");
		
	/// <summary>
	/// Gets the outgoing header for the GetAccountProgressionInfo (Unity) message.
	/// </summary>
	public Header GetAccountProgressionInfo => Get("GetAccountProgressionInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetAvatarList (Unity) message.
	/// </summary>
	public Header GetAvatarList => Get("GetAvatarList");
		
	/// <summary>
	/// Gets the outgoing header for the CreateNewAvatar (Unity) message.
	/// </summary>
	public Header CreateNewAvatar => Get("CreateNewAvatar");
		
	/// <summary>
	/// Gets the outgoing header for the DeactivateAvatar (Unity) message.
	/// </summary>
	public Header DeactivateAvatar => Get("DeactivateAvatar");
		
	/// <summary>
	/// Gets the outgoing header for the GetInitialRooms (Unity) message.
	/// </summary>
	public Header GetInitialRooms => Get("GetInitialRooms");
		
	/// <summary>
	/// Gets the outgoing header for the RoomNetworkForward (Unity) message.
	/// </summary>
	public Header RoomNetworkForward => Get("RoomNetworkForward");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2Init (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NewNavigatorInit" />.
	/// </summary>
	public Header Navigator2Init => Get("Navigator2Init");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2Search (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NewNavigatorSearch" />.
	/// </summary>
	public Header Navigator2Search => Get("Navigator2Search");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2AddSavedSearch (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NavigatorAddSavedSearch" />.
	/// </summary>
	public Header Navigator2AddSavedSearch => Get("Navigator2AddSavedSearch");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2DeleteSavedSearch (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NavigatorDeleteSavedSearch" />.
	/// </summary>
	public Header Navigator2DeleteSavedSearch => Get("Navigator2DeleteSavedSearch");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2AddCollapsedCategory (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NavigatorAddCollapsedCategory" />.
	/// </summary>
	public Header Navigator2AddCollapsedCategory => Get("Navigator2AddCollapsedCategory");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2RemoveCollapsedCategory (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NavigatorRemoveCollapsedCategory" />.
	/// </summary>
	public Header Navigator2RemoveCollapsedCategory => Get("Navigator2RemoveCollapsedCategory");
		
	/// <summary>
	/// Gets the outgoing header for the Navigator2SetSearchCodeViewMode (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.NavigatorSetSearchCodeViewMode" />.
	/// </summary>
	public Header Navigator2SetSearchCodeViewMode => Get("Navigator2SetSearchCodeViewMode");
		
	/// <summary>
	/// Gets the outgoing header for the GetProductOffers (Unity) message.
	/// </summary>
	public Header GetProductOffers => Get("GetProductOffers");
		
	/// <summary>
	/// Gets the outgoing header for the StaffOpenCampaignCalendarDoor (Unity) message.
	/// </summary>
	public Header StaffOpenCampaignCalendarDoor => Get("StaffOpenCampaignCalendarDoor");
		
	/// <summary>
	/// Gets the outgoing header for the PickUpAllFurniAndResetHeightmap (Unity) message.
	/// </summary>
	public Header PickUpAllFurniAndResetHeightmap => Get("PickUpAllFurniAndResetHeightmap");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateRoomFloorProperties (Unity) message.
	/// </summary>
	public Header UpdateRoomFloorProperties => Get("UpdateRoomFloorProperties");
		
	/// <summary>
	/// Gets the outgoing header for the StackingHelperSetCaretHeight (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.SetCustomStackingHeight" />.
	/// </summary>
	public Header StackingHelperSetCaretHeight => Get("StackingHelperSetCaretHeight");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomOccupiedTiles (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetOccupiedTiles" />.
	/// </summary>
	public Header GetRoomOccupiedTiles => Get("GetRoomOccupiedTiles");
		
	/// <summary>
	/// Gets the outgoing header for the YoutubeDisplayGetStatus (Unity) message.
	/// </summary>
	public Header YoutubeDisplayGetStatus => Get("YoutubeDisplayGetStatus");
		
	/// <summary>
	/// Gets the outgoing header for the YoutubeDisplaySetPlaylist (Unity) message.
	/// </summary>
	public Header YoutubeDisplaySetPlaylist => Get("YoutubeDisplaySetPlaylist");
		
	/// <summary>
	/// Gets the outgoing header for the YoutubeDisplayControlPlayback (Unity) message.
	/// </summary>
	public Header YoutubeDisplayControlPlayback => Get("YoutubeDisplayControlPlayback");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceGetInfo (Unity) message.
	/// </summary>
	public Header RentableSpaceGetInfo => Get("RentableSpaceGetInfo");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceRentSpace (Unity) message.
	/// </summary>
	public Header RentableSpaceRentSpace => Get("RentableSpaceRentSpace");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceExtendRent (Unity) message.
	/// </summary>
	public Header RentableSpaceExtendRent => Get("RentableSpaceExtendRent");
		
	/// <summary>
	/// Gets the outgoing header for the RentableSpaceCancel (Unity) message.
	/// </summary>
	public Header RentableSpaceCancel => Get("RentableSpaceCancel");
		
	/// <summary>
	/// Gets the outgoing header for the SetPhoneNumberCollectionStatus (Unity) message.
	/// </summary>
	public Header SetPhoneNumberCollectionStatus => Get("SetPhoneNumberCollectionStatus");
		
	/// <summary>
	/// Gets the outgoing header for the GiveGift (Unity) message.
	/// </summary>
	public Header GiveGift => Get("GiveGift");
		
	/// <summary>
	/// Gets the outgoing header for the RestartPhoneNumberCollection (Unity) message.
	/// </summary>
	public Header RestartPhoneNumberCollection => Get("RestartPhoneNumberCollection");
		
	/// <summary>
	/// Gets the outgoing header for the GiveStarGems (Unity) message.
	/// </summary>
	public Header GiveStarGems => Get("GiveStarGems");
		
	/// <summary>
	/// Gets the outgoing header for the NuxGetGifts (Unity) message.
	/// </summary>
	public Header NuxGetGifts => Get("NuxGetGifts");
		
	/// <summary>
	/// Gets the outgoing header for the ScriptProceed (Unity) message.
	/// </summary>
	public Header ScriptProceed => Get("ScriptProceed");
		
	/// <summary>
	/// Gets the outgoing header for the GetTargetedOffer (Unity) message.
	/// </summary>
	public Header GetTargetedOffer => Get("GetTargetedOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetTargetedOfferList (Unity) message.
	/// </summary>
	public Header GetTargetedOfferList => Get("GetTargetedOfferList");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewSessionGuideDecidesOnOffer (Unity) message.
	/// </summary>
	public Header ChatReviewSessionGuideDecidesOnOffer => Get("ChatReviewSessionGuideDecidesOnOffer");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewSessionGuideVote (Unity) message.
	/// </summary>
	public Header ChatReviewSessionGuideVote => Get("ChatReviewSessionGuideVote");
		
	/// <summary>
	/// Gets the outgoing header for the ChatReviewSessionGuideDetached (Unity) message.
	/// </summary>
	public Header ChatReviewSessionGuideDetached => Get("ChatReviewSessionGuideDetached");
		
	/// <summary>
	/// Gets the outgoing header for the AccountSafetylockGetQuestions (Unity) message.
	/// </summary>
	public Header AccountSafetylockGetQuestions => Get("AccountSafetylockGetQuestions");
		
	/// <summary>
	/// Gets the outgoing header for the AccountSafetylockUnlock (Unity) message.
	/// </summary>
	public Header AccountSafetylockUnlock => Get("AccountSafetylockUnlock");
		
	/// <summary>
	/// Gets the outgoing header for the AccountSafetyLock (Unity) message.
	/// </summary>
	public Header AccountSafetyLock => Get("AccountSafetyLock");
		
	/// <summary>
	/// Gets the outgoing header for the SubmitGdprRequest (Unity) message.
	/// </summary>
	public Header SubmitGdprRequest => Get("SubmitGdprRequest");
		
	/// <summary>
	/// Gets the outgoing header for the CancelGdprRequest (Unity) message.
	/// </summary>
	public Header CancelGdprRequest => Get("CancelGdprRequest");
		
	/// <summary>
	/// Gets the outgoing header for the GetGdprRequest (Unity) message.
	/// </summary>
	public Header GetGdprRequest => Get("GetGdprRequest");
		
	/// <summary>
	/// Gets the outgoing header for the GetForumThreads (Unity) message.
	/// </summary>
	public Header GetForumThreads => Get("GetForumThreads");
		
	/// <summary>
	/// Gets the outgoing header for the GetForumThreadMessages (Unity) message.
	/// </summary>
	public Header GetForumThreadMessages => Get("GetForumThreadMessages");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateForumReadMarkers (Unity) message.
	/// </summary>
	public Header UpdateForumReadMarkers => Get("UpdateForumReadMarkers");
		
	/// <summary>
	/// Gets the outgoing header for the GetForumThread (Unity) message.
	/// </summary>
	public Header GetForumThread => Get("GetForumThread");
		
	/// <summary>
	/// Gets the outgoing header for the PostForumMessage (Unity) message.
	/// </summary>
	public Header PostForumMessage => Get("PostForumMessage");
		
	/// <summary>
	/// Gets the outgoing header for the ModerateForumThread (Unity) message.
	/// </summary>
	public Header ModerateForumThread => Get("ModerateForumThread");
		
	/// <summary>
	/// Gets the outgoing header for the ModerateForumMessage (Unity) message.
	/// </summary>
	public Header ModerateForumMessage => Get("ModerateForumMessage");
		
	/// <summary>
	/// Gets the outgoing header for the ReportForumThread (Unity) message.
	/// </summary>
	public Header ReportForumThread => Get("ReportForumThread");
		
	/// <summary>
	/// Gets the outgoing header for the ReportForumMessage (Unity) message.
	/// </summary>
	public Header ReportForumMessage => Get("ReportForumMessage");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateForumThread (Unity) message.
	/// </summary>
	public Header UpdateForumThread => Get("UpdateForumThread");
		
	/// <summary>
	/// Gets the outgoing header for the GetRoomUsersClassification (Unity) message.
	/// </summary>
	public Header GetRoomUsersClassification => Get("GetRoomUsersClassification");
		
	/// <summary>
	/// Gets the outgoing header for the GetPeerUsersClassification (Unity) message.
	/// </summary>
	public Header GetPeerUsersClassification => Get("GetPeerUsersClassification");
		
	/// <summary>
	/// Gets the outgoing header for the ReportSelfie (Unity) message.
	/// </summary>
	public Header ReportSelfie => Get("ReportSelfie");
		
	/// <summary>
	/// Gets the outgoing header for the ReportPhoto (Unity) message.
	/// </summary>
	public Header ReportPhoto => Get("ReportPhoto");
		
	/// <summary>
	/// Gets the outgoing header for the UserFeedback (Unity) message.
	/// </summary>
	public Header UserFeedback => Get("UserFeedback");
		
	/// <summary>
	/// Gets the outgoing header for the GetReputation (Unity) message.
	/// </summary>
	public Header GetReputation => Get("GetReputation");
		
	/// <summary>
	/// Gets the outgoing header for the GetFurniByRoomInventory (Unity) message.
	/// </summary>
	public Header GetFurniByRoomInventory => Get("GetFurniByRoomInventory");
		
	/// <summary>
	/// Gets the outgoing header for the GetInventoryForDebugging (Unity) message.
	/// </summary>
	public Header GetInventoryForDebugging => Get("GetInventoryForDebugging");
		
	/// <summary>
	/// Gets the outgoing header for the GetNewPetInfo (Unity) message.
	/// </summary>
	public Header GetNewPetInfo => Get("GetNewPetInfo");
		
	/// <summary>
	/// Gets the outgoing header for the PlacePetToFlat (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.PlacePet" />.
	/// </summary>
	public Header PlacePetToFlat => Get("PlacePetToFlat");
		
	/// <summary>
	/// Gets the outgoing header for the GetAvailablePetCommands (Unity) message.
	/// </summary>
	public Header GetAvailablePetCommands => Get("GetAvailablePetCommands");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveSaddle (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RemoveSaddleFromPet" />.
	/// </summary>
	public Header RemoveSaddle => Get("RemoveSaddle");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceMakeOffer (Unity) message.
	/// </summary>
	public Header MarketplaceMakeOffer => Get("MarketplaceMakeOffer");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceGetConfiguration (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMarketplaceConfiguration" />.
	/// </summary>
	public Header MarketplaceGetConfiguration => Get("MarketplaceGetConfiguration");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceCanMakeOffer (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMarketplaceCanMakeOffer" />.
	/// </summary>
	public Header MarketplaceCanMakeOffer => Get("MarketplaceCanMakeOffer");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceBuyTokens (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.BuyMarketplaceTokens" />.
	/// </summary>
	public Header MarketplaceBuyTokens => Get("MarketplaceBuyTokens");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceBuyOffer (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.BuyMarketplaceOffer" />.
	/// </summary>
	public Header MarketplaceBuyOffer => Get("MarketplaceBuyOffer");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceCancelOffer (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CancelMarketplaceOffer" />.
	/// </summary>
	public Header MarketplaceCancelOffer => Get("MarketplaceCancelOffer");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceRedeemOfferCredits (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RedeemMarketplaceOfferCredits" />.
	/// </summary>
	public Header MarketplaceRedeemOfferCredits => Get("MarketplaceRedeemOfferCredits");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceSearchOffers (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMarketplaceOffers" />.
	/// </summary>
	public Header MarketplaceSearchOffers => Get("MarketplaceSearchOffers");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceListOwnOffers (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMarketplaceOwnOffers" />.
	/// </summary>
	public Header MarketplaceListOwnOffers => Get("MarketplaceListOwnOffers");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceGetItemStats (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMarketplaceItemStats" />.
	/// </summary>
	public Header MarketplaceGetItemStats => Get("MarketplaceGetItemStats");
		
	/// <summary>
	/// Gets the outgoing header for the TogglePetRidingAccessRights (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.TogglePetRidingPermission" />.
	/// </summary>
	public Header TogglePetRidingAccessRights => Get("TogglePetRidingAccessRights");
		
	/// <summary>
	/// Gets the outgoing header for the MovePetInFlat (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.MovePet" />.
	/// </summary>
	public Header MovePetInFlat => Get("MovePetInFlat");
		
	/// <summary>
	/// Gets the outgoing header for the TogglePetBreedingRights (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.TogglePetBreedingPermission" />.
	/// </summary>
	public Header TogglePetBreedingRights => Get("TogglePetBreedingRights");
		
	/// <summary>
	/// Gets the outgoing header for the PlaceBotToFlat (Unity) message.
	/// </summary>
	public Header PlaceBotToFlat => Get("PlaceBotToFlat");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboClubOffers (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetClubOffers" />.
	/// </summary>
	public Header GetHabboClubOffers => Get("GetHabboClubOffers");
		
	/// <summary>
	/// Gets the outgoing header for the ChargeStuff (Unity) message.
	/// </summary>
	public Header ChargeStuff => Get("ChargeStuff");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboVipMembershipExtendOffer (Unity) message.
	/// </summary>
	public Header GetHabboVipMembershipExtendOffer => Get("GetHabboVipMembershipExtendOffer");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseDiscountedVipMembershipExtension (Unity) message.
	/// </summary>
	public Header PurchaseDiscountedVipMembershipExtension => Get("PurchaseDiscountedVipMembershipExtension");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseDiscountedBasicMembershipExtension (Unity) message.
	/// </summary>
	public Header PurchaseDiscountedBasicMembershipExtension => Get("PurchaseDiscountedBasicMembershipExtension");
		
	/// <summary>
	/// Gets the outgoing header for the GetHabboBasicMembershipExtendOffer (Unity) message.
	/// </summary>
	public Header GetHabboBasicMembershipExtendOffer => Get("GetHabboBasicMembershipExtendOffer");
		
	/// <summary>
	/// Gets the outgoing header for the PurchaseSnowWarGameTokens (Unity) message.
	/// </summary>
	public Header PurchaseSnowWarGameTokens => Get("PurchaseSnowWarGameTokens");
		
	/// <summary>
	/// Gets the outgoing header for the RequestBadge (Unity) message.
	/// </summary>
	public Header RequestBadge => Get("RequestBadge");
		
	/// <summary>
	/// Gets the outgoing header for the MarketplaceCancelAllOffers (Unity) message.
	/// </summary>
	public Header MarketplaceCancelAllOffers => Get("MarketplaceCancelAllOffers");
		
	/// <summary>
	/// Gets the outgoing header for the SendPetToHoliday (Unity) message.
	/// </summary>
	public Header SendPetToHoliday => Get("SendPetToHoliday");
		
	/// <summary>
	/// Gets the outgoing header for the UserDefinedRoomEventsUpdateTrigger (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsUpdateTrigger => Get("UserDefinedRoomEventsUpdateTrigger");
		
	/// <summary>
	/// Gets the outgoing header for the UserDefinedRoomEventsUpdateAction (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsUpdateAction => Get("UserDefinedRoomEventsUpdateAction");
		
	/// <summary>
	/// Gets the outgoing header for the UserDefinedRoomEventsUpdateCondition (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsUpdateCondition => Get("UserDefinedRoomEventsUpdateCondition");
		
	/// <summary>
	/// Gets the outgoing header for the UserDefinedRoomEventsOpen (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsOpen => Get("UserDefinedRoomEventsOpen");
		
	/// <summary>
	/// Gets the outgoing header for the UserDefinedRoomEventsApplySnapshot (Unity) message.
	/// </summary>
	public Header UserDefinedRoomEventsApplySnapshot => Get("UserDefinedRoomEventsApplySnapshot");
		
	/// <summary>
	/// Gets the outgoing header for the GetMessageOfTheDay (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetMOTD" />.
	/// </summary>
	public Header GetMessageOfTheDay => Get("GetMessageOfTheDay");
		
	/// <summary>
	/// Gets the outgoing header for the ResetUnseenCounter (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ResetUnseenItems" />.
	/// </summary>
	public Header ResetUnseenCounter => Get("ResetUnseenCounter");
		
	/// <summary>
	/// Gets the outgoing header for the GetSeasonalQuests (Unity) message.
	/// </summary>
	public Header GetSeasonalQuests => Get("GetSeasonalQuests");
		
	/// <summary>
	/// Gets the outgoing header for the GetIdentityAgreementTypes (Unity) message.
	/// </summary>
	public Header GetIdentityAgreementTypes => Get("GetIdentityAgreementTypes");
		
	/// <summary>
	/// Gets the outgoing header for the SaveAgreements (Unity) message.
	/// </summary>
	public Header SaveAgreements => Get("SaveAgreements");
		
	/// <summary>
	/// Gets the outgoing header for the GetIdentityAgreements (Unity) message.
	/// </summary>
	public Header GetIdentityAgreements => Get("GetIdentityAgreements");
		
	/// <summary>
	/// Gets the outgoing header for the GetGuildMembershipRequests (Unity) message.
	/// </summary>
	public Header GetGuildMembershipRequests => Get("GetGuildMembershipRequests");
		
	/// <summary>
	/// Gets the outgoing header for the GuildMemberHqFurniCount (Unity) message.
	/// </summary>
	public Header GuildMemberHqFurniCount => Get("GuildMemberHqFurniCount");
		
	/// <summary>
	/// Gets the outgoing header for the GetDirectClubBuyAllowed (Unity) message.
	/// </summary>
	public Header GetDirectClubBuyAllowed => Get("GetDirectClubBuyAllowed");
		
	/// <summary>
	/// Gets the outgoing header for the GetExtendedProfileByUsername (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetExtendedProfileByName" />.
	/// </summary>
	public Header GetExtendedProfileByUsername => Get("GetExtendedProfileByUsername");
		
	/// <summary>
	/// Gets the outgoing header for the GetSeasonalCalendarDailyOffer (Unity) message.
	/// </summary>
	public Header GetSeasonalCalendarDailyOffer => Get("GetSeasonalCalendarDailyOffer");
		
	/// <summary>
	/// Gets the outgoing header for the GetLimitedFurniTimingInfo (Unity) message.
	/// </summary>
	public Header GetLimitedFurniTimingInfo => Get("GetLimitedFurniTimingInfo");
		
	/// <summary>
	/// Gets the outgoing header for the GetCommunityGoalEarnedPrizes (Unity) message.
	/// </summary>
	public Header GetCommunityGoalEarnedPrizes => Get("GetCommunityGoalEarnedPrizes");
		
	/// <summary>
	/// Gets the outgoing header for the RedeemCommunityGoalPrize (Unity) message.
	/// </summary>
	public Header RedeemCommunityGoalPrize => Get("RedeemCommunityGoalPrize");
		
	/// <summary>
	/// Gets the outgoing header for the GetCatalogPageExpiration (Unity) message.
	/// </summary>
	public Header GetCatalogPageExpiration => Get("GetCatalogPageExpiration");
		
	/// <summary>
	/// Gets the outgoing header for the GetBannedUsers (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.GetBannedUsersFromRoom" />.
	/// </summary>
	public Header GetBannedUsers => Get("GetBannedUsers");
		
	/// <summary>
	/// Gets the outgoing header for the RoomUnbanUser (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.UnbanUserFromRoom" />.
	/// </summary>
	public Header RoomUnbanUser => Get("RoomUnbanUser");
		
	/// <summary>
	/// Gets the outgoing header for the GetFlatFavouriteCount (Unity) message.
	/// </summary>
	public Header GetFlatFavouriteCount => Get("GetFlatFavouriteCount");
		
	/// <summary>
	/// Gets the outgoing header for the GetExternalImageFurniData (Unity) message.
	/// </summary>
	public Header GetExternalImageFurniData => Get("GetExternalImageFurniData");
		
	/// <summary>
	/// Gets the outgoing header for the StartCreateGuild (Unity) message.
	/// </summary>
	public Header StartCreateGuild => Get("StartCreateGuild");
		
	/// <summary>
	/// Gets the outgoing header for the CommitCreateGuild (Unity) message.
	/// </summary>
	public Header CommitCreateGuild => Get("CommitCreateGuild");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveUnseenElements (Unity) message.
	/// </summary>
	public Header RemoveUnseenElements => Get("RemoveUnseenElements");
		
	/// <summary>
	/// Gets the outgoing header for the RemoveUnseenElement (Unity) message.
	/// </summary>
	public Header RemoveUnseenElement => Get("RemoveUnseenElement");
		
	/// <summary>
	/// Gets the outgoing header for the RequestCameraToken (Unity) message.
	/// </summary>
	public Header RequestCameraToken => Get("RequestCameraToken");
		
	/// <summary>
	/// Gets the outgoing header for the InitCamera (Unity) message.
	/// </summary>
	public Header InitCamera => Get("InitCamera");
		
	/// <summary>
	/// Gets the outgoing header for the CompetitionPhoto (Unity) message.
	/// </summary>
	public Header CompetitionPhoto => Get("CompetitionPhoto");
		
	/// <summary>
	/// Gets the outgoing header for the RenderAndSaveRoomThumbnailPhoto (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.RenderRoomThumbnail" />.
	/// </summary>
	public Header RenderAndSaveRoomThumbnailPhoto => Get("RenderAndSaveRoomThumbnailPhoto");
		
	/// <summary>
	/// Gets the outgoing header for the MeltdownWatchVerify (Unity) message.
	/// </summary>
	public Header MeltdownWatchVerify => Get("MeltdownWatchVerify");
		
	/// <summary>
	/// Gets the outgoing header for the EarningStatus (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.IncomeRewardStatus" />.
	/// </summary>
	public Header EarningStatus => Get("EarningStatus");
		
	/// <summary>
	/// Gets the outgoing header for the ClaimEarning (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.IncomeRewardClaim" />.
	/// </summary>
	public Header ClaimEarning => Get("ClaimEarning");
		
	/// <summary>
	/// Gets the outgoing header for the VaultStatus (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.CreditVaultStatus" />.
	/// </summary>
	public Header VaultStatus => Get("VaultStatus");
		
	/// <summary>
	/// Gets the outgoing header for the WithdrawVault (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.WithdrawCreditVault" />.
	/// </summary>
	public Header WithdrawVault => Get("WithdrawVault");
		
	/// <summary>
	/// Gets the outgoing header for the UpdateAccountPreferences (Unity) message.
	/// </summary>
	public Header UpdateAccountPreferences => Get("UpdateAccountPreferences");
		
	/// <summary>
	/// Gets the outgoing header for the UnlinkIdentificationMethod (Unity) message.
	/// </summary>
	public Header UnlinkIdentificationMethod => Get("UnlinkIdentificationMethod");
		
	/// <summary>
	/// Gets the outgoing header for the LinkIdentificationMethod (Unity) message.
	/// </summary>
	public Header LinkIdentificationMethod => Get("LinkIdentificationMethod");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingPeerSlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingPeerSlow => Get("ClientDebugPingPeerSlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingPeerFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingPeerFast => Get("ClientDebugPingPeerFast");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingRoomSlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingRoomSlow => Get("ClientDebugPingRoomSlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingRoomFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingRoomFast => Get("ClientDebugPingRoomFast");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingMessengerSlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingMessengerSlow => Get("ClientDebugPingMessengerSlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingMessengerFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingMessengerFast => Get("ClientDebugPingMessengerFast");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingNavigatorSlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingNavigatorSlow => Get("ClientDebugPingNavigatorSlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingNavigatorFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingNavigatorFast => Get("ClientDebugPingNavigatorFast");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingRoomDirectorySlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingRoomDirectorySlow => Get("ClientDebugPingRoomDirectorySlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingRoomDirectoryFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingRoomDirectoryFast => Get("ClientDebugPingRoomDirectoryFast");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingProxySlow (Unity) message.
	/// </summary>
	public Header ClientDebugPingProxySlow => Get("ClientDebugPingProxySlow");
		
	/// <summary>
	/// Gets the outgoing header for the ClientDebugPingProxyFast (Unity) message.
	/// </summary>
	public Header ClientDebugPingProxyFast => Get("ClientDebugPingProxyFast");
		
	/// <summary>
	/// Gets the outgoing header for the Hello (Unity) message.
	/// The Flash equivalent for this message is <see cref="Outgoing.ClientHello" />.
	/// </summary>
	public Header Hello => Get("Hello");
		
	/// <summary>
	/// Gets the outgoing header for the GetProxyId (Unity) message.
	/// </summary>
	public Header GetProxyId => Get("GetProxyId");
}
