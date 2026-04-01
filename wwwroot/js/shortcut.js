const storedApiBaseUrl = (localStorage.getItem("PAYTOLL_API_BASE_URL") || "").trim();
const API_BASE_URL = storedApiBaseUrl || window.location.origin;

async function readResponsePayload(response) {
  const contentType = response.headers.get("content-type") || "";
  if (contentType.includes("application/json")) {
    return response.json();
  }

  const rawText = await response.text();
  return { rawText };
}

function getErrorMessage(status, payload) {
  if (payload?.message) {
    return payload.message;
  }

  if (payload?.Message) {
    return payload.Message;
  }

  if (status === 404) {
    return "No se encontró /api/Sql/execute. Verifica la URL del API o configura PAYTOLL_API_BASE_URL en localStorage.";
  }

  if (payload?.rawText && payload.rawText.includes("<!DOCTYPE")) {
    return "El servidor respondió HTML en lugar de JSON. El frontend está apuntando a un host sin backend API.";
  }

  return "No fue posible ejecutar la consulta.";
}

document.getElementById("execute-btn").addEventListener("click", async () => {
  const queryInput = document.getElementById("query-input").value.trim();
  const tableHead = document.getElementById("table-head");
  const tableBody = document.getElementById("table-body");

  tableHead.innerHTML = "";
  tableBody.innerHTML = "";

  if (!queryInput) {
    alert("Por favor, ingrese una consulta SQL.");
    return;
  }

  try {
    const endpoint = `${API_BASE_URL}/api/Sql/execute`;
    const response = await fetch(endpoint, {
      method: "POST",
      headers: { "Content-Type": "application/json", "Accept": "application/json" },
      body: JSON.stringify({ query: queryInput }),
    });

    const responseData = await readResponsePayload(response);

    if (!response.ok) {
      alert(`Error: ${getErrorMessage(response.status, responseData)}`);
      return;
    }

    const data = Array.isArray(responseData) ? responseData : [];
    if (data.length > 0) {
      const headers = Object.keys(data[0]);
      const headerRow = headers.map((header) => `<th>${header}</th>`).join("");
      tableHead.innerHTML = `<tr>${headerRow}</tr>`;

      data.forEach((row) => {
        const rowHtml = headers.map((header) => `<td>${row[header] || ""}</td>`).join("");
        tableBody.innerHTML += `<tr>${rowHtml}</tr>`;
      });
      return;
    }

    tableBody.innerHTML = "<tr><td colspan='100%'>No se encontraron resultados.</td></tr>";
  } catch (error) {
    console.error("Error:", error);
    alert("Error al ejecutar la consulta. " + (error?.message || ""));
  }
});
  
