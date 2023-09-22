
var InstituteList = [];

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
   obj.id = 0,
      obj.Name = $("#txtName").val(),
      obj.Address = $("#txtAddress").val(),
      obj.City = $("#txtCity").val(),
      obj.InstituteID = $("#ddlInstitute").val(),

      $.ajax({
         url: "https://localhost:7065/api/InsBranch/SaveBranch",
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
      $("#txtCity").val(''),
      $("#txtAddress").val(''),
      $("#ddlInstitute").val(''),
      $("#txtId").val('')
}
function Close() {
   $("#MyModal").modal('hide');
}
function load() {
   $.ajax({
      url: "https://localhost:7065/api/InsBranch/GetBranch",
      type: "JSON",
      method: "GET",
      success: function (result) {
         console.log("Get All = ", result)
         $("#tble tbody").empty();
         $.each(result, function (i, v) {
            console.log("Data value = ", v)
            var html = "<tr><td>" + v.insttitueName + "</td>" +
               " <td>" + v.name + "</td>" +
               "<td>" + v.city + "</td>" +
               "<td>" + v.address + "</td>" +
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
      url: "https://localhost:7065/api/InsBranch/GetByID?Id=" + id,
      type: "JSON",
      method: "GET", 
      contentType: "application/json",
      success: function (result) {
         console.log("Get by ID ", result);
         $("#exampleModalLabel").html("Update Branch Information");
         IsEdit = true;
            $("#txtName").val(result[0].name), 
            $("#txtAddress").val(result[0].address),
            $("#txtCity").val(result[0].city),
            $("#ddlInstitute").val(result[0].instituteID),
            $("#txtId").val(result[0].id),
            $("#MyModal").modal('show')
      },
      error: function (er) {
         console.log(er)
      }
   })
}
function Update() {
   var url = "https://localhost:7065/api/InsBranch/UpdateBranch"
   var updateData = new Object()
      updateData.Id = $("#txtId").val(),
      updateData.Name = $("#txtName").val(), 
      updateData.Address = $("#txtAddress").val(),
      updateData.City = $("#txtCity").val(),
      updateData.InstituteID = $("#ddlInstitute").val()
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
   var url = "https://localhost:7065/api/InsBranch/Delete?id=" + id;
   $.ajax({
      url: url,
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      type: "Delete",
      success: function (result) {
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
      url: "https://localhost:7065/api/InsBranch/GetInitialData",
      method: "GET",
      dataType: "json",
      success: function (data) {
         InstituteList = data.institute; 
         console.log("Institute = ", InstituteList); 
         var s = '<option value="-1">Select Institute</option>';
         for (var i = 0; i < InstituteList.length; i++) {
            console.log(data[i])
            s += '<option value="' + InstituteList[i].id + '">' + InstituteList[i].name + '</option>';
         }
         $("#ddlInstitute").html(s); 
      },
      error: function (jqXHR, textStatus, errorThrown) {
         console.log("Error:", textStatus, errorThrown);
      }
   });
}

function AddNew() {
   $('#staticBackdropLabel').text('Create New Institute');
   $('#btnSave').removeClass('btn btn-ghost-info active w-10');
   // $('#spanParentID').html(0);
   $('#txtId').html(0);
   $('#txtName').val('');
   $('#txtCity').val('');
   $('#txtAddress').val('');
   $('#ddlInstitute').val('');
   $('#MyModal').modal('toggle');
   $('#btnSave').text('Save');
   $('#btnSave').addClass('btn btn-ghost-primary active w-10');
}
