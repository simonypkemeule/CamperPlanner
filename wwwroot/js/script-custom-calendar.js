var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: true
    });
    InitializeCalendar();
});

var calendar;
function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                locale: 'nl',
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                weekNumbers: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDetail) {
    $("#appointmentInput").modal("show");
}
function onCloseModal() {
    $("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    var requestData = {
        Id: parseInt ($("#AfspraakId").val()),
        Voertuig: $("#VoertuigID").val(),
        BeginDatum: $("#BeginDatum").val()
    };

    $.ajax({
        url: routeURL + "/api/AppointmentApi/SaveCalendarData",
        type: "POST",
        data: JSON.stringify(requestData),
        contentType: "application/json",
        success: function (response) {
            console.log(requestData.Voertuig);
            console.log(requestData.Id);
            console.log(requestData.BeginDatum);
            console.log(response.status);
            if (response.status === 1 || response.status === 2) {
                $.notify(response.message, "success");
                onCloseModal();

            } else {
                $.notify(response.message, "error");
                
            }
        },
        error: function (xhr) {
            $.notify("Error", "Foutje");
        }
    });
}