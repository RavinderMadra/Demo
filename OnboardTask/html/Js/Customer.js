//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "../Customer/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td style="display:none">' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Age + '</td>';
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
    var pdtObj = {
        EmployeeID: $('#CustomerID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        Address: $('#Address').val()
    };
    $.ajax({
        url: "/Customer/Add",
        data: JSON.stringify(pdtObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            $('.modal-backdrop.in').remove();
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            
               alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function getbyID(EmpID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Customer/GetbyID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CustomerID').val(result.Id);
            $('#Name').val(result.Name);
            $('#Age').val(result.Age);
            $('#Address').val(result.Address);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    
}

//function for updating employee's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#CustomerID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        Address: $('#Address').val()
    };
    $.ajax({
        url: "/Customer/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
                loadData();
            $('#myModal').modal('hide');
            $('#CustomerID').val("");
            $('#Name').val("");
            $('#Age').val("");
            $('#Address').val("");            
        },
        error: function (errormessage) {
              alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Customer/Delete/" + ID,
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
    $('#CustomerID').val("");
    $('#Name').val("");
    $('#Age').val("");
    $('#Address').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
 
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
    if ($('#Age').val().trim() == "") {
        $('#Age').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Age').css('border-color', 'lightgrey');
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

