using Microsoft.AspNetCore.Mvc;
using PayTollCardApi.Core.Entities;
using PayTollCardApi.Core.Services;
using System.Data;
using System.Text.RegularExpressions;

namespace PayTollCardApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SqlController : ControllerBase
    {
        private static readonly Regex ForbiddenSqlPattern = new(
            @"\b(insert|update|delete|drop|alter|create|truncate|grant|revoke|copy|call|do)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly SqlService _sqlService;

        public SqlController(SqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpPost("execute")]
        public IActionResult Execute([FromBody] SqlQueryModel request)
        {
            if (string.IsNullOrWhiteSpace(request?.Query))
            {
                return BadRequest(new { Message = "La consulta SQL es obligatoria." });
            }

            var query = request.Query.Trim();
            if (query.Contains(';'))
            {
                return BadRequest(new { Message = "Solo se permite una consulta por solicitud." });
            }

            if (!query.StartsWith("select", StringComparison.OrdinalIgnoreCase) &&
                !query.StartsWith("with", StringComparison.OrdinalIgnoreCase) &&
                !query.StartsWith("explain", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { Message = "Solo se permiten consultas de lectura." });
            }

            if (ForbiddenSqlPattern.IsMatch(query))
            {
                return BadRequest(new { Message = "La consulta contiene operaciones no permitidas." });
            }

            try
            {
                var resultTable = _sqlService.ExecuteQuery(query);
                var rows = ConvertToRows(resultTable);
                return Ok(rows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al ejecutar la consulta: {ex.Message}" });
            }
        }

        private static List<Dictionary<string, object?>> ConvertToRows(DataTable table)
        {
            var rows = new List<Dictionary<string, object?>>();

            foreach (DataRow dataRow in table.Rows)
            {
                var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                foreach (DataColumn column in table.Columns)
                {
                    row[column.ColumnName] = dataRow[column] == DBNull.Value ? null : dataRow[column];
                }

                rows.Add(row);
            }

            return rows;
        }
    }
}
