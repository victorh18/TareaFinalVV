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

    $('#TablaPeriodos').jtable({
        title: 'Periodos',
        paging: true,
        pageSize: 5,
        sorting: true,
        defaultSorting: 'Codigo ASC',

        actions: {
            listAction: '/Periodos/Get',
            createAction: '/Periodos/Create',
            updateAction: '/Periodos/Edit',
            deleteAction: '/Periodos/Delete'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            Codigo: {
                title: 'Codigo',
                width: '15%',
                create: false,
                edit: false
            },
            Anio: {
                title: 'Año',
                width: '15%',
                list: false
            },
            Cuatrimestre: {
                title: 'Cuatrimestre',
                width: '15%',
                options: { 1:'C1', 2:'C2', 3:'C3'},
                list: false
            }
        }
    });
    $('#TablaPeriodos').jtable('load');
    
    $('#TablaGrupos').jtable({
        title: 'Grupos del Centro Educativo',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'MateriaId ASC',

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
            Codigo: {
                title: 'Codigo',
                width: '15%'
            },
            SeccionId: {
                title: 'Seccion',
                width: '15%',
                options: '/seccion/getsecciones'
            },
            MateriaId: {
                title: 'Materia',
                width: '15%',
                options: '/materias/getmaterias'
            },
            PeriodoId: {
                title: 'Periodo',
                width: '15%',
                options: '/periodos/getperiodos'
            }
        }
    });
    $('#TablaGrupos').jtable('load');

    $('#TablaSeleccion').jtable({
        title: 'Seleccion de Materias',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Materia ASC',

        actions: {
            listAction: '/Grupos/Seleccion/Get',
            updateAction: '/Grupos/Seleccion/Edit'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            EstadoSeleccion: {
                title: 'Estado de Seleccion',
                width: '15%',
                options: { 0: 'No Registrado', 1: 'Registrado'},
                edit: true
            },
            Grupo: {
                title: 'Grupo',
                width: '15%',
                edit: false
            },
            Seccion: {
                title: 'Seccion',
                width: '15%',
                edit: false
            },
            Materia: {
                title: 'Materia',
                width: '15%',
                edit: false
            },
            Periodo: {
                title: 'Periodo',
                width: '15%',
                edit: false
            }
        }
    });
    $('#TablaSeleccion').jtable('load');

    $('#TablaAsignacionProfesores').jtable({
        title: 'Asignacion de Profesores',
        paging: true,
        pageSize: 10,
        sorting: true,
        defaultSorting: 'Materia ASC',

        actions: {
            listAction: '/Grupos/Asignacion/Get',
            updateAction: '/Grupos/Asignacion/Edit'
        },
        fields: {
            Id: {
                key: true,
                list: false
            },
            EstadoSeleccion: {
                title: 'Profesor',
                width: '15%',
                options: '/Account/Get/Profesores',
                edit: true
            },
            Grupo: {
                title: 'Grupo',
                width: '15%',
                edit: false
            },
            Seccion: {
                title: 'Seccion',
                width: '15%',
                edit: false
            },
            Materia: {
                title: 'Materia',
                width: '15%',
                edit: false
            },
            Periodo: {
                title: 'Periodo',
                width: '15%',
                edit: false
            }
        }
    });
    $('#TablaAsignacionProfesores').jtable('load');


    $('#TablaAsignarCalificaciones').jtable({
        title: 'Asignar Calificaciones',
        paging: true,
        sorting: true,
        //defaultSorting: 'Name ASC',
        actions: {
            listAction: '/Notas/Estudiantes/Get',
            updateAction: '/Notas/Estudiantes/Edit'
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            Nombre: {
                title: 'Nombre',
                edit: false
            },
            GrupoMateria: {
                title: 'Grupo',
                edit: false
            },
            PrimerParcial: {
                title: 'Primer Parcial (35%)',
                edit: true
            },
            SegundoParcial: {
                title: 'Segundo Parcial (35%)',
                edit: true
            },
            ParcialFinal: {
                title: 'Parcial Final (30%)',
                edit: true
            },
            NotaTotal: {
                title: 'Nota Total',
                width: '1%',
                edit: false
            },
            Literal: {
                title: 'Literal',
                width: '2%',
                edit: false
            }
        }
    });
    $('#TablaAsignarCalificaciones').jtable('load');

    //Re-load records when user click 'load records' button.
    $('#BuscarNotasProfesor').click(function (e) {
        e.preventDefault();
        $('#TablaAsignarCalificaciones').jtable('load', {
            Nombre: $('#NombreSearch').val(),
            GrupoMateria: $('#MateriaIdSearch').val()
        });
    });

    //Carga Estudiantes para asignar Notas
    $.ajax({
        url: '/Materias/GetMaterias/Profesor',
        type: 'get',
        success: function (data) {
            var Materias = data.Options;
            $.each(Materias, function (item) {
                //Use the Option() constructor to create a new HTMLOptionElement.
                var option = new Option(Materias[item].DisplayText, Materias[item].Value);
                //Convert the HTMLOptionElement into a JQuery object that can be used with the append method.
                $(option).html(Materias[item].DisplayText, Materias[item].Value);
                //Append the option to our Select element.
                $("#MateriaIdSearch").append(option);
            });
        }
    });
    /*
     */

    $('#TablaConsultarCalificaciones').jtable({
        title: 'Consultar Calificaciones',
        paging: true,
        sorting: true,
        //defaultSorting: 'Name ASC',
        actions: {
            listAction: '/Notas/Get'
        },
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false
            },
            GrupoMateria: {
                title: 'Materia',
                edit: false
            },
            Nombre: {
                title: 'Cuatrimestre',
                edit: false
            },

            PrimerParcial: {
                title: 'Primer Parcial (35%)',
                edit: true
            },
            SegundoParcial: {
                title: 'Segundo Parcial (35%)',
                edit: true
            },
            ParcialFinal: {
                title: 'Parcial Final (30%)',
                edit: true
            },
            NotaTotal: {
                title: 'Nota Total',
                width: '1%',
                edit: false
            },
            Literal: {
                title: 'Literal',
                width: '2%',
                edit: false
            }
        }
    });
    $('#TablaConsultarCalificaciones').jtable('load');

    //Re-load records when user click 'load records' button.
    $('#BuscarNotasEstudiante').click(function (e) {
        e.preventDefault();
        $('#TablaConsultarCalificaciones').jtable('load', {
            Nombre: $('#PeriodoIdSearch').val()
        });
    });

    //Carga Periodos para la consulta de las notas por parte de los estudiantes
    $.ajax({
        url: '/Periodos/GetPeriodos/Estudiante',
        type: 'get',
        success: function (data) {
            var Materias = data.Options;
            $.each(Materias, function (item) {
                //Use the Option() constructor to create a new HTMLOptionElement.
                var option = new Option(Materias[item].DisplayText, Materias[item].Value);
                //Convert the HTMLOptionElement into a JQuery object that can be used with the append method.
                $(option).html(Materias[item].DisplayText, Materias[item].Value);
                //Append the option to our Select element.
                $("#PeriodoIdSearch").append(option);
            });
        }
    });

});
