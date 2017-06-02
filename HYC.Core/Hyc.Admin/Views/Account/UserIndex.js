$(function () {
    $("#btnAdd").click(function () { add(); });
    $("#btnSave").click(function () { save(); });
    $("#btnDelete").click(function () { deleteMulti(); });
    $("#checkAll").click(function () { checkAll(this) });
    loadTables(1, 10);
});

//加载列表数据
function loadTables(startPage, pageSize) {
    $("#tableBody").html("");
    $("#checkAll").prop("checked", false);
    $.ajax({
        type: "GET",
        url: "/Account/GetUserList?startPage=" + startPage + "&pageSize=" + pageSize + "&_t=" + new Date().getTime(),
        success: function (data) {
            $.each(data.rows, function (i, item) {
                var tr = "<tr>";
                tr += "<td align='center'><input type='checkbox' class='checkboxs' value='" + item.id + "'/></td>";
                tr += "<td>" + item.UserName + "</td>";
                tr += "<td>" + (item.Name == null ? "" : item.Name) + "</td>";
                tr += "<td>" + (item.MobileNumber == null ? "" : item.MobileNumber) + "</td>";
                tr += "<td>" + (item.EMail == null ? "" : item.EMail) + "</td>";
                tr += "<td><button class='btn btn-info btn-xs' href='javascript:;' onclick='edit(\"" + item.Id + "\")'><i class='fa fa-edit'></i> 编辑 </button> <button class='btn btn-danger btn-xs' href='javascript:;' onclick='deleteSingle(\"" + item.Id + "\")'><i class='fa fa-trash-o'></i> 删除 </button> </td>"
                tr += "</tr>";
                $("#tableBody").append(tr);
            })
            var elment = $("#grid_paging_part"); //分页插件的容器id
            if (data.rowCount > 0) {
                var options = { //分页插件配置项
                    bootstrapMajorVersion: 3,
                    currentPage: startPage, //当前页
                    numberOfPages: data.rowsCount, //总数
                    totalPages: data.pageCount, //总页数
                    onPageChanged: function (event, oldPage, newPage) { //页面切换事件
                        loadTables(newPage, pageSize);
                    }
                }
                elment.bootstrapPaginator(options); //分页插件初始化
            }
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
    $("#Title").text("新增用户");
    $("#UserName").val("");
    $("#Name").val("");
    $("#Password").val("");
    $("#MobileNumber").val("");
    $("#EMail").val("");
    //弹出新增窗体
    $("#addRootModal").modal("show");
};
//编辑
function edit(id) {
    $.ajax({
        type: "Get",
        url: "/Account/GetUserById?id=" + id + "&_t=" + new Date(),
        success: function (data) {
            $("#Id").val(data.Id);
            $("#UserName").val(data.UserName);
            $("#Name").val(data.Name);
            $("#Password").val(data.Password);
            $("#MobileNumber").val(data.MobileNumber);
            $("#EMail").val(data.EMail);
            $("#addRootModal").modal("show");
        }
    })
};
//保存
function save() {
    var postData = { "dto": { "Id": $("#Id").val(), "UserName": $("#UserName").val(), "Password": $("#Password").val(), "Name": $("#Name").val(), "MobileNumber": $("#MobileNumber").val(), "EMail": $("#EMail").val() } };
    $.ajax({
        type: "Post",
        url: "/Account/EditUser",
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
            url: "/Account/DeleteMutiUser",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    loadTables(1, 10)
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
            url: "/Account/DeleteUser",
            data: { "id": id },
            success: function (data) {
                if (data.Result == "Success") {
                    loadTables(1, 10)
                    layer.closeAll();
                }
                else {
                    layer.alert("删除失败！");
                }
            }
        })
    });
};