using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public class TagsUserNotice : TagsUserMesage
    {
        public string Login { get; set; }
        public string MessageId { get; set; }
        public string SystemMessage { get; set; }


        public string MsgParamCumulativeMonths { get; set; }
        public string MsgParamDisplayname { get; set; }
        public string MsgParamLogin { get; set; }
        public string MsgParamMonths { get; set; }
        public string MsgParamPromoGiftTotal { get; set; }
        public string MsgParamPromoName { get; set; }
        public string MsgParamRecipientDisplayName { get; set; }
        public string MsgParamRecipientId { get; set; }
        public string MsgParamRecipientUserName { get; set; }
        public string MsgParamSenderLogin { get; set; }
        public string MsgParamSenderName { get; set; }
        public string MsgParamShouldShareStreak { get; set; }
        public string MsgParamStreakMonths { get; set; }
        public string MsgParamSubPlan { get; set; }
        public string MsgParamSubPlanName { get; set; }
        public string MsgParamViewerCount { get; set; }
        public string MsgParamRitualName { get; set; }
        public string MsgParamThreshold { get; set; }

        public TagsUserNotice()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.Login = serializer.GetSingle("login");
            this.MessageId = serializer.GetSingle("msg-id");
            this.SystemMessage = serializer.GetSingle("system-msg");

            this.MsgParamCumulativeMonths = serializer.GetSingle("msg-param-cumulative-months");
            this.MsgParamDisplayname = serializer.GetSingle("msg-param-displayName");
            this.MsgParamLogin = serializer.GetSingle("msg-param-login");
            this.MsgParamMonths = serializer.GetSingle("msg-param-months");
            this.MsgParamPromoGiftTotal = serializer.GetSingle("msg-param-promo-gift-total");
            this.MsgParamPromoName = serializer.GetSingle("msg-param-promo-name");
            this.MsgParamRecipientDisplayName = serializer.GetSingle("msg-param-recipient-display-name");
            this.MsgParamRecipientId = serializer.GetSingle("msg-param-recipient-id");
            this.MsgParamRecipientUserName = serializer.GetSingle("msg-param-recipient-user-name");
            this.MsgParamSenderLogin = serializer.GetSingle("msg-param-sender-login");
            this.MsgParamSenderName = serializer.GetSingle("msg-param-sender-name");
            this.MsgParamShouldShareStreak = serializer.GetSingle("msg-param-should-share-streak");
            this.MsgParamStreakMonths = serializer.GetSingle("msg-param-streak-months");
            this.MsgParamSubPlan = serializer.GetSingle("msg-param-sub-plan");
            this.MsgParamSubPlanName = serializer.GetSingle("msg-param-sub-plan-name");
            this.MsgParamViewerCount = serializer.GetSingle("msg-param-viewerCount");
            this.MsgParamRitualName = serializer.GetSingle("msg-param-ritual-name");
            this.MsgParamThreshold = serializer.GetSingle("msg-param-threshold");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
