var i = 0;
$(document).ready(function () {
    $('#add_more').on('click', function () {
        var colorR = Math.floor((Math.random() * 256));
        var colorG = Math.floor((Math.random() * 256));
        var colorB = Math.floor((Math.random() * 256));
        i++;
        var html = '<div id="append_no_' + i + '" class="animated bounceInLeft">' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-user-edit"></i></span>' +
            '</div>' +
            '<input type="text" placeholder="Full Name"  class="form-control"/>' +
            '</div>' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-phone-square"></i></span>' +
            '</div>' +
            '<input type="text" placeholder="Nick Name" class="form-control"/>' +
            '</div>' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-key"></i></span>' +
            '</div>' +
            '<input type="Password" placeholder="Password" class="form-control"/>' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-user-tie"></i></span>' +
            '</div>' +
            '<input type="Text" placeholder="Position" class="form-control"/>'
            '</div></div>' +
            
            
       
        $('#dynamic_container').append(html);
        $('#remove_more').fadeIn(function () {
            $(this).show();
        });
    });

    $('#remove_more').on('click', function () {

        $('#append_no_' + i).removeClass('bounceInLeft').addClass('bounceOutRight')
            .fadeOut(function () {
                $(this).remove();
            });
        i--;
        if (i == 0) {
            $('#remove_more').fadeOut(function () {
                $(this).hide()
            });;
        }

    });
    $("#addAnswer").click(function () {
        let queryId = $("#text-question").data("id");
        let text = $("#answer").val();
        let d = { id: queryId, text: text };
        $.ajax({
            type: 'POST',
            url: '/Process/QueryAnswer',
            contentType: 'application/json',
            data: JSON.stringify(d), // access in body
        }).done(function (data) {

            if (data.data.IsSucced) {
                $("#answer").val("");
                document.getElementById("text-question").innerText = "";
            }
        }).fail(function (msg) {
            console.log('FAIL');
        }).always(function (msg) {
            console.log('ALWAYS');
        });
       
    });
    $("#addQuery").click(function () {
        let id = document.getElementById("id").value;
        let question = $("#question").val();
        $.ajax({
            url: '/Process/AddQuery',
            type: "POST",
            dataType: "json",
            data: { userId: id, question: question },
            success: function (data) {

                if (data.data.IsSucced) {
                    console.log("isSucced")
                    $("#question").val("");
                }
            }
        });
    });
    $(document).on("click", "#cueryCancel",function () {
        let id = $(this).data("id");
        console.log(id);
        $.ajax({
            url: '/Process/CancelQuery',
            type: "POST",
            dataType: "json",
            data: { queryId: id },
            success: function (data) {
                if (data.data.IsSucced) {
                    console.log("isSucced")
                }
            }
        });
    });
    let k = document.getElementById("role").value;
    console.log(k);
    if (k === "Worker") {
        window.setInterval(function () {
            let id = document.getElementById("id").value;

            $.ajax({
                url: '/Process/GetQ',
                type: "GET",
                dataType: "json",
                data: { workerId: id },
                success: function (data2) {
                    if (data2.data.IsSucced) {
                        $("#text-question").data("id", data2.data.Data.Id)
                        $("#text-question").html(data2.data.Data.Question)
                    }
                }
            });



        }, 5000);
    }
    else if (k === "User") {
        window.setInterval(function () {
            let id = document.getElementById("id").value;

            $.ajax({
                url: '/Process/PendingQueryByUser',
                type: "GET",
                dataType: "json",
                data: { userId: id },
                success: function (data) {
                    if (data.data.IsSucced) {
                        $('table> tbody').html("");

                        data.data.Data.forEach(element => {
                            let item = '<tr>' +
                                '<td>' + element.Id + '</td>' +
                                '<td>' + element.Question + '</td>';
                            if (element.QueryStatus === 1) {
                                item += '<td>Pending</td>';
                            }
                            else if (element.QueryStatus === 2) {
                                item += '<td>Activ</td>';

                            }
                            else if (element.QueryStatus === 3) {
                                item += '<td>End</td>';

                            }
                            else {
                                item += '<td>Cancel</td>';

                            }
                            item += '<td>' + element.Answer + '</td>';
                            if (element.QueryStatus === 1) {
                                item+= '<td><button type="button" id="cueryCancel" data-id="' + element.Id + '" class="update btn btn-warningbtn-sm">Cancel</button></td>'
                            }
                            var $row = $(item);
                            $('table> tbody:last').append($row);

                        });

                    }
                }
            });



        }, 5000);
    }
    
   
});