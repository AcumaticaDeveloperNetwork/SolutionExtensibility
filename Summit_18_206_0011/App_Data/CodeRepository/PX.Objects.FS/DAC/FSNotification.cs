using PX.Data;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.SM;
using System;

namespace PX.Objects.AR
{
    public class FSModule : PXModule
    {
        public const string FS = "FS";

        public class fs : Constant<string>
        {
            public fs() : base(FS)
            {
            }
        }

        public class fs_ : Constant<string>
        {
            public fs_() : base(FS + "%")
            {
            }
        }

        /*public class namespaceFS : Constant<string>
        {
            public namespaceFS() : base("PX.Objects.AR.Reports") { } @TODO : SD-6165 
        }*/
    }

    [PXProjection(typeof(Select<NotificationSetup,
        Where<NotificationSetup.module, Equal<FSModule.fs>>>), Persistent = true)]
    [Serializable]
    public partial class FSNotification : NotificationSetup
    {
        #region SetupID
        public new abstract class setupID : PX.Data.IBqlField
        {
        }
        #endregion
        #region Module
        public new abstract class module : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, IsKey = true)]
        [PXDefault(FSModule.FS)]
        public override string Module
        {
            get
            {
                return this._Module;
            }

            set
            {
                this._Module = value;
            }
        }
        #endregion
        #region SourceCD
        public new abstract class sourceCD : PX.Data.IBqlField
        {
        }

        [PXDefault(FSNotificationSource.Appointment)]
        [PXDBString(10, IsKey = true, InputMask = "")]
        [PXCheckUnique]
        public override string SourceCD
        {
            get
            {
                return this._SourceCD;
            }

            set
            {
                this._SourceCD = value;
            }
        }
        #endregion
        #region NotificationCD
        public new abstract class notificationCD : PX.Data.IBqlField
        {
        }

        [PXDBString(30, IsKey = true)]
        [PXUIField(DisplayName = "Mailing ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXCheckUnique]
        public override string NotificationCD
        {
            get
            {
                return this._NotificationCD;
            }

            set
            {
                this._NotificationCD = value;
            }
        }
        #endregion
        #region ReportID
        public new abstract class reportID : PX.Data.IBqlField
        {
        }

        [PXDBString(8, InputMask = "CC.CC.CC.CC")]
        [PXUIField(DisplayName = "Report")]
        [PXSelector(typeof(Search<SiteMap.screenID,
            Where<SiteMap.screenID, Like<FSModule.fs_>, And<SiteMap.url, Like<PX.Objects.Common.urlReports>>>,
            OrderBy<Asc<SiteMap.screenID>>>), typeof(SiteMap.screenID), typeof(SiteMap.title),
            Headers = new string[] { CA.Messages.ReportID, CA.Messages.ReportName },
            DescriptionField = typeof(SiteMap.title))]
        public override string ReportID
        {
            get
            {
                return this._ReportID;
            }

            set
            {
                this._ReportID = value;
            }
        }
        #endregion
        #region TemplateID
        public abstract class templateID : PX.Data.IBqlField
        {
        }
        #endregion
        #region Active
        public new abstract class active : PX.Data.IBqlField
        {
        }
        #endregion
    }

    public class FSNotificationSource
    {
        public const string Appointment = "Appt";

        public class appointment : Constant<string>
        {
            public appointment() : base(Appointment)
            {
            }
        }
    }
}