/// <reference path="jtable/jquery.jtable.js" />
$(document).ready(function () {
    $('#TablaSecciones').jtable({
        title: 'Secciones del Centro Educativo',
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
                title: 'Nombre de la Seccion',
                width: '15%'
            }
        }
    });
    $('#TablaSecciones').jtable('load');

    $('#TablaMaterias').jtable({
        title: 'Materias del Centro Educativo',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Nombre ASC',

        actions: {
            listAction: '/Materias/Get',
            createAction: '/Materias/Create',
            updateAction: '/Materias/Edit',
            deleteAction: '/Materias/Delete'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Nombre: {
                title: 'Materia',
                width: '15%'
            }
        }
    });
    $('#TablaMaterias').jtable('load');


    $('#TablaGrupos').jtable({
        title: 'Grupos del Centro Educativo',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Materia ASC',

        actions: {
            listAction: '/Grupos/Get',
            createAction: '/Grupos/Create',
            updateAction: '/Grupos/Edit',
            deleteAction: '/Grupos/Delete'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Seccion: {
                title: 'Seccion',
                width: '15%'
            },
            Materia: {
                title: 'Materia',
                width: '15%'
            }
        }
    });
    $('#TablaGrupos').jtable('load');
});
