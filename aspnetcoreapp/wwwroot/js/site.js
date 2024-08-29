// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function sortTable(columnIndex) {
    const table = document.getElementById("NbrSonsParArtiste");
    const rows = Array.from(table.getElementsByTagName("tbody")[0].getElementsByTagName("tr"));
    const isAscending = table.getAttribute("data-sort-order") === "asc";

    rows.sort((rowA, rowB) => {
        const cellA = rowA.getElementsByTagName("td")[columnIndex].innerText;
        const cellB = rowB.getElementsByTagName("td")[columnIndex].innerText;

        if (isNaN(cellA) || isNaN(cellB)) {
            return isAscending 
                ? cellA.localeCompare(cellB) 
                : cellB.localeCompare(cellA);
        } else {
            return isAscending 
                ? cellA - cellB 
                : cellB - cellA;
        }
    });

    rows.forEach(row => table.getElementsByTagName("tbody")[0].appendChild(row));
    table.setAttribute("data-sort-order", isAscending ? "desc" : "asc");
}