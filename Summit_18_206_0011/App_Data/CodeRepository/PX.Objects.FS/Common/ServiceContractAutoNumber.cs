using PX.Data;

namespace PX.Objects.FS
{
    public class ServiceContractAutoNumberAttribute : AlternateAutoNumberAttribute
    {
        #region Private Members

        private string initialRefNbr = "000001";

        #endregion

        /// <summary>
        /// Allows to calculate the <c>RefNbr</c> sequence when trying to insert a new register
        /// It's called from the Persisting event of FSServiceContract.
        /// </summary>
        protected override bool SetRefNbr(PXCache cache, object row)
        {
            FSServiceContract fsServiceContractRow = (FSServiceContract)row;

            FSServiceContract fsServiceContractRow_tmp = PXSelectGroupBy<FSServiceContract,
                                            Where<
                                                FSServiceContract.customerID, Equal<Current<FSServiceContract.customerID>>>,
                                            Aggregate<
                                                Max<FSServiceContract.refNbr,
                                            GroupBy<
                                                FSServiceContract.customerID>>>>
                                    .Select(cache.Graph);

            string refNbr = fsServiceContractRow_tmp == null ? null : fsServiceContractRow_tmp.RefNbr;

            if (string.IsNullOrEmpty(refNbr))
            {
                refNbr = initialRefNbr;
            }
            else
            {
                refNbr = (int.Parse(refNbr) + 1).ToString().PadLeft(initialRefNbr.Length, '0');
            }

            fsServiceContractRow.RefNbr = refNbr;

            return true;
        }
    }
}