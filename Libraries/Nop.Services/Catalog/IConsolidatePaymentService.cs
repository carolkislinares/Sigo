using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public partial interface IConsolidatePaymentService
    {
        /// <summary>
        /// Delete ConsolidatePayment
        /// </summary>
        /// <param name="category">Category</param>
        void Delete(ConsolidatePayment obj);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching)</param>
        /// <returns>Categories</returns>
        IPagedList<ConsolidatePayment> GetAllConsolidatePayments(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IPagedList<ConsolidatePayment> GetAllConsolidatePayments(string reference,
            int orderid = 0,
            int receiverbank = 0,
            int issuingbank = 0,
            int transctiontype = 0,
            DateTime? date= null, 
            int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);


        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="Id">Category identifier</param>
        /// <returns>Category</returns>
        ConsolidatePayment GetConsolidatePaymentById(int Id);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="obj">Category</param>
        void InsertConsolidatePayment(ConsolidatePayment obj);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateConsolidatePayment(ConsolidatePayment obj);

       
           }
}