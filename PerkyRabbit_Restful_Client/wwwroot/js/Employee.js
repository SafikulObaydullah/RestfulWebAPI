
var BranchList = [];
$(document).ready(function () {
   $("#btnUpdate").hide();
   $("#btnSave").show();
   var IsEdit = false;
   $("#MyModal").modal({
      backdrop: 'static',
      keyboard: false
   });
   load();
   LoadInitalData();
   $("#btnModal").click(function () {
      $("#MyModal").modal('show')
   })
})
function Save() {
   var obj = new Object();
      obj.Id = $('#txtId').val(),
      obj.Name = $("#txtName").val(),
      obj.ContactNumber = $("#txtContactNumber").val(),
      obj.Email = $("#txtEmail").val(),
      obj.BranchID = $("#ddlBranch").val(),
      $.ajax({
         url: "https://localhost:7065/api/Employee/SaveEmployee",
         type: "JSON",
         method: "POST",
         data: JSON.stringify(obj),
         contentType: "application/json",
         success: function (result) {
            toastr.success(result.message, 'Save Successfully');
            $("#MyModal").modal('hide')
            load();
            clearALl();
         },
         error: function (er) {
            console.log(er)
         }
      })
}
function clearALl() {
      $("#txtName").val(''),
      $("#txtEmail").val(''),
      $("#txtContactNumber").val(''),
      $("#ddlBranch").val(''),
      $("#txtId").val('')
}
function Close() {
   $("#MyModal").modal('hide');
}
function load() {
   $.ajax({
      url: "https://localhost:7065/api/Employee/GetEmployee",
      type: "JSON",
      method: "GET",
      success: function (result) { 
         $("#tble tbody").empty();
         $.each(result, function (i, v) { 
            var html = "<tr><td>" + v.branchName + "</td>" +
               " <td>" + v.name + "</td>" +
               "<td>" + v.email + "</td>" +
               "<td>" + v.contactNumber + "</td>" +
               " <td> <button onClick='Edit(" + v.id + ")'>Edit </button></td>" +
               " <td> <button onClick='Delete(" + v.id + ")'>Delete </button></td></tr>";
            $("#tble tbody").append(html)
         })
      },
      error: function (er) {
         console.log(er)
      }
   })
}

function Edit(id) {
   $("#btnUpdate").show();
   $("#btnSave").hide();
   $.ajax({
      url: "https://localhost:7065/api/Employee/GetByID?Id=" + id,
      type: "JSON",
      method: "GET",
      contentType: "application/json",
      success: function (result) { 
         $("#exampleModalLabel").html("Update Branch Information");
         IsEdit = true;
         $("#txtName").val(result[0].name),
            $("#txtContactNumber").val(result[0].contactNumber),
            $("#txtEmail").val(result[0].email),
            $("#ddlBranch").val(result[0].branchID),
            $("#txtId").val(result[0].id),
            $("#MyModal").modal('show')
      },
      error: function (er) {
         console.log(er)
      }
   })
}
function Update() {
   var url = "https://localhost:7065/api/Employee/UpdateEmployee"
   var updateData = new Object()
      updateData.Id = $("#txtId").val(),
      updateData.Name = $("#txtName").val(),
      updateData.ContactNumber = $("#txtContactNumber").val(),
      updateData.Email = $("#txtEmail").val(),
      updateData.BranchID = $("#ddlBranch").val()
   $.ajax({
      url: url,
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "Put",
      data: JSON.stringify(updateData),
      success: function (result) {
         toastr.success(result.message, 'Update Successfully');
         $("#MyModal").modal('hide');
         load();
         clearALl();
         $("#btnUpdate").hide();
         $("#btnSave").show();
         $("#btnSave").text("Save");
      },
      error: function (er) {
         console.log(er.responseText);
      }
   })
}
function Delete(id) {
   var url = "https://localhost:7065/api/Employee/Delete?id=" + id;
   $.ajax({
      url: url,
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "Delete",
      success: function (result) {
         toastr.success(result.message, 'Delete Successfully');
         clearALl();
         load();
      },
      error: function (msg) {
         alert(msg);
      }
   });
}
function LoadInitalData() {
   $.ajax({
      url: "https://localhost:7065/api/Employee/GetInitialData",
      method: "GET",
      dataType: "json",
      success: function (data) {
         BranchList = data.branches; 
         var s = '<option selected value="-1">Select Branch</option>';
         for (var i = 0; i < BranchList.length; i++) { 
            s += '<option value="' + BranchList[i].id + '">' + BranchList[i].name + '</option>';
         }
         $("#ddlBranch").html(s);
      },
      error: function (jqXHR, textStatus, errorThrown) {
         console.log("Error:", textStatus, errorThrown);
      }
   });
}

function AddNew() {
   $('#exampleModalLabel').text('Create New Employee');
   $('#btnSave').removeClass('btn btn-ghost-info active w-10');
   $('#txtId').html(0);
   $('#txtName').val('');
   $('#txtEmail').val('');
   $('#txtContactNumber').val('');
   $('#ddlBranch').val('');
   $('#MyModal').modal('toggle');
   $('#btnSave').text('Save');
   $('#btnSave').addClass('btn btn-ghost-primary active w-10');
}
