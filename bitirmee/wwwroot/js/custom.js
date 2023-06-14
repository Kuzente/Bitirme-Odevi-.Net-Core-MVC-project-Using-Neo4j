/* 

1. Add your custom JavaScript code below
2. Place the this code in your template:

  

*/
$(document).ready(function () {
    var table = $('#datatable').DataTable({
        buttons: [{
            extend: 'print',
            title: 'Test Data export',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'pdf',
            title: 'Test Data export',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'excel',
            title: 'Test Data export',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'csv',
            title: 'Test Data export',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'copy',
            title: 'Test Data export',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }]
    });
    table.buttons().container().appendTo('#export_buttons');
    $("#export_buttons .btn").removeClass('btn-secondary').addClass('btn-light');
});