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
      const response = await fetch("https://paytollcard-2b6b0c89816c.herokuapp.com/api/Sql/execute", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ query: queryInput }),
      });
  
      if (!response.ok) {
        const errorData = await response.json();
        alert(`Error: ${errorData}`);
        return;
      }
  
      const data = await response.json();
  
      if (data.length > 0) {
        const headers = Object.keys(data[0]);
        const headerRow = headers.map((header) => `<th>${header}</th>`).join("");
        tableHead.innerHTML = `<tr>${headerRow}</tr>`;
  
        data.forEach((row) => {
          const rowHtml = headers.map((header) => `<td>${row[header] || ""}</td>`).join("");
          tableBody.innerHTML += `<tr>${rowHtml}</tr>`;
        });
      } else {
        tableBody.innerHTML = "<tr><td colspan='100%'>No se encontraron resultados.</td></tr>";
      }
    } catch (error) {
      alert("Error al ejecutar la consulta. Revisa la consola para más detalles.");
      console.error(error);
    }
  });
  