﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(document).bind('ajaxStart', function () {
        $('#preloader').css('display', 'block');
    }).bind('ajaxStop', function () {
        $('#preloader').css('display', 'none');
    });
});

$(window).on('load', function () {
    //$(".loader").fadeOut();
    $("#preloader").delay(100).fadeOut("slow");

    /*------------------
        Product filter
    --------------------*/
    $('.filter__controls li').on('click', function () {
        $('.filter__controls li').removeClass('active');
        $(this).addClass('active');
    });
    if ($('.property__gallery').length > 0) {
        var containerEl = document.querySelector('.property__gallery');
        var mixer = mixitup(containerEl);
    }
});

function makePayment(url) {
    $.ajax({
        type: "POST",
        url: url,
        data: {},
        success: function (res) {
            if (res.res === true) {
                alertify.success(res.message);
                $('#largeModal').modal('hide');
                
                // delay 0,5s
                setTimeout(function () {
                    location.reload();
                }, 500);
            } else {
                alertify.error(res.message);
                $('#largeModal').modal('hide');
            }

        }
    });
}

function callPostActionWithForm(formInput) {
    let formData = new FormData(formInput);

    $.ajax({
        type: "POST",
        url: formInput.action,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.res === true) {
                if (response.viewName === "Profile") {
                    $('#main').html(response.partialView);
                }
                alertify.success('Updated successfully');
            } else if (response.res === "deleted" || response.res === "updated") {
                alertify.success(response.res + ' successfully');
            } else if (response.res === "modalUpdated") {
                $('#largeModal').modal('hide');
                alertify.success('updated successfully');
            } else if (response.res === false) {
                if (response.viewName === "_ProfileEdit") {
                    $('#profile-edit').html(response.partialView);
                    $('#profile-edit-button').click();
                } else if (response.viewName === "_ChangePassword") {
                    $('#profile-change-password').html(response.partialView);
                    $('#profile-change-password-button').click();
                }

                alertify.error('Update failed');
            }
        },
        error: function (err) {
            console.log(err);
        }
    })

    return false;
}


function createChangeRequest(formInput) {
    let formData = new FormData(formInput);

    $.ajax({
        type: "POST",
        url: formInput.action,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.res === false) {
                alertify.error('Request failed');
            } else if (response.res === 'updated') {
                alertify.success('Request successfully');
            }

            let $el = $('#fileElem');
            $el.wrap('<form>').closest(
                'form').get(0).reset();
            $el.unwrap();

            let $gallery = $('#gallery');
            $gallery.empty();
        },
        error: function (err) {
            console.log(err);
        }
    })

    return false;
}

function RemoveTutor() {
    $('#tutorInfo').attr("value", '');
    $('#tutorId').attr("value", 0);
}

function OpenGetDialog(url, title) {
    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (res) {
            $('#largeModal .modal-title').html(title);
            $('#largeModal .modal-body').html(res.partialView);

            $('#largeModal').modal('show')
        }
    })
}

function OpenConfirmDialog(url, title) {
    $('#verticalCentered .modal-title').html(title);

    $('#confirmDialogForm').attr('action', url);
    $('#verticalCentered').modal('show')

}

function LoadImage(url, id) {
    var formData = new FormData();
    formData.append('formFile', $('#formFile')[0].files[0]);
    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.res === true) {
                $('#' + id).attr("src", res.image);
                $('#image').attr("value", res.image);
            }
            console.log(res);

            location.reload();
        },
        error: function (err) {
            console.log(err);
            //alert(err);
        }
    })
    return false;
}

function ApproveTutor(id, name, phone) {
    $('#largeModal').hide();

    $('#largeModal .modal-body').html("");
    $(document.body).removeClass('modal-open');
    $('.modal-backdrop').remove();
    $('#tutorId').attr("value", id);
    $('#tutorInfo').attr("value", name + " - " + phone);

}

function CancelRequest(url) {
    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (res) {
            if (res.res === true) {
                alertify.success('Canceled successfully');
                setTimeout('', 1000);
                location.reload();
            } else {
                alertify.error('Cancel failed');
            }
        }
    })

}

function AddMajorSubject(id, name, des) {
    $('#tutorMajorCard .list-group')
        .append(` <div class=" list-group-item list-group-item-action" id="${id}-item">
                                    <div class="row">
                                        <input name="subjectId" value="${id}"  hidden="hidden" />
                                        <a href="/admin/subject/detail?id=${id}" class="col-11">
                                            <div class="d-flex w-100 justify-content-between">
                                                <h5 class="mb-1">` + name + `</h5>
                                            </div>
                                            <p class="mb-1">` + des + `</p>
                                        </a>
                                        <button type="button" class="col-1 btn btn-danger" onclick="RemoveMajorSubject('${id}')">Remove</button>

                                    </div>
                                </div>`);


}