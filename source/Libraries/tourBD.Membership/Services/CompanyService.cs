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
        
        public async Task<Company> GetAsync(Guid Id)
        {
            return await _companyUnitOfWork.CompanyRepository.GetAsync(Id);
        }

        public async Task<IEnumerable<Company>> GetUserCompaniesAsync(Guid userId)
        {
            var Companies = await _companyUnitOfWork.CompanyRepository.GetAsync(c => c.UserId == userId, "", "", "", true);
            var propertyIncludedCompanyList = new List<Company>();
            foreach (var company in Companies)
            {
                propertyIncludedCompanyList.Add(await GetCompanyWithAllIncludePropertiesAsync(company.Id));
            }

            return propertyIncludedCompanyList;
        }

        public async Task CreateTourPackage(TourPackage tourPackage)
        {
            await _companyUnitOfWork.TourPackageRepository.AddAsync(tourPackage);
            await _companyUnitOfWork.SaveAsync();
        }

        public async Task<Company> GetCompanyWithAllIncludePropertiesAsync(Guid companyId)
        {
            return await _companyUnitOfWork.CompanyRepository.GetWithAllIncludePropertiesAsync(companyId);
        }

        public async Task<List<Company>> GetAllIncludePropertiesAsync()
        {
            return await _companyUnitOfWork.CompanyRepository.GetAllIncludePropertiesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _companyUnitOfWork.CompanyRepository.GetCountAsync();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyUnitOfWork.CompanyRepository.GetAllAsync();
        }
    }
}
