// This is the Aduanas Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in AduanasServices.cs
    public interface IAduanasService {
        Task<int> AduanasInsert(string nombreAduana, string abreviacionAduana);
        Task<IEnumerable<Aduanas>> AduanasList();
        Task<Aduanas> Aduanas_GetOne(int AduanasID);
        Task<int> AduanasUpdate(string nombreAduana, string abreviacionAduana, int aduanaID);
    }
}
