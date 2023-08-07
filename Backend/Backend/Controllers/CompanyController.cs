using Backend.DAO;
using Backend.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDAO _companyDAO;
        public CompanyController(CompanyDAO companyDAO)
        {
            _companyDAO = companyDAO;
        }

        [HttpGet("{id}")]
        public IActionResult ListCompany(int id)
        {
            var company = _companyDAO.ListCompany(id);

            if (company == null) { return NotFound(); }

            return Ok(company);
        }

        [HttpGet]
        public IActionResult ListAllCompanies()
        {
            var companies = _companyDAO.ListAllCompanies();
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult InsertCompany([FromBody] CompanyDTO company)
        {
            if (company == null)
            {
                return BadRequest("Dados da empresa não foram fornecidos.");
            }

            _companyDAO.InsertCompany(company);

            return Ok("Empresa cadastrada com sucesso!");
        }

        [HttpPut]
        public IActionResult UpdateCompany([FromBody] CompanyDTO company)
        {
            if (company == null)
            {
                return BadRequest("Dados da empresa não foram fornecidos.");
            }

            var existingCompany = _companyDAO.ListCompany(company.Id);
            if (existingCompany == null)
            {
                return NotFound();
            }

            _companyDAO.UpdateCompany(company);

            return Ok("Empresa atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var existingCompany = _companyDAO.ListCompany(id);
            if (existingCompany == null)
            {
                return NotFound();
            }

            _companyDAO.DeleteCompany(id);

            return Ok("Empresa deletada com sucesso!");
        }

        [HttpDelete("addresses/{addressId}")]
        public IActionResult DeleteAddress(int addressId)
        {
            _companyDAO.DeleteAddress(addressId);

            return Ok("Endereço deletado com sucesso!");
        }

        [HttpDelete("telephones/{telephoneId}")]
        public IActionResult DeleteTelephone(int telephoneId)
        {
            _companyDAO.DeleteTelephone(telephoneId);

            return Ok("Telefone deletado com sucesso!");
        }
    }
}
