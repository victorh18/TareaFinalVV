/// <reference path="jtable/jquery.jtable.js" />
$(document).ready(function () {
    $('#TablaSecciones').jtable({
        title: 'Secciones',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Nombre ASC',

        actions: {
            listAction: '/Seccion/Get',
            createAction: '/Seccion/Create',
            updateAction: '/Seccion/Edit',
            deleteAction: '/Seccion/Delete'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Nombre: {
                title: 'Seccion',
                width: '15%'
            }
        }
    });
    $('#TablaSecciones').jtable('reload');
});