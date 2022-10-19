using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCRUD.Data;
using WebApiCRUD.DTOs;

namespace WebApiCRUD.Controllers
{
    [RoutePrefix("api/emp")]
    public class EmployeesController : ApiController
    {
        private DatabaseContext _databaseContext = new DatabaseContext();

        [HttpGet]
        [Route("")]
        public IEnumerable<Employee> Get()
        {
            //throw new Exception("maneul bir hata ile karşılaşıldı.");
            return _databaseContext.Employees.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get([FromBody] int id)
        {
            var result = _databaseContext.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (result != null)
            {
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    Id = result.EmployeeID,
                    FirstName = result.FirstName,
                    LastName = result.LastName
                };
                return Request.CreateResponse(HttpStatusCode.OK, employeeDto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"id : {id} bulunamadı.");
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            _databaseContext.Employees.Add(employee);
            if (_databaseContext.SaveChanges() > 0)
            {
                EmployeeDto employeeDto = new EmployeeDto()
                {
                    Id = employee.EmployeeID,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName
                };
                return Request.CreateResponse(HttpStatusCode.Created, employeeDto);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri ekleme işlemi yapılamadı.");
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody] Employee model)
        {
            var employee = _databaseContext.Employees.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.City = model.City;
                employee.Country = model.Country;
                employee.PostalCode = model.PostalCode;

                if (_databaseContext.SaveChanges() > 0)
                {
                    EmployeeDto employeeDto = new EmployeeDto()
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Id = employee.EmployeeID
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, employeeDto);
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri güncelleme işlemi yapılamadı.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veri bulunamadı.");
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromBody] int id)
        {
            var employee = _databaseContext.Employees.FirstOrDefault(x => x.EmployeeID == id);
            string employeeName = employee.FirstName + " " + employee.LastName;
            if (employee != null)
            {
                _databaseContext.Employees.Remove(employee);
                if (_databaseContext.SaveChanges() > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, $"{employeeName} silindi.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri silme işlemi yapılamadı.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Veri bulunamadı.");
        }
    }
}
