﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PayTollCardApi.Models;
using PayTollCardApi.Services;
using System.Data;

namespace PayTollCardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SqlController : ControllerBase
    {
        private readonly SqlService _sqlService;

        public SqlController(SqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpPost("execute")]
        public IActionResult ExecuteSql([FromBody] SqlQueryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Query))
            {
                return BadRequest("La consulta SQL no puede estar vacía.");
            }

            var lowerQuery = model.Query.ToLower();
            if (lowerQuery.Contains("delete") || lowerQuery.Contains("drop") || lowerQuery.Contains("truncate") || lowerQuery.Contains("update") || lowerQuery.Contains("insert"))
            {
                return BadRequest("Operaciones de modificación no permitidas.");
            }

            try
            {
                var dataTable = _sqlService.ExecuteQuery(model.Query);

                var result = new List<Dictionary<string, object>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        dict[col.ColumnName] = row[col];
                    }
                    result.Add(dict);
                }

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Error al ejecutar la consulta SQL: {ex.Message}");
            }
        }
    }
}
