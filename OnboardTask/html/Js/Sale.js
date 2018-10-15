//Load Data function  
function loadData() {
    $.ajax({
        url: "../Sales/Index",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $('#ListData').html(result);
            alert("data Updated");
            
        },
        error: function (errormessage) {
            //debugger;
            $('#ListData').html(errormessage.responseText);
            //alert(errormessage.responseText);
        }
    });
}





//Add Data Function
function Add() {
    debugger;
    var res = validate();
    if (!res) {
        return false;
    }
    var objsales = {        
        ProductId: $('#ddlProduct').val(),
        CustomerId: $('#ddlCustomer').val(),
        StoreId: $('#ddlStores').val(),
        DateSold: $('#txtDate').val()
    };
      $.ajax({
        url: "/Sales/Add",
        data: JSON.stringify(objsales),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
          success: function (result) {
              
            $('.modal-backdrop.in').remove();
              loadData();
              $('#myModal').modal('hide');
              clearTextBox(); 
          },

        error: function (errormessage) {
            alert(errormessage.responseText);
         }
       });
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');    

    $.ajax({
        url: "../Sales/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            debugger;
            $('#Id').val(result[0].Id);
            $('#ddlProduct').val(result[0].ProductId);
            $('#ddlCustomer').val(result[0].CustomerId);
            $('#ddlStores').val(result[0].StoreId);
            $('#txtDate').val(converttimestamp(result[0].DateSold));
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

function converttimestamp(currdate) {
    //var singledate = "/Date(1365715800000)/";
    var singledate = currdate;
    var milisegundos = parseInt(singledate.replace("/Date(", "").replace(")/", ""));
    var newDate = new Date(milisegundos).toLocaleDateString("en-UE");
    console.log(newDate);
    return newDate;
}

//function for updating employee's record  
function Update() {
    debugger;
    var res = validate();
    if (!res) {
        return false;
    }
    var objsale = {
        Id: $('#Id').val(),
        ProductId: $('#ddlProduct').val(),
        CustomerId: $('#ddlCustomer').val(),
        StoreId: $('#ddlStores').val(),
        DateSold: $('#txtDate').val()
    };
    debugger;
    $.ajax({
        url: "/Sales/Update",
        data: JSON.stringify(objsale),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            
            $('#myModal').modal('hide');
            clearTextBox()
        },
        error: function (errormessage) {
            console.log(result)
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delete(ID) {

    var ans = confirm("Are you sure you want to delete this Record?");    
    if (ans)
        debugger;
        $.ajax({            
            url: "/Sales/DeleteRec/" + ID,
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




//Function for clearing the textboxes  
function clearTextBox() {
    $('#ddlProduct').val("");
    $('#ddlCustomer').val("");
    $('#ddlStores').val("");
    $('#txtDate').val("");
    //$('#EmployeeID').val("");
    //$('#Name').val("");
    //$('#Age').val("");
    //$('#State').val("");
    //$('#Country').val("");
    //$('#btnUpdate').hide();
    //$('#btnAdd').show();
    //$('#Name').css('border-color', 'lightgrey');
    //$('#Age').css('border-color', 'lightgrey');
    //$('#State').css('border-color', 'lightgrey');
    //$('#Country').css('border-color', 'lightgrey');
}


//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#txtDate').val().trim() == "") {
        $('#txtDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#txtDate').css('border-color', 'lightgrey');
    }
    if ( $("ddlProduct").val() == "") {
        $('#ddlProduct').css('border-color', 'Red');
        isValid = false;
       }
    else {
        $('#ddlProduct').css('border-color', 'lightgrey');
      }
    
    return isValid;
}  
