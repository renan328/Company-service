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
            var companyDictionary = new Dictionary<int, CompanyDTO>();
            var addressDictionary = new Dictionary<int, CompanyAddressDTO>();

            while (dataReader.Read())
            {
                var companyId = int.Parse(dataReader["CompanyId"].ToString());

                if (!companyDictionary.TryGetValue(companyId, out var company))
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

                    companyDictionary.Add(companyId, company);
                    companies.Add(company);
                }

                var addressId = int.Parse(dataReader["CompanyAddressId"].ToString());
                if (!addressDictionary.TryGetValue(addressId, out var address))
                {
                    address = new CompanyAddressDTO
                    {
                        Id = addressId,
                        CompanyId = companyId,
                        Street = dataReader["Street"].ToString(),
                        Neighborhood = dataReader["Neighborhood"].ToString(),
                        City = dataReader["City"].ToString(),
                        PostalCode = dataReader["PostalCode"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CompanyAddressCreateDate"]),
                        UpdateDate = Convert.ToDateTime(dataReader["CompanyAddressUpdateDate"]),
                        CompanyTelephones = new List<CompanyTelephoneDTO>()
                    };

                    addressDictionary.Add(addressId, address);
                    company.CompanyAddresses.Add(address);
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
            comandoCompany.Parameters.AddWithValue("@createDate", DateTime.Now);
            comandoCompany.Parameters.AddWithValue("@updateDate", DateTime.Now);
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
                comandoAddress.Parameters.AddWithValue("@createDate", DateTime.Now);
                comandoAddress.Parameters.AddWithValue("@updateDate", DateTime.Now);
                comandoAddress.ExecuteNonQuery();

                var addressId = (int)comandoAddress.LastInsertedId;

                foreach (var telephone in address.CompanyTelephones)
                {
                    var queryTelephone = @"INSERT INTO CompanyTelephone (CompanyAddress, PhoneNumber, CreateDate, UpdateDate) VALUES (@addressId, @phoneNumber, @createDate, @updateDate);";
                    var comandoTelephone = new MySqlCommand(queryTelephone, conexao);
                    comandoTelephone.Parameters.AddWithValue("@addressId", addressId);
                    comandoTelephone.Parameters.AddWithValue("@phoneNumber", telephone.PhoneNumber);
                    comandoTelephone.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comandoTelephone.Parameters.AddWithValue("@updateDate", DateTime.Now);
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
            comandoCompany.Parameters.AddWithValue("@updateDate", DateTime.Now);
            comandoCompany.ExecuteNonQuery();

            var queryExistingAddresses = "SELECT Id FROM CompanyAddress WHERE CompanyId = @companyid";
            var comandoExistingAddresses = new MySqlCommand(queryExistingAddresses, conexao);
            comandoExistingAddresses.Parameters.AddWithValue("@companyid", company.Id);
            var dataReaderExistingAddresses = comandoExistingAddresses.ExecuteReader();

            var ExistingAddressesIds = new List<int>();

            while (dataReaderExistingAddresses.Read())
            {
                var idAddress = int.Parse(dataReaderExistingAddresses["Id"].ToString());
                ExistingAddressesIds.Add(idAddress);
            }

            dataReaderExistingAddresses.Close();

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
                    ExistingAddressesIds.Remove(address.Id);
                }
                var comandoAddress = new MySqlCommand(queryAddress, conexao);
                comandoAddress.Parameters.AddWithValue("@addressId", address.Id);
                comandoAddress.Parameters.AddWithValue("@companyId", company.Id);
                comandoAddress.Parameters.AddWithValue("@street", address.Street);
                comandoAddress.Parameters.AddWithValue("@neighborhood", address.Neighborhood);
                comandoAddress.Parameters.AddWithValue("@city", address.City);
                comandoAddress.Parameters.AddWithValue("@postalCode", address.PostalCode);
                comandoAddress.Parameters.AddWithValue("@country", address.Country);
                comandoAddress.Parameters.AddWithValue("@createDate", DateTime.Now);
                comandoAddress.Parameters.AddWithValue("@updateDate", DateTime.Now);
                comandoAddress.ExecuteNonQuery();

                if (address.Id == 0)
                {
                    address.Id = (int)comandoAddress.LastInsertedId;
                }


                var queryExistingTelephones = "SELECT Id FROM CompanyTelephone WHERE CompanyAddress = @companyAddress";
                var comandoExistingTelephones = new MySqlCommand(queryExistingTelephones, conexao);
                comandoExistingTelephones.Parameters.AddWithValue("@companyAddress", address.Id);
                var dataReaderExistingTelephones = comandoExistingTelephones.ExecuteReader();

                var ExistingTelephonesIds = new List<int>();

                while (dataReaderExistingTelephones.Read())
                {
                    var idTelephone = int.Parse(dataReaderExistingTelephones["Id"].ToString());
                    ExistingTelephonesIds.Add(idTelephone);
                }

                dataReaderExistingTelephones.Close();


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
                        ExistingTelephonesIds.Remove(telephone.Id);
                    }
                    var comandoTelephone = new MySqlCommand(queryTelephone, conexao);
                    comandoTelephone.Parameters.AddWithValue("@telephoneId", telephone.Id);
                    comandoTelephone.Parameters.AddWithValue("@addressId", address.Id);
                    comandoTelephone.Parameters.AddWithValue("@phoneNumber", telephone.PhoneNumber);
                    comandoTelephone.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comandoTelephone.Parameters.AddWithValue("@updateDate", DateTime.Now);
                    comandoTelephone.ExecuteNonQuery();
                }

                foreach (var telephoneId in ExistingTelephonesIds)
                {
                    DeleteTelephone(telephoneId);
                }
            }

            foreach (var addressId in ExistingAddressesIds)
            {
                DeleteAddress(addressId);
            }

            conexao.Close();
        }

        public void DeleteCompany(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var queryDeleteTelephones = @"DELETE FROM CompanyTelephone WHERE CompanyAddress IN (SELECT Id FROM CompanyAddress WHERE CompanyId = @companyId);";
            var comandoDeleteTelephones = new MySqlCommand(queryDeleteTelephones, conexao);
            comandoDeleteTelephones.Parameters.AddWithValue("@companyId", id);
            comandoDeleteTelephones.ExecuteNonQuery();

            var queryDeleteAddresses = @"DELETE FROM CompanyAddress WHERE CompanyId = @companyId;";
            var comandoDeleteAddresses = new MySqlCommand(queryDeleteAddresses, conexao);
            comandoDeleteAddresses.Parameters.AddWithValue("@companyId", id);
            comandoDeleteAddresses.ExecuteNonQuery();

            var queryDeleteCompany = @"DELETE FROM Company WHERE Id = @id;";
            var comandoDeleteCompany = new MySqlCommand(queryDeleteCompany, conexao);
            comandoDeleteCompany.Parameters.AddWithValue("@id", id);
            comandoDeleteCompany.ExecuteNonQuery();

            conexao.Close();
        }
        public void DeleteAddress(int addressId)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var queryDeleteTelephones = @"DELETE FROM CompanyTelephone WHERE CompanyAddress = @addressId;";
            var comandoDeleteTelephones = new MySqlCommand(queryDeleteTelephones, conexao);
            comandoDeleteTelephones.Parameters.AddWithValue("@addressId", addressId);
            comandoDeleteTelephones.ExecuteNonQuery();

            var queryDeleteAddress = @"DELETE FROM CompanyAddress WHERE Id = @addressId;";
            var comandoDeleteAddress = new MySqlCommand(queryDeleteAddress, conexao);
            comandoDeleteAddress.Parameters.AddWithValue("@addressId", addressId);
            comandoDeleteAddress.ExecuteNonQuery();

            conexao.Close();
        }

        public void DeleteTelephone(int telephoneId)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var queryDeleteTelephone = @"DELETE FROM CompanyTelephone WHERE Id = @telephoneId;";
            var comandoDeleteTelephone = new MySqlCommand(queryDeleteTelephone, conexao);
            comandoDeleteTelephone.Parameters.AddWithValue("@telephoneId", telephoneId);
            comandoDeleteTelephone.ExecuteNonQuery();

            conexao.Close();
        }

    }
}
