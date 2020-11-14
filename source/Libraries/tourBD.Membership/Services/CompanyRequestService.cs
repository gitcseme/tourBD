using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;
using tourBD.Membership.UnitOfWorks;

namespace tourBD.Membership.Services
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private ICompanyRequestUnitOfWork _companyRequestUnitOfWork;
        public CompanyRequestService(ICompanyRequestUnitOfWork companyRequestUnitOfWork)
        {
            _companyRequestUnitOfWork = companyRequestUnitOfWork;
        }

        public async Task CreateAsync(CompanyRequest entity)
        {
            await _companyRequestUnitOfWork.CompanyRequestRepository.AddAsync(entity);
            await _companyRequestUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(CompanyRequest entity)
        {
            await _companyRequestUnitOfWork.CompanyRequestRepository.RemoveAsync(entity);
            await _companyRequestUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _companyRequestUnitOfWork.Dispose();
        }

        public async Task EditAsync(CompanyRequest entity)
        {
            await _companyRequestUnitOfWork.CompanyRequestRepository.EditAsync(entity);
            await _companyRequestUnitOfWork.SaveAsync();
        }

        public CompanyRequest Get(Guid Id)
        {
            return _companyRequestUnitOfWork.CompanyRequestRepository.Get(Id);
        }

        public async Task<CompanyRequest> GetAsync(Guid Id)
        {
            return await _companyRequestUnitOfWork.CompanyRequestRepository.GetAsync(Id);
        }

        public (IEnumerable<CompanyRequest>, int, int) GetRequests(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return _companyRequestUnitOfWork.CompanyRequestRepository.Get(r => r.Description.Contains(searchText), orderingColumn, orderDirection, "", pageIndex, pageSize, isTrackingOff);
        }

        public async Task<(IEnumerable<CompanyRequest>, int, int)> GetRequestsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return await _companyRequestUnitOfWork.CompanyRequestRepository.GetAsync(null, orderingColumn, orderDirection, "", pageIndex, pageSize, isTrackingOff);
        }

        public async Task<bool> HastPendingReques(Guid userId)
        {
            return (await _companyRequestUnitOfWork.CompanyRequestRepository.GetAllAsync()).
                ToList()
                .Where(cr => cr.UserId == userId && cr.RequestStatus == CompanyRequestStatus.Pending.ToString()).Any();
        }
    }
}
