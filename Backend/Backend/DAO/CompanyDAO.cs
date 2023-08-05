using Backend.DTO;
using MySql.Data.MySqlClient;

namespace Backend.DAO
{
    public class CompanyDAO
    {
        public CompanyDTO ListCompany(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT Company.Id AS CompanyId, Company.Name, Company.Document, Company.CreateDate AS CompanyCreateDate, Company.UpdateDate AS CompanyUpdateDate, CompanyAddress.Id AS CompanyAddressId, CompanyAddress.CompanyId AS CompanyAddressCompanyId, CompanyAddress.Street, CompanyAddress.Neighborhood, CompanyAddress.City, CompanyAddress.PostalCode, CompanyAddress.Country, CompanyAddress.CreateDate AS CompanyAddressCreateDate, CompanyAddress.UpdateDate AS CompanyAddressUpdateDate, CompanyTelephone.Id AS CompanyTelephoneId, CompanyTelephone.CompanyAddress, CompanyTelephone.PhoneNumber, CompanyTelephone.CreateDate AS CompanyTelephoneCreateDate, CompanyTelephone.UpdateDate AS CompanyTelephoneUpdateDate FROM Company INNER JOIN CompanyAddress ON Company.Id = CompanyAddress.CompanyId INNER JOIN CompanyTelephone ON CompanyAddress.Id = CompanyTelephone.CompanyAddress WHERE Company.Id = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);
            var dataReader = comando.ExecuteReader();

            var companyDictionary = new Dictionary<int, CompanyAddressDTO>();
            CompanyDTO company = null;

            while (dataReader.Read())
            {
                company ??= new CompanyDTO
                    {
                        Id = int.Parse(dataReader["CompanyId"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Document = dataReader["Document"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyUpdateDate"]),

                        CompanyAddresses = new List<CompanyAddressDTO>()
                    };

                var addressId = int.Parse(dataReader["CompanyAddressId"].ToString());
                if (!companyDictionary.TryGetValue(addressId, out var address))
                {
                    address = new CompanyAddressDTO
                    {
                        Id = addressId,
                        CompanyId = company.Id,
                        Street = dataReader["Street"].ToString(),
                        Neighborhood = dataReader["Neighborhood"].ToString(),
                        City = dataReader["City"].ToString(),
                        PostalCode = dataReader["PostalCode"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyAddressCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyAddressUpdateDate"]),
                        CompanyTelephones = new List<CompanyTelephoneDTO>()
                    };

                    company.CompanyAddresses.Add(address);
                    companyDictionary.Add(address.Id, address);
                }

                var telephoneId = int.Parse(dataReader["CompanyTelephoneId"].ToString());
                if (!address.CompanyTelephones.Any(x => x.Id == telephoneId))
                {
                    var telephone = new CompanyTelephoneDTO
                    {
                        Id = telephoneId,
                        CompanyAddress = address.Id,
                        PhoneNumber = dataReader["PhoneNumber"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyTelephoneCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyTelephoneUpdateDate"])
                    };

                    address.CompanyTelephones.Add(telephone);
                }
            }

            conexao.Close();
            return company;
        }

        public List<CompanyDTO> ListAllCompanies()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT Company.Id AS CompanyId, Company.Name, Company.Document, Company.CreateDate AS CompanyCreateDate, Company.UpdateDate AS CompanyUpdateDate, CompanyAddress.Id AS CompanyAddressId, CompanyAddress.CompanyId AS CompanyAddressCompanyId, CompanyAddress.Street, CompanyAddress.Neighborhood, CompanyAddress.City, CompanyAddress.PostalCode, CompanyAddress.Country, CompanyAddress.CreateDate AS CompanyAddressCreateDate, CompanyAddress.UpdateDate AS CompanyAddressUpdateDate, CompanyTelephone.Id AS CompanyTelephoneId, CompanyTelephone.CompanyAddress, CompanyTelephone.PhoneNumber, CompanyTelephone.CreateDate AS CompanyTelephoneCreateDate, CompanyTelephone.UpdateDate AS CompanyTelephoneUpdateDate FROM Company INNER JOIN CompanyAddress ON Company.Id = CompanyAddress.CompanyId INNER JOIN CompanyTelephone ON CompanyAddress.Id = CompanyTelephone.CompanyAddress;";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var companies = new List<CompanyDTO>();

            while (dataReader.Read())
            {
                var companyId = int.Parse(dataReader["CompanyId"].ToString());
                var company = companies.FirstOrDefault(c => c.Id == companyId);

                if (company == null)
                {
                    company = new CompanyDTO
                    {
                        Id = companyId,
                        Name = dataReader["Name"].ToString(),
                        Document = dataReader["Document"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyUpdateDate"]),
                        CompanyAddresses = new List<CompanyAddressDTO>()
                    };

                    companies.Add(company);
                }

                var addressId = int.Parse(dataReader["CompanyAddressId"].ToString());
                var address = company.CompanyAddresses.FirstOrDefault(a => a.Id == addressId);

                if (address == null)
                {
                    address = new CompanyAddressDTO
                    {
                        Id = addressId,
                        CompanyId = company.Id,
                        Street = dataReader["Street"].ToString(),
                        Neighborhood = dataReader["Neighborhood"].ToString(),
                        City = dataReader["City"].ToString(),
                        PostalCode = dataReader["PostalCode"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyAddressCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyAddressUpdateDate"]),
                        CompanyTelephones = new List<CompanyTelephoneDTO>()
                    };

                    company.CompanyAddresses.Add(address);
                }

                var telephoneId = int.Parse(dataReader["CompanyTelephoneId"].ToString());
                var telephone = address.CompanyTelephones.FirstOrDefault(t => t.Id == telephoneId);

                if (telephone == null)
                {
                    telephone = new CompanyTelephoneDTO
                    {
                        Id = telephoneId,
                        CompanyAddress = address.Id,
                        PhoneNumber = dataReader["PhoneNumber"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyTelephoneCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyTelephoneUpdateDate"])
                    };

                    address.CompanyTelephones.Add(telephone);
                }
            }

            conexao.Close();
            return companies;
        }


        public void InsertCompany(CompanyDTO company)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var queryCompany = @"INSERT INTO Company (Name, Document, CreateDate, UpdateDate) VALUES (@name, @document, @createDate, @updateDate);";
            var comandoCompany = new MySqlCommand(queryCompany, conexao);
            comandoCompany.Parameters.AddWithValue("@name", company.Name);
            comandoCompany.Parameters.AddWithValue("@document", company.Document);
            comandoCompany.Parameters.AddWithValue("@createDate", company.CreateDate);
            comandoCompany.Parameters.AddWithValue("@updateDate", company.UpdateDate);
            comandoCompany.ExecuteNonQuery();

            var companyId = (int)comandoCompany.LastInsertedId;

            foreach (var address in company.CompanyAddresses)
            {
                var queryAddress = @"INSERT INTO CompanyAddress (CompanyId, Street, Neighborhood, City, PostalCode, Country, CreateDate, UpdateDate) VALUES (@companyId, @street, @neighborhood, @city, @postalCode, @country, @createDate, @updateDate);";
                var comandoAddress = new MySqlCommand(queryAddress, conexao);
                comandoAddress.Parameters.AddWithValue("@companyId", companyId);
                comandoAddress.Parameters.AddWithValue("@street", address.Street);
                comandoAddress.Parameters.AddWithValue("@neighborhood", address.Neighborhood);
                comandoAddress.Parameters.AddWithValue("@city", address.City);
                comandoAddress.Parameters.AddWithValue("@postalCode", address.PostalCode);
                comandoAddress.Parameters.AddWithValue("@country", address.Country);
                comandoAddress.Parameters.AddWithValue("@createDate", address.CreateDate);
                comandoAddress.Parameters.AddWithValue("@updateDate", address.UpdateDate);
                comandoAddress.ExecuteNonQuery();

                var addressId = (int)comandoAddress.LastInsertedId;

                foreach (var telephone in address.CompanyTelephones)
                {
                    var queryTelephone = @"INSERT INTO CompanyTelephone (CompanyAddress, PhoneNumber, CreateDate, UpdateDate) VALUES (@addressId, @phoneNumber, @createDate, @updateDate);";
                    var comandoTelephone = new MySqlCommand(queryTelephone, conexao);
                    comandoTelephone.Parameters.AddWithValue("@addressId", addressId);
                    comandoTelephone.Parameters.AddWithValue("@phoneNumber", telephone.PhoneNumber);
                    comandoTelephone.Parameters.AddWithValue("@createDate", telephone.CreateDate);
                    comandoTelephone.Parameters.AddWithValue("@updateDate", telephone.UpdateDate);
                    comandoTelephone.ExecuteNonQuery();
                }
            }

            conexao.Close();
        }

        public void UpdateCompany(CompanyDTO company)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var queryCompany = @"UPDATE Company SET Name = @name, Document = @document, UpdateDate = @updateDate WHERE Id = @id;";
            var comandoCompany = new MySqlCommand(queryCompany, conexao);
            comandoCompany.Parameters.AddWithValue("@id", company.Id);
            comandoCompany.Parameters.AddWithValue("@name", company.Name);
            comandoCompany.Parameters.AddWithValue("@document", company.Document);
            comandoCompany.Parameters.AddWithValue("@updateDate", company.UpdateDate);
            comandoCompany.ExecuteNonQuery();

            var existingAddressIds = new List<int>();
            foreach (var address in company.CompanyAddresses)
            {
                existingAddressIds.Add(address.Id);
            }

            var addressIdsToDelete = new List<int>();
            var queryExistingAddresses = @"SELECT Id FROM CompanyAddress WHERE CompanyId = @companyId;";
            var comandoExistingAddresses = new MySqlCommand(queryExistingAddresses, conexao);
            comandoExistingAddresses.Parameters.AddWithValue("@companyId", company.Id);
            var dataReader = comandoExistingAddresses.ExecuteReader();
            while (dataReader.Read())
            {
                var addressId = int.Parse(dataReader["Id"].ToString());
                if (!existingAddressIds.Contains(addressId))
                {
                    addressIdsToDelete.Add(addressId);
                }
            }
            dataReader.Close();

            foreach (var addressId in addressIdsToDelete)
            {
                var queryDeleteTelephones = @"DELETE FROM CompanyTelephone WHERE CompanyAddress = @addressId;";
                var comandoDeleteTelephones = new MySqlCommand(queryDeleteTelephones, conexao);
                comandoDeleteTelephones.Parameters.AddWithValue("@addressId", addressId);
                comandoDeleteTelephones.ExecuteNonQuery();

                var queryDeleteAddress = @"DELETE FROM CompanyAddress WHERE Id = @addressId;";
                var comandoDeleteAddress = new MySqlCommand(queryDeleteAddress, conexao);
                comandoDeleteAddress.Parameters.AddWithValue("@addressId", addressId);
                comandoDeleteAddress.ExecuteNonQuery();
            }

            foreach (var address in company.CompanyAddresses)
            {
                var queryAddress = string.Empty;
                if (address.Id == 0)
                {
                    queryAddress = @"INSERT INTO CompanyAddress (CompanyId, Street, Neighborhood, City, PostalCode, Country, CreateDate, UpdateDate) VALUES (@companyId, @street, @neighborhood, @city, @postalCode, @country, @createDate, @updateDate);";
                }
                else
                {
                    queryAddress = @"UPDATE CompanyAddress SET Street = @street, Neighborhood = @neighborhood, City = @city, PostalCode = @postalCode, Country = @country, UpdateDate = @updateDate WHERE Id = @addressId;";
                }
                var comandoAddress = new MySqlCommand(queryAddress, conexao);
                comandoAddress.Parameters.AddWithValue("@addressId", address.Id);
                comandoAddress.Parameters.AddWithValue("@companyId", company.Id);
                comandoAddress.Parameters.AddWithValue("@street", address.Street);
                comandoAddress.Parameters.AddWithValue("@neighborhood", address.Neighborhood);
                comandoAddress.Parameters.AddWithValue("@city", address.City);
                comandoAddress.Parameters.AddWithValue("@postalCode", address.PostalCode);
                comandoAddress.Parameters.AddWithValue("@country", address.Country);
                comandoAddress.Parameters.AddWithValue("@createDate", address.CreateDate);
                comandoAddress.Parameters.AddWithValue("@updateDate", address.UpdateDate);
                comandoAddress.ExecuteNonQuery();

                if (address.Id == 0)
                {
                    address.Id = (int)comandoAddress.LastInsertedId;
                }

                foreach (var telephone in address.CompanyTelephones)
                {
                    string queryTelephone;
                    if (telephone.Id == 0)
                    {
                        queryTelephone = @"INSERT INTO CompanyTelephone (CompanyAddress, PhoneNumber, CreateDate, UpdateDate) VALUES (@addressId, @phoneNumber, @createDate, @updateDate);";
                    }
                    else
                    {
                        queryTelephone = @"UPDATE CompanyTelephone SET PhoneNumber = @phoneNumber, UpdateDate = @updateDate WHERE Id = @telephoneId;";
                    }
                    var comandoTelephone = new MySqlCommand(queryTelephone, conexao);
                    comandoTelephone.Parameters.AddWithValue("@telephoneId", telephone.Id);
                    comandoTelephone.Parameters.AddWithValue("@addressId", address.Id);
                    comandoTelephone.Parameters.AddWithValue("@phoneNumber", telephone.PhoneNumber);
                    comandoTelephone.Parameters.AddWithValue("@createDate", telephone.CreateDate);
                    comandoTelephone.Parameters.AddWithValue("@updateDate", telephone.UpdateDate);
                    comandoTelephone.ExecuteNonQuery();

                    if (telephone.Id == 0)
                    {
                        telephone.Id = (int)comandoTelephone.LastInsertedId;
                    }
                }
            }

            conexao.Close();
        }
    }
}
