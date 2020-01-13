//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "../Store/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Address + '</td>';
                html += '<td><a href="#" class="btn btn-success" onclick="return getbyID(' + item.Id + ')"><i class="fa fa-edit" ></i>Edit</a>  <a href="#" class="btn btn-danger" onclick="Delele(' + item.Id + ')"><i class="fa fa-trash" ></i>Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var Obj = {
        Name: $('#Name').val(),
        Address: $('#Address').val()
    };
    $.ajax({
        url: "/Store/Add",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('.modal-backdrop.in').remove();
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            $('.modal-backdrop.in').css("opacity", "0");
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Store ID  
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Store/getbyID/" +ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.Id);
            $('#Name').val(result.Name);
            $('#Address').val(result.Address);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var Obj = {
        Id: $('#ID').val(),
        Name: $('#Name').val(),
        Address: $('#Address').val()
    };
    
    $.ajax({
        url: "/Store/Update",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            loadData();
            $('.modal-backdrop.in').css("opacity", "0");
            $('#myModal').modal('hide');
            $('#Name').val("");
            $('#Address').val("");

        },
        error: function (errormessage) {
            $('.modal-backdrop.in').css("opacity", "0");
            alert(errormessage.responseText);
        }
    });
}

//function for deleting Store's record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Store/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#Name').val("");
    $('#Address').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
}

//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }

    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }

    return isValid;
}

