
$(document).ready(function () {
   $("#btnUpdate").hide();
   $("#btnSave").show();
   var IsEdit = false;
   $("#MyModal").modal({
      backdrop: 'static',
      keyboard: false
   });
   load(); 
   $("#btnModal").click(function () {
      $("#MyModal").modal('show')
   })
})
function Save() {
   var obj = new Object();
   obj.id = 0,
      obj.Name = $("#txtName").val(),
      obj.Type = $("#txtInstituteType").val(), 
      obj.Address = $("#txtAddress").val(),
      obj.Description = $("#txtDescription").val(),

      $.ajax({
         url: "https://localhost:7065/api/Institutes/SaveInstitute",
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
      $("#txtInstituteType").val(''),
      $("#txtAddress").val(''),
      $("#txtDescription").val(''), 
      $("#txtId").val('')
}
function Close() {
   $("#MyModal").modal('hide');
}
function load() {
   $.ajax({
      url: "https://localhost:7065/api/Institutes/GetInstitute",
      type: "JSON",
      method: "GET",
      success: function (result) {
         console.log("Get All = ", result)
         $("#tble tbody").empty();
         $.each(result, function (i, v) {
            console.log("Data value = ", v)
            var html = "<tr><td>" + v.name + "</td>" +
               " <td>" + v.type + "</td>" +
               "<td>" + v.address + "</td>" +
               "<td>" + v.description + "</td>" +
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
      url: "https://localhost:7065/api/Institutes/GetByID?Id=" + id,
      type: "JSON",
      method: "GET", 
      contentType: "application/json",
      success: function (result) { 
         console.log("Get by ID ", result);
         $("#exampleModalLabel").html("Update Unit Information");
         IsEdit = true;
            $("#txtName").val(result[0].name),
            $("#txtInstituteType").val(result[0].type),
            $("#txtAddress").val(result[0].address),
            $("#txtDescription").val(result[0].description),
            $("#txtId").val(result[0].id),
            $("#MyModal").modal('show')
      },
      error: function (er) {
         console.log(er)
      }
   })
}
function Update() {
   var url = "https://localhost:7065/api/Institutes/UpdateInstitute"
   var updateData = new Object()
      updateData.Id = $("#txtId").val(),
      updateData.Name = $("#txtName").val(),
      updateData.Type = $("#txtInstituteType").val(),
      updateData.Address = $("#txtAddress").val(),
      updateData.Description = $("#txtDescription").val()
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
   var url = "https://localhost:7065/api/Institutes/Delete?id=" + id;
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


function AddNew() {
   $('#exampleModalLabel').text('Create New Institute');
   $('#btnSave').removeClass('btn btn-ghost-info active w-10'); ;
   $('#txtId').html(0);
   $('#txtName').val('');
   $('#txtInstituteType').val('');
   $('#txtAddress').val('');
   $('#txtDescription').val('');
   $('#MyModal').modal('toggle');
   $('#btnSave').text('Save');
   $('#btnSave').addClass('btn btn-ghost-primary active w-10');
}