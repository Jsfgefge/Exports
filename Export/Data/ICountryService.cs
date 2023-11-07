// This is the Country Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in CountryServices.cs
    public interface ICountryService {
        Task<int> CountryInsert(string countryName, 
                                string countryISO);
        Task<IEnumerable<Country>> CountryList();
        Task<Country> Country_GetOne(int CountryID);
        Task<int> CountryUpdate(string countryName, 
                                string countryISO, 
                                int countryID);
    }
}