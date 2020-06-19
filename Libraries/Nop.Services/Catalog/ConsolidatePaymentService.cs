using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;

namespace Nop.Services.Catalog
{

    /// <summary>
    /// Consolidate Payment service
    /// </summary>
    public partial class ConsolidatePaymentService : IConsolidatePaymentService
    {
        #region Constants

        /// <summary>
        /// Cache key for pickup points
        /// </summary>
        /// <remarks>
        /// {0} : page index
        /// {1} : page size
        /// {2} : current store ID
        /// </remarks>
        private const string CONSOLIDATE_ALL_KEY = "Nop.CONSOLIDATE.all-{0}-{1}-{2}";
        private const string CONSOLIDATE_PATTERN_KEY = "Nop.CONSOLIDATE.";

        #endregion

        #region Fields
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ConsolidatePayment> _paymentRepository;
        private readonly IRepository<Order> _orderRepository;

        #endregion


        #region Ctor
        public ConsolidatePaymentService(ICacheManager cacheManager,
          IRepository<ConsolidatePayment> paymentRepository, 
          IRepository<Order> orderRepository)
        {
            this._cacheManager = cacheManager;
            this._paymentRepository = paymentRepository;
            this._orderRepository = orderRepository;
        }
        #endregion


        #region method

        public void Delete(ConsolidatePayment obj)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ConsolidatePayment> GetAllConsolidatePayments(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {

                var key = string.Format(CONSOLIDATE_ALL_KEY, pageIndex, pageSize, storeId);
                var consolidatepayments = _paymentRepository.Table;
                var orders = _orderRepository.Table;

                return _cacheManager.Get(key, () =>
                {
                    var listTransfer = from tr in consolidatepayments
                                       join or in orders on tr.OrderId equals or.Id 
                                       select tr;
                    return new PagedList<ConsolidatePayment>(listTransfer, pageIndex, pageSize);
                });

            }
            catch (Exception ex)
            {
                throw new NopException("Error al obtener la lista: " + ex.Message, ex);
            }
        }

        public IPagedList<ConsolidatePayment> GetAllConsolidatePayments(string reference, int orderid = 0, 
            int receiverbank = 0, int issuingbank = 0, int transctiontype = 0, 
            DateTime? date = null, int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            try
            {
                var key = string.Format(CONSOLIDATE_ALL_KEY, pageIndex, pageSize, storeId);
                return _cacheManager.Get(key, () =>
                {
                    var query = _paymentRepository.Table;

                    if (storeId > 0)
                        query = query.Where(b => b.StoreId == storeId || b.StoreId == 0);

                    if (!string.IsNullOrWhiteSpace(reference))
                         query = query.Where(b => b.ReferenceCode.Contains(reference));

                    if (orderid > 0)
                        query = query.Where(b => b.OrderId == orderid || b.OrderId == 0);

                    if (receiverbank > 0)
                        query = query.Where(b => b.ReceiverBankId == receiverbank || b.ReceiverBankId == 0);

                    if (issuingbank > 0)
                        query = query.Where(b => b.IssuingBankId == issuingbank || b.IssuingBankId == 0);

                    if (date.HasValue)
                        query = query.Where(b => b.CreateOn.Date <= date.Value.Date);

                    if (transctiontype > 0)
                        query = query.Where(b => b.TransactionType == transctiontype || b.TransactionType == 0);

                    query = query.OrderByDescending(b => b.OrderId).ThenBy(b => b.CreateOn);

                    return new PagedList<ConsolidatePayment>(query, pageIndex, pageSize);
                });
            }
            catch (Exception ex)
            {
                throw new NopException("Error al obtener los pagos: " + ex.Message, ex);
            }
        }

        public ConsolidatePayment GetConsolidatePaymentById(int Id)
        {
            try
            {
                if (Id == 0)
                    return null;

                return _paymentRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw new NopException("Error al obtener el banco: " + ex.Message, ex);
            }
        }

        public void InsertConsolidatePayment(ConsolidatePayment obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));

                _paymentRepository.Insert(obj);
                _cacheManager.RemoveByPattern(CONSOLIDATE_PATTERN_KEY);
            }
            catch (Exception ex)
            {
                throw new NopException("Error al insertar el pago: " + ex.Message, ex);
            }
        }

        public void UpdateConsolidatePayment(ConsolidatePayment obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));

                _paymentRepository.Update(obj);
                _cacheManager.RemoveByPattern(CONSOLIDATE_PATTERN_KEY);

            }
            catch (Exception ex)
            {
                throw new NopException("Error al actualizar el pago: " + ex.Message, ex);
            }
        }
    }

    #endregion
}