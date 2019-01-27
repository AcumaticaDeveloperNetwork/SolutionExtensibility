using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Common;
using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.WZ
{
    #region WizardScenarioStatusesAttribute

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class WizardScenarioStatusesAttribute : PXStringListAttribute
    {
        public const string _PENDING = "PN";
        public const string _ACTIVE = "AC";
        public const string _SUSPEND = "SU";
        public const string _COMPLETED = "CP";

        public WizardScenarioStatusesAttribute()
            : base(new[] { _PENDING, _ACTIVE, _SUSPEND, _COMPLETED },
                    new[] { Messages.Pending, Messages.Active, Messages.Suspend, Messages.Completed })
        { }

        public sealed class Pending : Constant<string>
        {
            public Pending() : base(_PENDING) { }
        }

        public sealed class Active : Constant<string>
        {
            public Active() : base(_ACTIVE) { }
        }

        public sealed class Suspend : Constant<string>
        {
            public Suspend() : base(_SUSPEND) { }
        }

        public sealed class Completed : Constant<string>
        {
            public Completed() : base(_COMPLETED) { }
        }
    }

    #endregion

    #region WizardTaskTypesAttribute

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class WizardTaskTypesAttribute : PXStringListAttribute
    {
        public const string _ARTICLE = "AR";
        public const string _SCREEN = "SC";

        public WizardTaskTypesAttribute()
            : base(new[] { _ARTICLE, _SCREEN },
                    new[] { Messages.Article, Messages.Screen })
        { }

        public sealed class Article : Constant<string>
        {
            public Article() : base(_ARTICLE) { }
        }

        public sealed class Screen : Constant<string>
        {
            public Screen() : base(_SCREEN) { }
        }
    }

    #endregion

    #region WizardTaskStatusesAttribute

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class WizardTaskStatusesAttribute : PXStringListAttribute
    {
        public const string _PENDING = "PN";
        public const string _SKIPPED = "SK";
        public const string _ACTIVE = "AC";
        public const string _DISABLED = "DS";
        public const string _COMPLETED = "CP";
        public const string _OPEN = "OP";

        public WizardTaskStatusesAttribute()
            : base(new[] { _PENDING, _SKIPPED, _ACTIVE, _DISABLED, _COMPLETED, _OPEN },
                    new[] { Messages.Pending, Messages.Skipped, Messages.Active, Messages.Disabled, Messages.Completed, Messages.Open })
        { }

        public sealed class Pending : Constant<string>
        {
            public Pending() : base(_PENDING) { }
        }

        public sealed class Skipped : Constant<string>
        {
            public Skipped() : base(_SKIPPED) { }
        }

        public sealed class Active : Constant<string>
        {
            public Active() : base(_ACTIVE) { }
        }

        public sealed class Disabled : Constant<string>
        {
            public Disabled() : base(_DISABLED) { }
        }

        public sealed class Completed : Constant<string>
        {
            public Completed() : base(_COMPLETED) { }
        }

        public sealed class Open : Constant<string>
        {
            public Open() : base(_OPEN) { }
        }

    }

    #endregion
}
