using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Membership.Entities;
using tourBD.Membership.UnitOfWorks;

namespace tourBD.Membership.Services
{
    public class CompanyService : ICompanyService
    {
        private ICompanyUnitOfWork _companyUnitOfWork { get; }

        public CompanyService(ICompanyUnitOfWork companyUnitOfWork)
        {
            _companyUnitOfWork = companyUnitOfWork;
        }

        public async Task CreateAsync(Company entity)
        {
            await _companyUnitOfWork.CompanyRepository.AddAsync(entity);
            await _companyUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Company entity)
        {
            await _companyUnitOfWork.CompanyRepository.RemoveAsync(entity);
            await _companyUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _companyUnitOfWork.Dispose();
        }

        public async Task EditAsync(Company entity)
        {
            await _companyUnitOfWork.CompanyRepository.EditAsync(entity);
            await _companyUnitOfWork.SaveAsync();
        }

        public Company Get(Guid Id)
        {
            return _companyUnitOfWork.CompanyRepository.Get(Id);
        }

        public async Task<IEnumerable<Company>> GetUserCompaniesAsync(Guid userId)
        {
            return await _companyUnitOfWork.CompanyRepository.GetAsync(c => c.UserId == userId, "", "", "", true);
        }
    }
}
