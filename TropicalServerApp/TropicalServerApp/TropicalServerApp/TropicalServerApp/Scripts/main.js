
$(document).ready(function () {


    // when page loaded, check cookies exist
    if (Cookies.get()) {


        var username = Cookies.get('username');
        var password = Cookies.get('password');

        if (username != undefined && password != undefined) {

            $("#textUserID").attr("value", username);
            $("#textPassword").attr("value", password);

        };
    }
        // Login 
    $('.loginButton').click(function () {


        // cookie
        // if remember me checked, set cookies
        if ($("#rememberme").prop('checked')) {
            var username = $("#textUserID").val();
            var password = $("#textPassword").val();

            Cookies.set('username', username);
            Cookies.set('password', password);
            Cookies.set('remember', true);

        }

        // post login info
        $.ajax({
            type: "POST",
            url: "/Login/LoginClick",
            data: {
                userid: $(".useridtextbox").val(),
                password: $(".passwordtextbox").val()
            },
            success: function (result) {

                var response = JSON.parse(result);
                if (response["status"] == "success") {
                    window.location.href = response["redirect"];
                } else if (response["status"] == "failure") {
                    alert("username or password is incorrect");
                }

            }
        })

    });




    // log out
    $("#logout").click(function () {
        window.location.href = "/Login/Login";
    });

    // order table 
    $("#filterdata").click(function () {
        $.ajax({
            type: "POST",
            url: "/Product/FilterData",
            data: {
                OrderDate: $("#datetext").val(),
                CustomerID: $("#customerIDtext").val(),
                CustomerName: $("#customerNametext").val(),
                SalesManager: $("#managertext").val()
            },
            success: function (result) {

                var result = JSON.parse(result);
                // console.log(result);
                var tableBody = $('#data tbody');
                tableBody.empty();

                for (var rowkey in result) {
                    const tr = document.createElement('tr');
                    var row = result[rowkey];
                    const td2 = document.createElement('td');
                    const btn1 = document.createElement("BUTTON");
                    btn1.setAttribute("id", "view");
                    const btn2 = document.createElement("BUTTON");
                    btn2.setAttribute("id", "edit");
                    const btn3 = document.createElement("BUTTON");
                    btn3.setAttribute("id", "delete");

                    btn1.innerHTML = "View";
                    btn2.innerHTML = "Edit";
                    btn3.innerHTML = "Delete";
                        

                    for (var i in row) {
                        const td = document.createElement('td');
      
                        td.textContent = row[i];
                            
        
                        tr.appendChild(td);
                            
                    }
                        
                    td2.appendChild(btn1);
                    td2.appendChild(btn2);
                    td2.appendChild(btn3);


                    tr.appendChild(td2);
             
                    tableBody.append(tr);
                }

                   
            }
        })
    })

    $("body").on("click","#delete", function () {
          
        alert("Are you sure you want to delete this record?");
        $(this).parents("tr").remove();
    });

    $("body").on("click", "#edit", function () {

        var Tracking = $(this).parents("tr").find("td:eq(0)").text();
        var OrderDate = $(this).parents("tr").find("td:eq(1)").text();
        var CustomerID = $(this).parents("tr").find("td:eq(2)").text();
        var CustomerName = $(this).parents("tr").find("td:eq(3)").text();
        var Address = $(this).parents("tr").find("td:eq(4)").text();
        var Route = $(this).parents("tr").find("td:eq(5)").text();

        $(this).parents("tr").find("td:eq(0)").html('<input name="edit_tracking" value="' + Tracking + '">');
        $(this).parents("tr").find("td:eq(1)").html('<input name="edit_orderdate" value="' + OrderDate + '">');
        $(this).parents("tr").find("td:eq(2)").html('<input name="edit_customerID" value="' + CustomerID + '">');
        $(this).parents("tr").find("td:eq(3)").html('<input name="edit_customerName" value="' + CustomerName + '">');
        $(this).parents("tr").find("td:eq(4)").html('<input name="edit_Address" value="' + Address + '">');
        $(this).parents("tr").find("td:eq(5)").html('<input name="edit_route" value="' + Route + '">');

        $(this).parents("tr").find("td:eq(6)").prepend("<button class='btn-update'>Update</button>");
        $(this).parents("tr").find("#edit").hide();
        $(this).parents("tr").find("#view").hide();
        $(this).parents("tr").find("#delete").hide();
    });

    $("body").on("click", ".btn-update", function () {
        var Tracking = $(this).parents("tr").find("input[name='edit_tracking']").val();
        var OrderDate = $(this).parents("tr").find("input[name='edit_orderdate']").val();
        var CustomerID = $(this).parents("tr").find("input[name='edit_customerID']").val();
        var CustomerName = $(this).parents("tr").find("input[name='edit_customerName']").val();
        var Address = $(this).parents("tr").find("input[name='edit_Address']").val();
        var Route = $(this).parents("tr").find("input[name='edit_route']").val();

        $(this).parents("tr").find("td:eq(0)").text(Tracking);
        $(this).parents("tr").find("td:eq(1)").text(OrderDate);
        $(this).parents("tr").find("td:eq(2)").text(CustomerID);
        $(this).parents("tr").find("td:eq(3)").text(CustomerName);
        $(this).parents("tr").find("td:eq(4)").text(Address);
        $(this).parents("tr").find("td:eq(5)").text(Route);

        $(this).parents("tr").find("#edit").show();
        //$(this).parents("tr").find(".btn-cancel").remove();
        $(this).parents("tr").find("#view").show();
        $(this).parents("tr").find("#delete").show();
        $(this).parents("tr").find(".btn-update").remove();

    });

    //$("body").on("click", ".btn-cancel", function () {
    //    var Tracking = $(this).parents("tr").find("td:eq(0)").text();
    //    var OrderDate = $(this).parents("tr").find("td:eq(1)").text();
    //    var CustomerID = $(this).parents("tr").find("td:eq(2)").text();
    //    var CustomerName = $(this).parents("tr").find("td:eq(3)").text();
    //    var Address = $(this).parents("tr").find("td:eq(4)").text();
    //    var Route = $(this).parents("tr").find("td:eq(5)").text();

    //    $(this).parents("tr").find("td:eq(0)").text(Tracking);
    //    $(this).parents("tr").find("td:eq(1)").text(OrderDate);
    //    $(this).parents("tr").find("td:eq(2)").text(CustomerID);
    //    $(this).parents("tr").find("td:eq(3)").text(CustomerName);
    //    $(this).parents("tr").find("td:eq(4)").text(Address);
    //    $(this).parents("tr").find("td:eq(5)").text(Route);

    //    $(this).parents("tr").find("#edit").show();
    //    $(this).parents("tr").find("#view").show();
    //    $(this).parents("tr").find("#delete").show();
    //    $(this).parents("tr").find(".btn-cancel").remove();
    //    $(this).parents("tr").find(".btn-update").remove();

    //});


    // view 
    //$("body").on("click", "#view", function () {
    //    $.ajax({
    //        type: "POST",
    //        url: "/Product/FilterData",
    //        data: {
    //            OrderDate: $("#datetext").val(),
    //            CustomerID: $("#customerIDtext").val(),     
    //            SalesManager: $("#managertext").val()
    //        },



    });