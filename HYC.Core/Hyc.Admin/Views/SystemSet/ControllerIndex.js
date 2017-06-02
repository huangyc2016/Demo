$(function () {
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
    $.ajax({
        type: "GET",
        url: "/SystemSet/GetControllerList?_t=" + new Date().getTime(),
        success: function (data) {
            $.each(data.rows, function (i, item) {
                var tr = "<tr>";
                tr += "<td align='center'><input type='checkbox' class='checkboxs' value='" + item.Id + "'/></td>";
                tr += "<td>" + item.Name + "</td>";
                tr += "<td><a class='btn btn-link' href='javascript:;' onclick='toAction(\"" + item.Id + "\")'>" + item.ActionNum + "</a></td>";
                tr += "<td>" + (item.Description == null ? "" : item.Description) + "</td>";
                tr += "<td><button class='btn btn-info btn-xs' href='javascript:;' onclick='edit(\"" + item.Id + "\")'><i class='fa fa-edit'></i> 编辑 </button> <button class='btn btn-danger btn-xs' href='javascript:;' onclick='deleteSingle(\"" + item.Id + "\")'><i class='fa fa-trash-o'></i> 删除 </button> </td>"
                tr += "</tr>";
                $("#tableBody").append(tr);
            })
        }
    })
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
    $("#Id").val("0");
    $("#Title").text("新增控制器");
    $("#Name").val("");
    $("#Description").val("");
    //弹出新增窗体
    $("#addRootModal").modal("show");
};

function toAction(id) {
    window.location.href = "/SystemSet/ActionIndex?ControllerId=" + id + "";
}

//编辑
function edit(id) {
    $.ajax({
        type: "Get",
        url: "/SystemSet/GetControllerById?id=" + id + "&_t=" + new Date(),
        success: function (data) {
            $("#Id").val(data.Id);
            $("#Name").val(data.Name);
            $("#Description").val(data.Description);
            $("#addRootModal").modal("show");
        }
    })
};
//保存
function save() {
    var saveBtn = this;
    var postData = { "dto": { "Id": $("#Id").val(), "Name": $("#Name").val(), "Description": $("#Description").val() } };
    $.ajax({
        type: "Post",
        url: "/SystemSet/EditController",
        data: postData,
        beforeSend: function () {
            $(saveBtn).attr('disabled', 'disabled'); //让提交按钮失效，以实现防止按钮重复点击                      
        },
        complete: function () {
            $(saveBtn).removeAttr('disabled');
        },
        success: function (data) {
            if (data.Result == "Success") {
                loadTables();
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
            url: "/SystemSet/DeleteMutiController",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    loadTables();
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
            url: "/SystemSet/DeleteController",
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