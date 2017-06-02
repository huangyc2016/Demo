$(function () {
    $("#j-ControllerId").change(function () { change(); });
    $("#btnToController").click(function () { toController();});
    $("#btnAdd").click(function () { add(); });
    $("#btnSave").click(function () { save(); });
    $("#btnDelete").click(function () { deleteMulti(); });
    $("#checkAll").click(function () { checkAll(this) });
    loadTables();
});

//加载列表数据
function loadTables() {
    $("#tableBody").html("");
    $("#checkAll").prop("checked", false);
    var controllerId = $("#j-ControllerId").val();
    console.log('a', controllerId);
    $.ajax({
        type: "GET",
        url: "/SystemSet/GetActionList?ControllerId=" + controllerId + "&_t=" + new Date().getTime(),
        success: function (data) {
            $.each(data.rows, function (i, item) {
                var tr = "<tr>";
                tr += "<td align='center'><input type='checkbox' class='checkboxs' value='" + item.id + "'/></td>";
                tr += "<td>" + item.Name + "</td>";
                tr += "<td>" + (item.Description == null ? "" : item.Description) + "</td>";
                tr += "<td><button class='btn btn-info btn-xs' href='javascript:;' onclick='edit(\"" + item.Id + "\")'><i class='fa fa-edit'></i> 编辑 </button> <button class='btn btn-danger btn-xs' href='javascript:;' onclick='deleteSingle(\"" + item.Id + "\")'><i class='fa fa-trash-o'></i> 删除 </button> </td>"
                tr += "</tr>";
                $("#tableBody").append(tr);
            })
        }
    })
}

function change() {
    //var self = this;
    loadTables();
};

function toController() {
    window.location.href = "/SystemSet/ControllerIndex";
}

//全选
function checkAll(obj) {
    $(".checkboxs").each(function () {
        if (obj.checked == true) {
            $(this).prop("checked", true)

        }
        if (obj.checked == false) {
            $(this).prop("checked", false)
        }
    });
};
//新增
function add() {
    var controllerId = $("#j-ControllerId").val();
    var controllerName = $("#j-ControllerId option:selected").text(); //获取选中的显示值
    
    $("#Id").val("0");
    $("#Title").text("新增功能项");
    $("#ControllerId").val(controllerId);
    $("#ControllerName").val(controllerName);
    $("#Name").val("");
    $("#Description").val("");
    //弹出新增窗体
    $("#addRootModal").modal("show");
};
//编辑
function edit(id) {
    var controllerId = $("#j-ControllerId").val();
    var controllerName = $("#j-ControllerId option:selected").text(); //获取选中的显示值
    $.ajax({
        type: "Get",
        url: "/SystemSet/GetActionById?id=" + id + "&_t=" + new Date(),
        success: function (data) {
            $("#Id").val(data.Id);
            $("#ControllerId").val(controllerId);
            $("#ControllerName").val(controllerName);
            $("#Name").val(data.Name);
            $("#Description").val(data.Description);
            $("#addRootModal").modal("show");
        }
    })
};
//保存
function save() {
    var postData = { "dto": { "Id": $("#Id").val(), "ControllerId": $("#ControllerId").val(), "Name": $("#Name").val(), "Description": $("#Description").val() } };
    $.ajax({
        type: "Post",
        url: "/SystemSet/EditAction",
        data: postData,
        success: function (data) {
            if (data.Result == "Success") {
                loadTables(1, 10);
                $("#addRootModal").modal("hide");
            } else {
                layer.tips(data.Message, "#btnSave", { tips: 4 });
            };
        }
    });
};
//批量删除
function deleteMulti() {
    var ids = "";
    $(".checkboxs").each(function () {
        if ($(this).prop("checked") == true) {
            ids += $(this).val() + ","
        }
    });
    ids = ids.substring(0, ids.length - 1);
    if (ids.length == 0) {
        layer.alert("请选择要删除的记录。");
        return;
    };
    //询问框
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        var sendData = { "ids": ids };
        $.ajax({
            type: "Post",
            url: "/SystemSet/DeleteMutiAction",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    loadTables(1, 10);
                    layer.closeAll();
                }
                else {
                    layer.alert("删除失败！");
                }
            }
        });
    });
};
//删除单条数据
function deleteSingle(id) {
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        $.ajax({
            type: "POST",
            url: "/SystemSet/DeleteAction",
            data: { "id": id },
            success: function (data) {
                if (data.Result == "Success") {
                    loadTables(1, 10);
                    layer.closeAll();
                }
                else {
                    layer.alert("删除失败！");
                }
            }
        })
    });
};