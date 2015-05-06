using System;
using System.Collections.Generic;
using System.Linq;
using GlobalUtil;
using PaymentGateway.Models;

namespace PaymentGateway.Repository
{
    public class UserPaymentMethodRepository : Repository<UserPaymentMethod>, IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository()
            : base(AEGClientPortalEntities.ConnectionString)
        {
        }

        public IEnumerable<UserPaymentMethod> GetExpiringCards(DateTime cutOffDate)
        {
            var query = this.Query();

            var results = this.Query().Where(x =>
                DateTime.Today < x.ExpirationDate  // not expired yet
                && x.ExpirationDate <= cutOffDate // expire on or before cutoff date
                && x.ExpiringSoonEmailSentOn == null // haven't been emailed yet
            );

            var list = results.ToList();

            return list;
        }

        public IEnumerable<UserPaymentMethod> GetExpiredCards()
        {
            var query = this.Query();

            var results = this.Query().Where(x =>
                 x.ExpirationDate <= DateTime.Today // expired on or before today
                && x.ExpiredEmailSentOn == null // haven't been emailed yet
            ).ToList();

            var list = results.ToList();

            return list;
        }
    }
}
