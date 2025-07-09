using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface ISaleService
    {
        Task<string> CreateStripeSessionAsync(SaleVm vm, string userId);
        Task<string> CreateStripeSessionAsync(List<BasketItemVm> basketItems, string userId);
        Task ProcessStripePaymentAsync(string sessionId);
        Task<List<SaleVm>> GetSalesByUserAsync(string userId);
    }

}
