
namespace Nop.Plugin.Payments.InstaPago
{
    /// <summary>
    /// Represents manual payment processor transaction mode
    /// </summary>
    public enum TransactMode
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Authorize
        /// </summary>
        Authorize = 1,

        /// <summary>
        /// Authorize and capture
        /// </summary>
        AuthorizeAndCapture= 2
    }


    public enum StatusTransactInstaPago
    {
        /// <summary>
        /// Pre-autorizaci�n
        /// </summary>
        Retener = 1,

        /// <summary>
        /// Autorizaci�n
        /// </summary>
        Pagar = 2
    }
}
