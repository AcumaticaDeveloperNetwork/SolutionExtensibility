using System;
using PX.Data;
using PX.Objects.CM.Extensions;

namespace PX.Objects.Extensions.MultiCurrency
{
    /// <summary>A mapped cache extension that represents a document that supports multiple currencies.</summary>
    public class Document : PXMappedCacheExtension
    {
        #region BAccountID
        /// <exclude />
        public abstract class bAccountID : IBqlField
        {
        }
        /// <summary>The identifier of the business account of the document.</summary>
        /// <value>
        /// Corresponds to the <see cref="BAccount.BAccountID" /> field.
        /// </value>
        public virtual Int32? BAccountID
        {
			get;
			set;
        }
        #endregion
        #region CuryID
        /// <exclude />
        public abstract class curyID : IBqlField
        {
        }
        /// <summary>
        /// The code of the <see cref="Currency"/> of the document.
        /// </summary>
        /// <value>
        /// Defaults to the <see cref="Company.BaseCuryID">base currency of the company</see>.
        /// Corresponds to the <see cref="Currency.CuryID"/> field.
        /// </value>
        public virtual String CuryID
        {
			get;
			set;
        }
		#endregion
		#region CuryViewState
		public abstract class curyViewState : IBqlField { }
		[PXBool]
		public virtual bool? CuryViewState
		{
			get;
			set;
		}
		#endregion
		#region CurrencyRate
		public abstract class curyRate : IBqlField { }
		[PXDecimal]
		public virtual decimal? CuryRate
		{
			get;
			set;
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField { }
		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[CurrencyInfo(typeof(CurrencyInfo.curyInfoID))]
		public virtual Int64? CuryInfoID { get; set; }
		#endregion
		#region DocumentDate
		/// <exclude />
		public abstract class documentDate : IBqlField
        {
        }
        /// <summary>The date of the document.</summary>
        public virtual DateTime? DocumentDate
        {
			get;
			set;
        }
        #endregion
    }
}
